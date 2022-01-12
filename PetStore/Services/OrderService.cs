using AutoMapper;
using Microsoft.Extensions.Configuration;
using PetStore.Interface;
using System.Threading.Tasks;
using System;
using PetStore.Data.Entities;
using PetStore.Data.ViewModels;
using PetStore.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace PetStore.Services
{
  
    public class OrderService : IOrderService
    {
        private readonly IPetStoreRepository _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IMailService _mailService;

        public OrderService(IPetStoreRepository repository,
            IConfiguration config,
            IMapper mapper, UserManager<StoreUser> userManager, IMailService mailService)
        {
            _repository = repository;
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderViewModel model, string user)
        {
            try
            {
                var newOrder = _mapper.Map<Order>(model);
                var currentUser = await _userManager.FindByNameAsync(user);
                newOrder.User = currentUser;
                _repository.AddEntity(newOrder);
                _repository.SaveAll();
                // send email

                await SendOrderEmail(currentUser.Email, model.Items,$"PetStore Order Confirmation {model.OrderNumber}");

                return _mapper.Map<OrderViewModel>(newOrder);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return null;
            }
        }

        private async Task SendOrderEmail(string customermail, ICollection<OrderItemViewModel> vm,string order)
        {
            try
            {
                string quote = @"Her name was ""Sara.""";
                var msg = new StringBuilder();
                msg.Append("<h2>PetStore Order Confirmation</h2>");
                msg.Append(@"<table border=""1"" ><tr> <th> Pet </th><th> Description </th><th> Price </th> </tr>");
                foreach (var item in vm)
                {
                    msg.Append($"<tr>");
                    msg.Append($"<td>  {item.ProductTitle} </td> ");
                    msg.Append($"<td>  {item.ProductDescription} </td> ");
                    msg.Append($"<td> $ {item.UnitPrice} </td> ");
                    msg.Append($"</tr>");
                }
                msg.Append($"</table>");
                msg.Append("<p>Thank you for shopping with us.</p>");

                await  _mailService.GMailSMTP(customermail, Convert.ToString(msg), order);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
