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
    public class ReferralModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IConfiguration _configuration;

        public List<Referrals> Referrals { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Specialties> Specialties { get; set; }



        public ReferralModel(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //   await LoadReferralsAsync();   ***** working code for show numbers.
        //     return Page();
        // }

        public async Task OnGet()
        {
            await LoadReferralsAsync();
            await LoadPatients();
            await LoadProviders();
            await LoadSpecialties();
        }


       //--create  referrals  --
        public async Task<IActionResult> OnPostCreateAsync(int patientId, int providerId, int specialtyId)
        {
            var referral = new Referrals
            {
                PatientId = patientId,
                ProviderId = providerId,
                SpecialtyId = specialtyId
            };

            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "api/referral", referral);

            if (response.IsSuccessStatusCode)
            {
                await LoadReferralsAsync();
                return RedirectToPage("Referral");
            }
            else
            {
                return Page();
            }
        }

        //--update  referrals  --
        public async Task<IActionResult> OnPostUpdateAsync(int referralId, int updatedPatientId, int updatedProviderId, int updatedSpecialtyId)
        {
            var updatedReferral = new Referrals
            {
                ReferralId = referralId,
                PatientId = updatedPatientId,
                ProviderId = updatedProviderId,
                SpecialtyId = updatedSpecialtyId
            };

            var response = await _httpClient.PutAsJsonAsync(_apiBaseUrl + $"api/referral/{referralId}", updatedReferral);

            if (response.IsSuccessStatusCode)
            {
                await LoadReferralsAsync();
                return RedirectToPage("Referral");
            }
            else
            {
                return Page();
            }
        }

        //--delete  referrals  --
        public async Task<IActionResult> OnPostDeleteAsync(int referralId)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"api/referral/{referralId}");

            if (response.IsSuccessStatusCode)
            {
                await LoadReferralsAsync(); // Refresh the referral list
                return RedirectToPage("Referral");
            }
            else
            {
                // Handle the error response
                return Page();
            }
        }

        private async Task LoadReferralsAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "api/referral");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Referrals = JsonConvert.DeserializeObject<List<Referrals>>(content);
            }
            else
            {
                // Handle the error
            }
        }


        //load Patient info
        private async Task LoadPatients()
        {
            var apiUrl = _configuration.GetValue<string>("ApiUrl");
            var endpoint = apiUrl + "/api/Patient";

            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Patients = JsonConvert.DeserializeObject<List<Patient>>(content);
            }
            else
            {
                Patients = new List<Patient>();
            }
        }

        public string GetPatientName(int patientId)
        {
            var patient = Patients.Find(p => p.PatientId == patientId);
            return patient != null ? patient.Name : "Unknown";
        }


        //load Specialty info
        private async Task LoadSpecialties()
        {
            var apiUrl = _configuration.GetValue<string>("ApiUrl");
            var endpoint = apiUrl + "/api/Specialties";

            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Specialties = JsonConvert.DeserializeObject<List<Specialties>>(content);
            }
            else
            {
                Specialties = new List<Specialties>();
            }
        }

        private async Task<string> GetSpecialtyName(int specialtyId)
        {
            var specialty = Specialties.Find(s => s.SpecialityId == specialtyId);
            return specialty != null ? specialty.Name : "Unknown";

        }




        //load Provider info
        private async Task LoadProviders()
        {
            var apiUrl = _configuration.GetValue<string>("ApiUrl");
            var endpoint = apiUrl + "/api/Provider";

            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Providers = JsonConvert.DeserializeObject<List<Provider>>(content);
            }
            else
            {
                Providers = new List<Provider>();
            }
        }
        private async Task<string> GetProviderName(int providerId)
        {
            var provider = Providers.Find(p => p.ProviderId == providerId);
            return provider != null ? provider.Name : "Unknown";

        }
    }
}
    
