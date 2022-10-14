using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ILoginService _loginService = null;

        public LoginController(IConfiguration config)
        {
            _config = config;
            _loginService = FactoryManager.GetInstance<ILoginService>();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {

            var data = await _loginService.Authenticate(userLogin);

            if(data != null)
            {
                var token = _loginService.GenerateToken(data.Respuesta, _config);
                return Ok(token);
            }

            return Ok(data);
        }
    }
}
