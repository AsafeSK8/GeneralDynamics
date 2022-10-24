using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services
{
    // Service that manages all token related content
    public interface ITokenManagerService
    {
        Task<string> GetRawSecurityToken(string key);
        Task<JwtSecurityToken> GetJwtSecurityToken(string key);
        Task SaveToken(string key, string token);
        Task RemoveToken(string key);
        Task<bool> IsTokenValid(string token);
        Task<bool> IsCurrentTokenValid(string key);
    }
}
