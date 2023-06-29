using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using referral_management_system_1.Models;
using Newtonsoft.Json;

namespace referral_management_frontend.Pages
{
    public class ProviderModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public List<Provider> Providers { get; set; }

        public ProviderModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    await LoadProvidersAsync();
        //    return Page();
        //}


        //Load all providers with Search Existing providers function
        public async Task<IActionResult> OnGetAsync(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var response = await _httpClient.GetAsync(_apiBaseUrl + $"api/provider?name={searchTerm}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Providers = JsonConvert.DeserializeObject<List<Provider>>(content);
                }
                else
                {
                    // Handle the error
                }
            }
            else
            {
                await LoadProvidersAsync();
            }

            return Page();
        }


        //-- create provider  --
        public async Task<IActionResult> OnPostCreateAsync(string name, string address, int phoneNum)
        {
            var provider = new Provider
            {
                Name = name,
                Address = address,
                PhoneNum = phoneNum,
            };

            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "api/provider", provider);

            if (response.IsSuccessStatusCode)
            {
                await LoadProvidersAsync(); // Refresh the provider list
                return RedirectToPage("Provider");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        //-- update provider  --
        public async Task<IActionResult> OnPostUpdateAsync(int providerId, string updatedName, 
                                               string updatedAddress, int phoneNum)
        {
            var updatedProvider = new Provider
            {
                ProviderId = providerId,
                Name = updatedName,
                Address = updatedAddress,
                PhoneNum = phoneNum,
             };

            var response = await _httpClient.PutAsJsonAsync(_apiBaseUrl + $"api/provider/{providerId}", updatedProvider);

            if (response.IsSuccessStatusCode)
            {
                await LoadProvidersAsync(); // Refresh the provider list
                return RedirectToPage("Provider");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        //-- delete provider  --
        public async Task<IActionResult> OnPostDeleteAsync(int providerId)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"api/provider/{providerId}");

            if (response.IsSuccessStatusCode)
            {
                await LoadProvidersAsync(); // Refresh the provider list
                return RedirectToPage("Provider");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        private async Task LoadProvidersAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "api/provider");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Providers = JsonConvert.DeserializeObject<List<Provider>>(content);
            }
            else
            {
                // Handle the error
            }
        }
    }
}
