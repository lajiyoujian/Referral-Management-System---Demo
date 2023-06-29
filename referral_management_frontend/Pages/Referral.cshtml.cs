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

        public List<Referrals> Referrals { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Specialties> Specialties { get; set; }


        public ReferralModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadReferralsAsync();
            await LoadPatientsAsync();
            await LoadProvidersAsync();
            await LoadSpecialtiesAsync();
            return Page();
        }

        //--create  referrals  --
        public async Task<IActionResult> OnPostCreateAsync(string patient, string provider, string specialty)
        {
            var referral = new Referrals
            {
                Patient = patient,
                Provider = provider,
                Specialty = specialty
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
        public async Task<IActionResult> OnPostUpdateAsync(int referralId, string updatedPatient, string updatedProvider, string updatedSpecialty)
        {
            var updatedReferral = new Referrals
            {
                ReferralId = referralId,
                Patient = updatedPatient,
                Provider = updatedProvider,
                Specialty = updatedSpecialty
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