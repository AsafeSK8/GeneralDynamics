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

        public async Task<Resultado<IEnumerable<Role>>> GetAllRoles()
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

        public async Task<Resultado<Role>> GetRoleById(int id)
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

        public async Task<Resultado> AddRole(Role role)
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

        public async Task<Resultado> DeleteRole(int id)
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
