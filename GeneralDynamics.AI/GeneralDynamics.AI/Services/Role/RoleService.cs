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
    public class RoleService : IRoleService
    {

        private IGenericService _genericService;

        public RoleService(HttpClient httpClient, ILocalStorageService localStorageService, IGenericService genericService)
        {
            _genericService = genericService;
        }

        public async Task<Resultado<IEnumerable<Role>>> GetAll()
        {
            var resultado = new Resultado<IEnumerable<Role>>(true);
            try
            {
                resultado = await _genericService.Get<Resultado<IEnumerable<Role>>>("api/role");
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;

        }

        public async Task<Resultado<Role>> GetById(int id)
        {
            var resultado = new Resultado<Role>(true);
            try
            {
                resultado = await _genericService.Get<Resultado<Role>>($"api/role/{id}");
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> Add(Role role)
        {
            var resultado = new Resultado(true);
            try
            {
                resultado = await _genericService.Post<Resultado>($"api/role", role);

            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> Delete(int id)
        {
            var resultado = new Resultado(true);
            try
            {
                resultado = await _genericService.Delete<Resultado>($"api/role/{id}");
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
