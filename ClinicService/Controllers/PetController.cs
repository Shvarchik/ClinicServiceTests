using ClinicService.Models;
using ClinicService.Models.Requests;
using ClinicService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {

        [HttpPost("create")]
        [SwaggerOperation(OperationId = "PetCreate")]
        public ActionResult<int> Create([FromBody] CreatePetRequest createRequest)
        {
            if (createRequest.Birthday < DateTime.Now.AddYears(-25) ||
                createRequest.Name.Length < 3 || createRequest.ClientId <=0)
            {
                return BadRequest(0);
            }
            
            return Ok(1); 
            
        }

        [HttpPut("update")]
        [SwaggerOperation(OperationId = "PetUpdate")]
        public ActionResult<int> Update([FromBody] UpdatePetRequest updateRequest)
        {
            if (updateRequest.PetId <=0)
            {
                return NotFound(0);
            }
            if (updateRequest.Birthday < DateTime.Now.AddYears(-25) ||
                updateRequest.Name.Length < 3)
            {
                return BadRequest(0);
            }
                return Ok(1);
        }


        [HttpDelete("delete")]
        [SwaggerOperation(OperationId = "PetDelete")]
        public ActionResult<int> Delete(int petId)
        {
            if (petId <= 0)
                return BadRequest(0);
            return Ok(1);
        }

        [HttpGet("get-all")]
        [SwaggerOperation(OperationId = "PetGetAll")]
        public ActionResult<List<Pet>> GetAll(int clientId)
        {
            if (clientId <= 0)
                return BadRequest(0);
            return Ok(new List<Pet>());
        }

        [HttpGet("get-by-id")]
        [SwaggerOperation(OperationId = "PetGetById")]
        public ActionResult<Pet> GetById(int petId)
        {
            if (petId <= 0)
                return BadRequest(0);
            return Ok(new Pet());
        }
    }
}

       


