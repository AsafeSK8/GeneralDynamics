using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace GeneralDynamics.AI.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Resultado> AddUser(User user)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (user != null)
                {
                    // Comprobamos si existe usuario con el mismo username, en el caso de que si cancelamos la operación
                    var existingUser = await _repository.Get(x => x.UserName == user.UserName);

                    if (existingUser.Any() && user.Id <= 0)
                    {
                        resultado.ResultadoOperacion = false;
                        resultado.Mensaje = "Username Already Exists";

                        return resultado;
                    }

                    var userToModify = existingUser.FirstOrDefault();

                    string passwordHash = BC.HashPassword(user.Password);

                    user.Password = passwordHash;

                    if (userToModify != null && userToModify.Id > 0)
                    {
                        userToModify.Name = user.Name;
                        userToModify.LastName = user.LastName;
                        userToModify.Phone = user.Phone;
                        userToModify.Email = user.Email;
                        userToModify.RoleId = user.RoleId;
                        userToModify.Password = passwordHash;
                        await _repository.Update(userToModify);
                        return resultado;
                    }

                    await _repository.Add(user);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "User can not be empty";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> AddUsers(IEnumerable<User> user)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (user.ToList().Count > 0)
                {
                    await _repository.AddRange(user);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Users can not be empty";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado<IEnumerable<User>>> FindUsers(Expression<Func<User, bool>> predicate)
        {
            Resultado<IEnumerable<User>> resultado = new Resultado<IEnumerable<User>>(true);

            try
            {
                var data = await _repository.Find(predicate);

                if (data.ToList().Count > 0)
                {
                    resultado.Respuesta = data;
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "No Users found";
                }

            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado<IEnumerable<User>>> GetAllUsers()
        {
            Resultado<IEnumerable<User>> resultado = new Resultado<IEnumerable<User>>(true);

            try
            {
                var data = await _repository.GetAll();

                if (data.ToList().Count > 0)
                {
                    resultado.Respuesta = data;
                    resultado.Mensajes = null;
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "No Users found";
                    resultado.Mensajes = null;
                }

            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
                resultado.Mensajes = null;
            }

            return resultado;
        }

        public async Task<Resultado<User>> GetUserById(int id)
        {
            Resultado<User> resultado = new Resultado<User>(true);

            try
            {
                //var data = await _repository.GetById(id);

                var userSet = await _repository.Get(x => x.Id == id, includeProperties: "Role");
                var data = userSet.FirstOrDefault();

                if (data != null)
                {
                    resultado.Respuesta = data;
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

        public async Task<Resultado> RemoveUser(int id)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (id <= 0)
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Select at least one User to remove";
                    return resultado;
                }

                var role = await _repository.GetById(id);

                if (role != null)
                {
                    await _repository.Remove(role);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Select at least one User to remove";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> RemoveUsers(IEnumerable<int> ids)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (ids.ToList().Count <= 0)
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Select at least one User to remove";
                    return resultado;
                }

                var roles = await _repository.Find(x => ids.Contains(x.Id));

                if (roles.ToList().Count > 0)
                {
                    await _repository.RemoveRange(roles);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Could not find Users";
                }
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
