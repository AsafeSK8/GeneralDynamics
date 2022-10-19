using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Factorias;
using GeneralDynamics.AI.Transversal.Mensajes;
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
    public class SessionController : ControllerBase
    {
        private IConfiguration _config;
        private ISessionService _sessionService = null;

        public SessionController(IConfiguration config)
        {
            _config = config;
            _sessionService = FactoryManager.GetInstance<ISessionService>();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {

            var data = await _sessionService.Authenticate(userLogin);

            if(data.ResultadoOperacion)
            {
                var token = _sessionService.GenerateToken(data.Respuesta, _config);
                await _sessionService.SaveToken(data.Respuesta, token.Respuesta);
                return Ok(token);
            }
            else
            {
                var res = new Resultado<string>(false);
                res.Mensaje = data.Mensaje;
                res.Mensajes = null;
                return Ok(res);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            var data = await _sessionService.LogOut(HttpContext);

            return Ok(data);
        }
    }
}
