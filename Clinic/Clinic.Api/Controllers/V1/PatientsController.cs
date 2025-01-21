using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Patients;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : BaseController
    {

        public PatientsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            : base(unitOfWork, userManager, mapper)
        {

        }


        [HttpPost]
        [Route("Patient")]
        public async Task<IActionResult> AddAsync([FromBody] PatientDto patientDto)
        {
            var result = new Result<PatientDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                     ErrorMessages.Profile.UserNotFound,
                                                                                     ErrorMessages.Generic.BadRequest));

            var patientUser = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (patientUser is not null) return BadRequest(result.Error = PopulateError(400,
                                                                                        ErrorMessages.User.PatientAlreadyExist,
                                                                                        ErrorMessages.Generic.InvalidRequest));

            //var mappedUser = _mapper.Map<User>(patientDto);

            var patient = new Patient
            {
                UserId = patientUser.Id,
                //User = patientUser
            };

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            result.Content = patientDto;

            return CreatedAtRoute("Patient", new { patient.Id }, result);
        }

        [HttpGet("{id}")]
        [Route("Patient")]
        public async Task<IActionResult> GetPatient([FromQuery] Guid id)
        {
            var result = new Result<ProfileDto>();

            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient is null) return BadRequest(result.Error = PopulateError(404,
                                             ErrorMessages.User.UserNotFound,
                                             ErrorMessages.Generic.ObjectNotFound));

            var mappedProfile = _mapper.Map<ProfileDto>(patient);

            result.Content = mappedProfile;

            return Ok(result);
        }
    }
}
