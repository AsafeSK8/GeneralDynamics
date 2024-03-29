﻿using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralDynamics.AI.Data;

namespace GeneralDynamics.AI.Services
{
    public interface IUserService
    {
        Task<Resultado<IEnumerable<User>>> GetAll();
        Task<Resultado<User>> GetById(int id);
        Task<Resultado> Add(User user);
        Task<Resultado> Delete(int id);
    }
}
