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
        Task<Resultado<IEnumerable<Role>>> GetAll();
        Task<Resultado<Role>> GetById(int id);
        Task<Resultado> Add(Role role);
        Task<Resultado> Delete(int id);

    }
}
