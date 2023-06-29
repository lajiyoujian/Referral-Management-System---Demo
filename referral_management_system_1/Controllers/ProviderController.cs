using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using referral_management_system_1.Data;
using referral_management_system_1.Models;
using System;
using System.Linq;


namespace referral_management_system_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProviderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProviders()
        {
            var providers = _dbContext.Providers.ToList();
            return Ok(providers);
        }

        [HttpGet("{providerId}")]
        public IActionResult GetProviderById(int providerId)
        {
            var provider = _dbContext.Providers.FirstOrDefault(p => p.ProviderId == providerId);
            if (provider == null)
                return NotFound();

            return Ok(provider);
        }

        [HttpPost]
        public IActionResult CreateProvider([FromBody] Provider provider)
        {
            if (provider == null)
                return BadRequest();

            _dbContext.Providers.Add(provider);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProviderById), new { providerId = provider.ProviderId }, provider);
        }

        [HttpPut("{providerId}")]
        public IActionResult UpdateProvider(int providerId, [FromBody] Provider updatedProvider)
        {
            var provider = _dbContext.Providers.FirstOrDefault(p => p.ProviderId == providerId);
            if (provider == null)
                return NotFound();

            provider.Name = updatedProvider.Name;
            provider.Address = updatedProvider.Address;
            provider.PhoneNum = updatedProvider.PhoneNum;
            // Update other relevant provider details

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{providerId}")]
        public IActionResult DeleteProvider(int providerId)
        {
            var provider = _dbContext.Providers.FirstOrDefault(p => p.ProviderId == providerId);
            if (provider == null)
                return NotFound();

            _dbContext.Providers.Remove(provider);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}