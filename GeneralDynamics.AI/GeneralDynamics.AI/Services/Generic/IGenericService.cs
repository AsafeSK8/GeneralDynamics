using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services.Generic
{
    public interface IGenericService
    {
        Task<T> Get<T>(string path);
        Task<T> Post<T>(string path, object obj);
        Task<T> Delete<T>(string path);
    }
}
