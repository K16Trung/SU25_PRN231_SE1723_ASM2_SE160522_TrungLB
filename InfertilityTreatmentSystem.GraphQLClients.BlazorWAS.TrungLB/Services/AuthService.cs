using Microsoft.JSInterop;
using TreatmentReminder.GraphQLClients.BlazorWAS.TrungLB.GraphQLClients;
using InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Models;

namespace InfertilityTreatmentSystem.GraphQLClients.BlazorWAS.TrungLB.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetCurrentUserAsync();
        Task<SystemUserAccount?> GetCurrentUserDetailsAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly GraphQLConsumer _graphQLClient;
        
        public AuthService(IJSRuntime jsRuntime, GraphQLConsumer graphQLClient)
        {
            _jsRuntime = jsRuntime;
            _graphQLClient = graphQLClient;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // Authenticate against the database via GraphQL
                var user = await _graphQLClient.AuthenticateUser(username, password);

                if (user != null && user.IsActive == true)
                {
                    // Store user info in localStorage
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", user.UserName);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userFullName", user.FullName ?? user.UserName);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userEmail", user.Email ?? "");
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userRole", user.RoleId?.ToString() ?? "");
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "loginTime", DateTime.Now.ToString());
                    
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log error and return false
                await _jsRuntime.InvokeVoidAsync("console.error", "Authentication error:", ex.Message);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userFullName");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userEmail");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userRole");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "loginTime");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var user = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
                return !string.IsNullOrEmpty(user);
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetCurrentUserAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
            }
            catch
            {
                return null;
            }
        }

        public async Task<SystemUserAccount?> GetCurrentUserDetailsAsync()
        {
            try
            {
                var username = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
                var fullName = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userFullName");
                var email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userEmail");
                var roleIdStr = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userRole");

                if (!string.IsNullOrEmpty(username))
                {
                    int.TryParse(roleIdStr, out int roleId);
                    
                    return new SystemUserAccount
                    {
                        UserName = username,
                        FullName = fullName,
                        Email = email,
                        RoleId = roleId == 0 ? null : roleId,
                        IsActive = true
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}