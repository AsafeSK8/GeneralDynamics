using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Mensajes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Application.Services
{
    public interface ISessionService : IApplicationService
    {
        Task<Resultado<UserDTO>> Authenticate(UserLogin userLogin);
        public Resultado<string> GenerateToken(UserDTO user, IConfiguration config);

        public Task<Resultado> SaveToken(UserDTO userDto, string token);

        public UserDTO GetCurrentUser(HttpContext context);

        public Task<Resultado> LogOut(HttpContext context);
    }
}
