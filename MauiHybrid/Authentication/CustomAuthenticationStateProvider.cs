using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiHybrid.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //Get Usersession from secure storage
                string getUserSessionFromStorage = await SecureStorage.Default.GetAsync("UserSession");
                if (string.IsNullOrEmpty(getUserSessionFromStorage))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                //Desrialize into and UserSession object.
                var DesrializedUserSession = JsonSerializer.Deserialize<UserSession>(getUserSessionFromStorage);
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, DesrializedUserSession.UserName!),
                    new Claim(ClaimTypes.Email, DesrializedUserSession.Email!),
                    new Claim(ClaimTypes.Role, DesrializedUserSession.UserRole!)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (!string.IsNullOrEmpty(userSession.UserName) || !string.IsNullOrEmpty(userSession.Email) || !string.IsNullOrEmpty(userSession.UserRole))
            {
                string serializeUserSession = JsonSerializer.Serialize(userSession);
                await SecureStorage.Default.SetAsync("UserSession", serializeUserSession);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName!),
                    new Claim(ClaimTypes.Email, userSession.Email!),
                    new Claim(ClaimTypes.Role, userSession.UserRole!)
                }));
            }
            else
            {
                SecureStorage.Default.Remove("UserSession");
                claimsPrincipal = anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

    }
}
