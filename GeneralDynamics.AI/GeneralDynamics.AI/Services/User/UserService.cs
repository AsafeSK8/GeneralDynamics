using Blazored.LocalStorage;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Services.Generic;
using Newtonsoft.Json.Linq;

namespace GeneralDynamics.AI.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;
        private GenericService _genericService;

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _genericService = new GenericService(_httpClient, _localStorageService);
        }

        public async Task<Resultado<IEnumerable<User>>> GetAllUsers()
        {
            var resultado = new Resultado<IEnumerable<User>>(true);
            try
            {
                resultado = await _genericService.GetAll<Resultado<IEnumerable<User>>>("api/user/all");
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;

        }
    }
}
