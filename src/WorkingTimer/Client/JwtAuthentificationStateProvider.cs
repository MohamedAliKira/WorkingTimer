using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WorkingTimer.Client
{
    public class JwtAuthentificationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;

        public JwtAuthentificationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                // The user is logged in
                var tokenAsString = await _storage.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                return authState;
            }
            return new AuthenticationState(new ClaimsPrincipal()); // Empty Claims principal means no identity and the user is not logged in
        }
    }
}
