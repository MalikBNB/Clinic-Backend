using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Incoming.Patients;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clinic.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientsController : BaseController
    {

        public PatientsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            : base(unitOfWork, userManager, mapper)
        {

        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = new Result<ProfileDto>();

            var patient = await _unitOfWork.Patients.FindAsync(p => p.Id == id && p.Status == 1);
            if (patient is null)
            {
                result.Error = PopulateError(404, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
                return BadRequest(result);
            }

            var mappedProfile = _mapper.Map<ProfileDto>(patient);

            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var pagedResult = new PagedResult<Patient>();

            var patients = await _unitOfWork.Patients.GetAllAsync();
            if (!patients.Any())
            {
                pagedResult.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.BadRequest);
                return BadRequest(pagedResult);
            }

            pagedResult.Content = patients.ToList();
            pagedResult.ResultCount = patients.Count();

            return Ok(pagedResult);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = new Result<Patient>();

            var patient = await _unitOfWork.Patients.FindAsync(p => p.Id == id && p.Status == 1);
            if (patient is null)
            {
                result.Error = PopulateError(400, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
                return BadRequest(result);
            }

            var isDeleted = await _unitOfWork.Patients.DeleteAsync(patient);
            if (!isDeleted)
            {
                result.Error = PopulateError(500, ErrorMessages.Generic.SomethingWentWrong, ErrorMessages.Generic.InvalidRequest);
                return BadRequest(result);
            }
            
            await _unitOfWork.CompleteAsync();

            return Ok();

        }
    }
}
