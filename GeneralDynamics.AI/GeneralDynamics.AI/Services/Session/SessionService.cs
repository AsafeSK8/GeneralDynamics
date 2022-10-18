using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Services.Generic;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services
{
    public class SessionService : ISessionService
    {

        private readonly HttpClient _httpClient;
        private IGenericService _genericService;

        public SessionService(HttpClient httpClient, IGenericService genericService)
        {
            _httpClient = httpClient;
            _genericService = genericService;
        }

        public async Task<Resultado<string>> Login(UserLogin userLogin)
        {

            var userLoginJson = new StringContent(JsonSerializer.Serialize(userLogin),
                 Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/session", userLoginJson);

            return await response.Content.ReadFromJsonAsync<Resultado<string>>();

            //var resultado = new Resultado<string>(true);
            //try
            //{
            //    resultado = await _genericService.Post<Resultado<string>>("api/login", userLogin);
            //}
            //catch (Exception e)
            //{
            //    resultado.ResultadoOperacion = false;
            //    resultado.Mensaje = e.Message;
            //}

            //return resultado;

        }

        public Task<Resultado> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
