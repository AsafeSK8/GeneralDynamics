using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralDynamics.AI.Data;

namespace GeneralDynamics.AI.Services
{
    public interface IRoleService
    {
        Task<Resultado<IEnumerable<Role>>> GetAllRoles();
        Task<Resultado<Role>> GetRoleById(int id);
        Task<Resultado> AddRole(Role role);
        Task<Resultado> DeleteRole(int id);

    }
}
