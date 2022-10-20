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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public async Task<Resultado> AddRole(Role role)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (role != null)
                {
                    // Comprobamos si existe rol con el mismo código, en el caso de que si cancelamos la operación
                    var existingRol = await _repository.Get(x => x.Code == role.Code);

                    if (existingRol.Any() && role.Id <= 0)
                    {
                        resultado.ResultadoOperacion = false;
                        resultado.Mensaje = "Rol Code Already Exists";

                        return resultado;
                    }

                    var roleToModify = existingRol.FirstOrDefault();

                    if(roleToModify != null && roleToModify.Id > 0)
                    {
                        roleToModify.Code = role.Code;
                        roleToModify.Description = role.Description;

                        await _repository.Update(roleToModify);
                        return resultado;
                    }

                    await _repository.Add(role);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Rol can not be empty";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> AddRoles(IEnumerable<Role> roles)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (roles.ToList().Count > 0)
                {
                    await _repository.AddRange(roles);
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Roles can not be empty";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado<IEnumerable<Role>>> FindRoles(Expression<Func<Role, bool>> predicate)
        {
            Resultado<IEnumerable<Role>> resultado = new Resultado<IEnumerable<Role>>(true);

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
                    resultado.Mensaje = "No Roles found";
                }

            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado<IEnumerable<Role>>> GetAllRoles()
        {
            Resultado<IEnumerable<Role>> resultado = new Resultado<IEnumerable<Role>>(true);

            try
            {
                var data = await _repository.GetAll();

                if (data.ToList().Count > 0)
                {
                    resultado.Respuesta = data;
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "No Roles found";
                }

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
            Resultado<Role> resultado = new Resultado<Role>(true);

            try
            {
                var data = await _repository.GetById(id);

                if(data != null)
                {
                    resultado.Respuesta = data;
                }
                else
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Role not found";
                }
            }
            catch(Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> RemoveRole(int id)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if (id <= 0)
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Select at least one Role to remove";
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
                    resultado.Mensaje = "Select at least one Role to remove";
                }
            }
            catch (Exception e)
            {
                resultado.ResultadoOperacion = false;
                resultado.Mensaje = e.Message;
            }

            return resultado;
        }

        public async Task<Resultado> RemoveRoles(IEnumerable<int> ids)
        {
            Resultado resultado = new Resultado(true);

            try
            {
                if(ids.ToList().Count <= 0)
                {
                    resultado.ResultadoOperacion = false;
                    resultado.Mensaje = "Select at least one Role to remove";
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
                    resultado.Mensaje = "Could not find Roles";
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
