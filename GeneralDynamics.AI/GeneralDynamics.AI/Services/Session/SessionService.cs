using GeneralDynamics.AI.Model;
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

        public SessionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Resultado<string>> Login(UserLogin userLogin)
        {

            var userLoginJson = new StringContent(JsonSerializer.Serialize(userLogin),
                 Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/session", userLoginJson);

            return await response.Content.ReadFromJsonAsync<Resultado<string>>();

        }

        public Task<Resultado> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
