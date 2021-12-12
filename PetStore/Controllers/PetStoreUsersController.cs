using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetStore.Data.ViewModels;
using PetStore.Interface;
using System;
using System.Threading.Tasks;

namespace PetStore.Controllers
{
    [Route("api/[controller]", Name = "petstoreusers")]
    [ApiController]
    public class PetStoreUsersController : ControllerBase
    {
        private readonly ILogger<PetStoreUsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IPetStoreAuthenticationService _petStoreAuthentication;

        public PetStoreUsersController(ILogger<PetStoreUsersController> logger,
            IMapper mapper,
            IPetStoreAuthenticationService petStoreAuthentication)
        {
            _logger = logger;
            _mapper = mapper;
            _petStoreAuthentication = petStoreAuthentication;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _petStoreAuthentication.RegisterUser(vm));
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Login error happened");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            try
            {
                return Ok(await _petStoreAuthentication.CreateToken(vm));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Login error happened");
        }
    }
}