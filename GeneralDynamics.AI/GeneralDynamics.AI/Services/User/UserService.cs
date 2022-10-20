using Blazored.LocalStorage;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Services.Generic;

namespace GeneralDynamics.AI.Services
{
    public class UserService : IUserService
    {

        private IGenericService _genericService;

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService, IGenericService genericService)
        {
            _genericService = genericService;
        }

        public async Task<Resultado<IEnumerable<User>>> GetAllUsers()
        {
            var resultado = new Resultado<IEnumerable<User>>(true);
            try
            {
                resultado = await _genericService.Get<Resultado<IEnumerable<User>>>("api/user");
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;

        }

        public async Task<Resultado<User>> GetUserById(int id)
        {
            var resultado = new Resultado<User>(true);
            try
            {
                resultado = await _genericService.Get<Resultado<User>>($"api/user/{id}");
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> AddUser(User user)
        {
            var resultado = new Resultado(true);
            try
            {
                resultado = await _genericService.Post<Resultado>($"api/user", user);

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
