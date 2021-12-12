using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PetStore.Data.Entities;
using PetStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PetStore.Data
{
    public class DbInitializer
    {
        private readonly PetStoreContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IMapper _mapper;

        public DbInitializer(PetStoreContext ctx,
            IWebHostEnvironment hosting,
            UserManager<StoreUser> userManager,
            IMapper mapper)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task SeedAsync()
        {
            await _ctx.Database.MigrateAsync();

            // _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("njorojimm@gmail.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "James",
                    LastName = "Njoroge",
                    Email = "njorojimm@gmail.com",
                    UserName = "njorojimm@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }

            if (!_ctx.Products.Any())
            {
                for (var i = 0; i < 10; i++)
                {
                    using var client = new HttpClient();
                    var url = ($"https://api.thedogapi.com/v1/images/search?size=med&mime_types=jpg&format=json&has_breeds=true&order=DESC&page={i}&limit=10");
                    var baseUrl = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("x-api-key", $"721ed885-7f2f-4e34-b82c-53e2c0846290");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(baseUrl);
                    var contentStr = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Pets>>(contentStr);
                    await _ctx.AddRangeAsync(_mapper.Map<List<Pets>, List<Product>>(data));

                    Console.WriteLine(@"{i}");
                    Console.WriteLine($"{i}");
                }


                await _ctx.SaveChangesAsync();
            }
        }
    }
}