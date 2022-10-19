using Blazored.LocalStorage;
using GeneralDynamics.AI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Data
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService;
        private ITokenManagerService _tokenManagerService;

        public AuthStateProvider(ILocalStorageService localStorageService, ITokenManagerService tokenManagerService)
        {
            _localStorageService = localStorageService;
            _tokenManagerService = tokenManagerService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new ClaimsIdentity();
            JwtSecurityToken jwtToken = await _tokenManagerService.GetJwtSecurityToken("token");

            if (jwtToken != null)
            {

                if (await _tokenManagerService.IsCurrentTokenValid("token"))
                {
                    identity = new ClaimsIdentity(new[]
                    { 
                    // As the token is already provided by the server, we only need to store the username and rol for the other actions
                    // Ya que el token los provee el servidor, sólo necesitamos almacenar el nombre de usuario y rol para las demás acciones
                    new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                    new Claim(ClaimTypes.Role, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
                    }, "apiauth_type");

                }
                else
                {
                    await _tokenManagerService.RemoveToken("token");
                }

            }

            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }


        public async Task MarkUserAsAuthenticated(string key)
        {
            JwtSecurityToken jwtToken = await _tokenManagerService.GetJwtSecurityToken(key);

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            { 
                // Ya que el token los provee el servidor, sólo necesitamos almacenar el nombre de usuario y rol para las demás acciones
                new Claim(ClaimTypes.NameIdentifier, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value),
                new Claim(ClaimTypes.Role, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
            }, "apiauth_type");

            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

        }

        public void MarkUserAsLoggedOut()
        {
            _localStorageService.RemoveItemAsync("token");

            ClaimsIdentity identity = new ClaimsIdentity();

            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
