using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralDynamics.AI.Transversal.Mensajes;
using GeneralDynamics.AI.Model;

namespace GeneralDynamics.AI.Services
{
    public interface ISessionService
    {
        Task<Resultado<string>> Login(UserLogin userLogin);

        Task<Resultado> LogOut();
    }
}
