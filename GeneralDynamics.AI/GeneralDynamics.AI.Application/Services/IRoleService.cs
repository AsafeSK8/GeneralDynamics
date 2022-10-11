using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Application.Services
{
    public interface IRoleService : IApplicationService
    {
        Task<Resultado<IEnumerable<Role>>> GetAllRoles();

        Task<Resultado<Role>> GetRoleById(int id);

        Task<Resultado<IEnumerable<Role>>> FindRoles(Expression<Func<Role, bool>> predicate);

        Task<Resultado> AddRole(Role role);

        Task<Resultado> AddRoles(IEnumerable<Role> roles);

        Task<Resultado> RemoveRole(int id);

        Task<Resultado> RemoveRoles(IEnumerable<int> ids);
    }
}
