using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BlazorApp2.Authentication
{
    public class Lamport : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous= new ClaimsPrincipal(new ClaimsIdentity());

        public Lamport(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;

        }
        public override async  Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var Claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
            new(ClaimTypes.Name,userSession.UserName),
            new(ClaimTypes.Role,userSession.Role)
            }, "LamportAuth"));
                return await Task.FromResult(new AuthenticationState(Claims));
            }
            catch 
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

           
            
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
            new(ClaimTypes.Name,userSession.UserName),
            new(ClaimTypes.Role,userSession.Role)
            }));
            } 
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;

            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
