using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using referral_management_system_1.Models;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;

namespace referral_management_frontend.Pages
{
    public class SpecialtyModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public List<Specialties> Specialties { get; set; }

        public string SpecialtyName { get; set; } // Added property to store  

        public SpecialtyModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        public async Task<IActionResult> OnGetAsync(string searchTerm, string specialtyName)
        {
            SpecialtyName = specialtyName; // Store the specialty ID

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var response = await _httpClient.GetAsync(_apiBaseUrl + $"api/Specialties?name={searchTerm}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Specialties = JsonConvert.DeserializeObject<List<Specialties>>(content);
                }
                else
                {
                    // Handle the error
                }
            }
            else
            {
                await LoadSpecialtiesAsync();
            }


            return Page();
        }

        //-- create Specialties
        public async Task<IActionResult> OnPostCreateAsync(Specialties specialties)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "api/Specialties", specialties);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to the patients list page or perform any other action
                return RedirectToPage("Specialty");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        //-- Update Existing Specialties
        public async Task<IActionResult> OnPostUpdateAsync(int specialtyId, Specialties updatedSpecialty)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiBaseUrl + $"api/specialties/{specialtyId}", updatedSpecialty);

            if (response.IsSuccessStatusCode)
            {
                await LoadSpecialtiesAsync(); // Refresh the patient list
                return RedirectToPage("Specialty");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int specialtyId)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"api/specialties/{specialtyId}");

            if (response.IsSuccessStatusCode)
            {
                await LoadSpecialtiesAsync();
                return RedirectToPage("Specialty");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        private async Task LoadSpecialtiesAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "api/specialties");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Specialties = JsonConvert.DeserializeObject<List<Specialties>>(content);
            }
            else
            {
                // Handle the error
            }
        }
    }
}
