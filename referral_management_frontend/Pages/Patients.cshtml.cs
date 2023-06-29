using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using referral_management_system_1.Models;
using static System.Net.WebRequestMethods;
//using referral_management_frontend.Models;
using Newtonsoft.Json;


namespace referral_management_frontend.Pages
{
    public class PatientsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public List<Patient> Patients { get; set; }

         public PatientsModel(HttpClient httpClient, IConfiguration configuration)

        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        //Load all patients with Search Existing patients function 
            public async Task<IActionResult> OnGetAsync(string searchTerm)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var response = await _httpClient.GetAsync(_apiBaseUrl + $"api/patient?name={searchTerm}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Patients = JsonConvert.DeserializeObject<List<Patient>>(content);
                    }
                    else
                    {
                        // Handle the error
                    }
                }
                else
                {
                    await LoadPatientsAsync();
                }

                return Page();
            }



        //-- create  patients  --
        public async Task<IActionResult> OnPostCreateAsync(Patient patient)
        {
            // Set the DateIn property of the patient object based on the submitted form data
            //patient.DateIn = patient.DateIn.ToUniversalTime();

            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "api/patient", patient);

            if (response.IsSuccessStatusCode)
            {
                // Redirect to the patients list page or perform any other action
                return RedirectToPage("Patients");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        //-- Update Existing patients  --

        public async Task<IActionResult> OnPostUpdateAsync(int patientId, Patient updatedPatient)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiBaseUrl + $"api/patient/{patientId}", updatedPatient);

            if (response.IsSuccessStatusCode)
            {
                await LoadPatientsAsync(); // Refresh the patient list
                return RedirectToPage("Patients");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }


        //-- Delete Existing patients  --
        public async Task<IActionResult> OnPostDeleteAsync(int patientId)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"api/patient/{patientId}");

            if (response.IsSuccessStatusCode)
            {
                // Redirect to the patients list page or perform any other action
                return RedirectToPage("Patients");
            }
            else
            {
                // Handle the error response
                return Page();
            }

        }
        private async Task LoadPatientsAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "api/patient");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Patients = JsonConvert.DeserializeObject<List<Patient>>(content);
            }
            else
            {
                // Handle the error
            }
        }
    }
}