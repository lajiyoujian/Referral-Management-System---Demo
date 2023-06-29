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
    public class ReferralController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReferralController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllReferrals()
        {
            var referrals = _dbContext.Referrals.ToList();
            return Ok(referrals);
        }

        [HttpGet("{referralId}")]
        public IActionResult GetReferralById(int referralId)
        {
            var referrals = _dbContext.Referrals.FirstOrDefault(r => r.ReferralId == referralId);
            if (referrals == null)
                return NotFound();

            return Ok(referrals);
        }

        [HttpPost]
        public IActionResult CreateReferral([FromBody] Referrals referrals)
        {
            if (referrals == null)
                return BadRequest();

            _dbContext.Referrals.Add(referrals);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetReferralById), new { referralId = referrals.ReferralId }, referrals);
        }

        [HttpPut("{referralId}")]
        public IActionResult UpdateReferral(int referralId, [FromBody] Referrals updatedReferral)
        {
            var referrals = _dbContext.Referrals.FirstOrDefault(r => r.ReferralId == referralId);
            if (referrals == null)
                return NotFound();

            referrals.PatientId = updatedReferral.PatientId;
            referrals.ProviderId = updatedReferral.ProviderId;
            referrals.SpecialtyId = updatedReferral.SpecialtyId;
            // Update other relevant referral details

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{referralId}")]
        public IActionResult DeleteReferral(int referralId)
        {
            var referral = _dbContext.Referrals.FirstOrDefault(r => r.ReferralId == referralId);
            if (referral == null)
                return NotFound();

            _dbContext.Referrals.Remove(referral);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}