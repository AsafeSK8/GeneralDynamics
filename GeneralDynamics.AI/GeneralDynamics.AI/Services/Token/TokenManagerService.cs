using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        private ILocalStorageService _localStorageService;

        public TokenManagerService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<JwtSecurityToken> GetJwtSecurityToken(string key)
        {
            var token = await _localStorageService.GetItemAsync<string>(key);

            if (string.IsNullOrEmpty(token))
                return null;

            return DecodeToken(token);
        }

        public async Task<string> GetRawSecurityToken(string key)
        {
            var token = await _localStorageService.GetItemAsync<string>(key);

            if (string.IsNullOrEmpty(token))
                return null;

            return token;
        }

        public async Task<bool> IsCurrentTokenValid(string key)
        {
            var token = await _localStorageService.GetItemAsync<string>(key);
            if (string.IsNullOrEmpty(token))
                return false;

            var jwtToken = DecodeToken(token);

            string date = jwtToken.Claims.First(c => c.Type == "exp").Value;

            // We use DateTimeOffset instead of DateTime since DateTimeOffset gets the current local time
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(date));
            DateTime dateTime = dateTimeOffset.UtcDateTime;

            // The correct way of comparing DateTime is with DateTimeOffset as shown below to avoid date conversions as the DateTimeOffset does
            // it for us correctly using local precise times.
            if (dateTime > DateTimeOffset.Now)
            {
                return true;
            }

            return false;
        }

        public Task<bool> IsTokenValid(string token)
        {
            throw new NotImplementedException();
        }

        public Task RemoveToken(string key)
        {
            throw new NotImplementedException();
        }

        public async Task SaveToken(string key, string token)
        {
            await _localStorageService.SetItemAsync(key, token);
        }

        public JwtSecurityToken DecodeToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);

            return jwtSecurityToken;
        }
    }
}
