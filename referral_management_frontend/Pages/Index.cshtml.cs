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
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

      //-- get total numbers --
        public List<Specialties> Specialties { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Referrals> Referrals { get; set; }

        public IndexModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = "http://localhost:5045/";
            //_apiBaseUrl = configuration["APIBaseUrl"]; // Retrieve the base URL from configuration
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "api/Specialties");
            var response2 = await _httpClient.GetAsync(_apiBaseUrl + "api/patient");
            var response3 = await _httpClient.GetAsync(_apiBaseUrl + "api/provider");
            var response4 = await _httpClient.GetAsync(_apiBaseUrl + "api/referral");

            if (response.IsSuccessStatusCode)
            {
                //-- get  specialties number --
                var content = await response.Content.ReadAsStringAsync();
                Specialties = JsonConvert.DeserializeObject<List<Specialties>>(content);
                //-- get  patients number --
                var content2 = await response2.Content.ReadAsStringAsync();
                Patients = JsonConvert.DeserializeObject<List<Patient>>(content2);
                //-- get  provider number --
                var content3 = await response3.Content.ReadAsStringAsync();
                Providers = JsonConvert.DeserializeObject<List<Provider>>(content3);
                //-- get  referral number --
                var content4 = await response4.Content.ReadAsStringAsync();
                Referrals = JsonConvert.DeserializeObject<List<Referrals>>(content4);
            }
            else
            {
                // Handle the error
            }
        }



    }
}
