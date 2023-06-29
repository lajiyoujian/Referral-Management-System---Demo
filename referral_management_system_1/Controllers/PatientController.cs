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
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PatientController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            var patients = _dbContext.Patients.ToList();
            return Ok(patients);
        }


        [HttpGet("{patientId}")]
        public IActionResult GetPatientById(int patientId)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public IActionResult CreatePatient([FromBody] Patient patient)
        {
            if (patient == null)
                return BadRequest();

            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetPatientById), new { patientId = patient.PatientId }, patient);
        }

        [HttpPut("{patientId}")]
        public IActionResult UpdatePatient(int patientId, [FromBody] Patient updatedPatient)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
                return NotFound();

            patient.Name = updatedPatient.Name;
            patient.PhoneNum = updatedPatient.PhoneNum;
            patient.DateIn = updatedPatient.DateIn;
            // Update other relevant patient details

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{patientId}")]
        public IActionResult DeletePatient(int patientId)
        {
            var patient = _dbContext.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
                return NotFound();

            _dbContext.Patients.Remove(patient);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}