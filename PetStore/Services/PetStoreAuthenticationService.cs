using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PetStore.Data.Entities;
using PetStore.Data.ViewModels;
using PetStore.Interface;

namespace PetStore.Services
{
    public class PetStoreAuthenticationService : IPetStoreAuthenticationService
    {
        private readonly ILogger<PetStoreAuthenticationService> _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PetStoreAuthenticationService(
            ILogger<PetStoreAuthenticationService> logger,
            SignInManager<StoreUser> signInManager,
            UserManager<StoreUser> userManager,
            IConfiguration config,
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel vm)
        {
            try
            {
             var user =   _mapper.Map<RegisterViewModel, StoreUser>(vm);
             user.UserName = vm.Email;
            return await _userManager.CreateAsync(user, vm.Password);


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<object> CreateToken(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null) return ("", "an error Occurred");
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return ("", "an error Occurred");
            // Create the Token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Audience"],
                claims,
                expires:  DateTime.UtcNow.AddMinutes(20),
                signingCredentials: credentials);

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return results;// Created("", results);

        }
    }
}
