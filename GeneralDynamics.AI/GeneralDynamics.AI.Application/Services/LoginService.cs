using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Factorias;
using GeneralDynamics.AI.Transversal.Mensajes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Application.Services
{
    public class LoginService : ILoginService
    {
        private IConfiguration _config;
        private readonly IRepository<User> _repository;

        public LoginService(IRepository<User> repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<Resultado<UserDTO>> Authenticate(UserLogin userLogin)
        {
            Resultado<UserDTO> resultado = new Resultado<UserDTO>(true);

            Expression<Func<User, bool>> expr = x => x.UserName == userLogin.Username && x.Password == userLogin.Password;

            try
            {

                var data = await _repository.FindOne(expr);

                if (data != null)
                {
                    resultado.Respuesta = FactoryManager.Map<User, UserDTO>(data);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "User not found";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public Resultado<string> GenerateToken(UserDTO user, IConfiguration config)
        {
            Resultado<string> resultado = new Resultado<string>(true);

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);

                resultado.Respuesta = new JwtSecurityTokenHandler().WriteToken(token);
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
