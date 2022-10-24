using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Application.Services
{
    public interface IUserService : IApplicationService
    {
        Task<Resultado<IEnumerable<User>>> GetAllUsers();

        Task<Resultado<User>> GetUserById(int id);

        Task<Resultado<IEnumerable<User>>> FindUsers(Expression<Func<User, bool>> predicate);

        Task<Resultado> AddUser(User user);

        Task<Resultado> AddUsers(IEnumerable<User> user);

        Task<Resultado> RemoveUser(int id);

        Task<Resultado> RemoveUsers(IEnumerable<int> ids);

    }
}
