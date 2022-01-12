using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetStore.Data.Entities;
using PetStore.Data.ViewModels;
using PetStore.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShopController : ControllerBase
    {
        private readonly IPetStoreRepository _repository;
        private readonly ILogger<ShopController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly UserManager<StoreUser> _userManager;

        public ShopController(IPetStoreRepository repository, ILogger<ShopController> logger,
               IMapper mapper, IOrderService orderService
               )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _orderService = orderService;
            // _userManager = userManager;
        }

        [HttpGet]
        [Route("getproducts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>>  GetProducts()
        {
            try
            {
                return Ok(await _repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("failed to get products");
            }
        }

        [HttpPost]
        [Route("postorder")]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _orderService.CreateOrderAsync(model, User.Identity.Name);
                    if (response!=null)
                    {
                        return Created($"/api/orders/{response.OrderId}", response);
                    }
                    else
                    {
                        return BadRequest("Failed to save new order");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order: {ex}");
            }

            return BadRequest("Failed to save new order.");
        }

        [HttpGet]
        [Route("getorderitems")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

    }
}
