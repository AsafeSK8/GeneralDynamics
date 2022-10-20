using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Services.Generic;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services
{
    public class SessionService : ISessionService
    {
        private IGenericService _genericService;

        public SessionService(HttpClient httpClient, IGenericService genericService)
        {
            _genericService = genericService;
        }

        public async Task<Resultado<string>> Login(UserLogin userLogin)
        {

            var resultado = new Resultado<string>(true);

            try
            {
                resultado = await _genericService.Post<Resultado<string>>("api/session", userLogin);
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;

        }

        //public async Task<bool> SaveToken(string token)
        //{
            
        //}

        public Task<Resultado> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
