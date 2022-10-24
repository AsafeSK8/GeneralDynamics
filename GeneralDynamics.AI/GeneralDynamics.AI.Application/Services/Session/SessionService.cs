using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Factorias;
using GeneralDynamics.AI.Transversal.Mensajes;
using Microsoft.AspNetCore.Http;
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
using BC = BCrypt.Net.BCrypt;

namespace GeneralDynamics.AI.Application.Services
{
    public class SessionService : ISessionService
    {
        private IConfiguration _config;
        private readonly IRepository<User> _repository;

        public SessionService(IRepository<User> repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<Resultado<UserDTO>> Authenticate(UserLogin userLogin)
        {
            Resultado<UserDTO> resultado = new Resultado<UserDTO>(true);

            Expression<Func<User, bool>> expr = x => x.UserName == userLogin.Username;

            try
            {

                //var data = await _repository.FindOne(expr);
                var data = await _repository.Get(expr, includeProperties: "Role");

                if (data.Any() && BC.Verify(userLogin.Password, data.FirstOrDefault().Password))
                {
                    resultado.Respuesta = FactoryManager.Map<User, UserDTO>(data.FirstOrDefault());
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Wrong Username or Password";
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
                new Claim(ClaimTypes.Role, user.Role)
            };

                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
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

        public async Task<Resultado> SaveToken(UserDTO userDto, string token)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                var user = await _repository.GetById(userDto.Id);
                user.Token = token;
                await _repository.Update(user);
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public UserDTO GetCurrentUser(HttpContext context)
        {
            var identity = GetUserIdentity(context);

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDTO
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }

            return null;
        }

        public async Task<Resultado> LogOut(HttpContext context)
        {

            Resultado resultado = new Resultado(true);

            try
            {
                // TODO Limpiar Cookies y caché del navegador

                var identity = GetUserIdentity(context);

                string username = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!String.IsNullOrEmpty(username))
                {
                    var userSet = await _repository.Get(x => x.UserName == username);

                    var user = userSet.FirstOrDefault();

                    user.Token = "";

                    await _repository.Update(user);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Error: No se encotró el usuario";
                }

            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        private ClaimsIdentity GetUserIdentity(HttpContext context)
        {
            return context.User.Identity as ClaimsIdentity;
        }
    }
}
