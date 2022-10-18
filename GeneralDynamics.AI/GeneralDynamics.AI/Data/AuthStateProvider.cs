using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Data
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService;

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            string token = await _localStorageService.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityToken jwtToken = DecodeToken(token);

                string userc = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

                identity = new ClaimsIdentity(new[]
                { 
                // Ya que el token los provee el servidor, sólo necesitamos almacenar el nombre de usuario y rol para las demás acciones
                new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                //new Claim(ClaimTypes.Email, jwtToken.Claims.First(claim => claim.Type == "email").Value),
                new Claim(ClaimTypes.Role, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
                //new Claim(ClaimTypes.Name, jwtToken.Claims.First(claim => claim.Type == "name").Value),
                //new Claim(ClaimTypes.Surname, jwtToken.Claims.First(claim => claim.Type == "lastname").Value)
            }, "apiauth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }



            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }


        public void MarkUserAsAuthenticated(string token)
        {
            JwtSecurityToken jwtToken = DecodeToken(token);

            string userc = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            { 
                // Ya que el token los provee el servidor, sólo necesitamos almacenar el nombre de usuario y rol para las demás acciones
                new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                //new Claim(ClaimTypes.Email, jwtToken.Claims.First(claim => claim.Type == "email").Value),
                new Claim(ClaimTypes.Role, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
                //new Claim(ClaimTypes.Name, jwtToken.Claims.First(claim => claim.Type == "name").Value),
                //new Claim(ClaimTypes.Surname, jwtToken.Claims.First(claim => claim.Type == "lastname").Value)
            }, "apiauth_type");

            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        }

        public JwtSecurityToken DecodeToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);

            return jwtSecurityToken;
        }
    }
}
