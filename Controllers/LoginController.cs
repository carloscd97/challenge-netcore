using BcpChallenge.Authetication;
using BcpChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcpChallenge.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IJwtAutheticationManager _security;

        public LoginController(IJwtAutheticationManager security)
        {
            _security = security;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            var userAuthentication = _security.Autheticate(loginViewModel.Username, loginViewModel.Password);
            if (userAuthentication == null)
                return Unauthorized("Usuario no encontrado");
            return Ok(userAuthentication);
        }
    }
}
