using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using referral_management_system_1.Data;
using referral_management_system_1.Models;

namespace referral_management_system_1.Controllers
{
    [Route("api/[controller]")]   //mappping request action method
    [ApiController]  //apply common api behaviors like validation & binding request to model
    public class SpecialtiesController : ControllerBase
    //manage http requests
    {
        private readonly AppDbContext _dbContext;

        public SpecialtiesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSpecialties()  //**return iaction result**
        {
            var specialties = _dbContext.Specialties.ToList();
            return Ok(specialties);
        }

        [HttpGet("{specialtyId}")]
        //****Generate Server produces response status  (for  testing controller functions)**** 
        //[ProducesResponseType(typeof(Specialties), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetSpecialtyById(int specialtyId)   //**return iaction result, 'specialtyId' is a parameter**
        {
            var specialty = _dbContext.Specialties.FirstOrDefault(s => s.SpecialityId == specialtyId);
            if (specialty == null)
                return NotFound();

            return Ok(specialty);
        }

        [HttpPost]
        public IActionResult CreateSpecialty([FromBody] Specialties specialty)
        {
            if (specialty == null)
                return BadRequest();

            _dbContext.Specialties.Add(specialty);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetSpecialtyById), new { specialtyId = specialty.SpecialityId }, specialty);
        }

        [HttpPut("{specialtyId}")]
        public IActionResult UpdateSpecialty(int specialtyId, [FromBody] Specialties updatedSpecialty)
        {
            //if (specialtyId != updatedSpecialty.SpecialityId)
            //    return BadRequest();   //***return bad request if specialty id is not equal to updated specialty id***

            var specialty = _dbContext.Specialties.FirstOrDefault(s => s.SpecialityId == specialtyId);
            if (specialty == null)
                return NotFound();

            specialty.Name = updatedSpecialty.Name;
            specialty.Description = updatedSpecialty.Description;
            specialty.PhoneNum = updatedSpecialty.PhoneNum;
            // Update other relevant specialty details

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{specialtyId}")]
        public IActionResult DeleteSpecialty(int specialtyId)
        {
            var specialty = _dbContext.Specialties.FirstOrDefault(s => s.SpecialityId == specialtyId);
            if (specialty == null)
                return NotFound();

            _dbContext.Specialties.Remove(specialty);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}