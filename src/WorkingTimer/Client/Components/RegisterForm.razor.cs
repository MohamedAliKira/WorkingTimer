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
    public partial class RegisterForm : ComponentBase
    {
        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        
        
        private RegisterRequest _model = new RegisterRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private string _activateCompte = string.Empty;

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            _activateCompte = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("auth/register", _model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserManagerResponse>();
                string[] rslt = result.Message.Split("|");
                _activateCompte = (rslt[1]);
                // NavigationManager.NavigateTo("/authentication/login");
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<UserManagerResponse>();
                _errorMessage =  errorResponse.Message + " || " + errorResponse.Errors.First();
            }

            _isBusy = false;
        }
    }
}
