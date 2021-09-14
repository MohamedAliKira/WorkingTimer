using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkingTimer.Shared;

namespace WorkingTimer.Client.Components
{
    public partial class LoginForm_Mud : ComponentBase
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public ILocalStorageService storageService { get; set; }

        private LoginRequest _model = new LoginRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("auth/login", _model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserManagerResponse>();
                //store token in localStorage
                await storageService.SetItemAsStringAsync("access_token", result.Message);
                await storageService.SetItemAsync<DateTime>("expiry_date", result.ExpireDate ?? DateTime.Now);

                await AuthenticationStateProvider.GetAuthenticationStateAsync();

                NavigationManager.NavigateTo("/");
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<UserManagerResponse>();
                _errorMessage = errorResponse.Message;
            }

            _isBusy = false;
        }
    }
}
