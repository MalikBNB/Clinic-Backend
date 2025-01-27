using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfilesController : BaseController
    {
        public ProfilesController(IUnitOfWork unitOfWork,
                                  UserManager<IdentityUser> userManager,
                                  IMapper mapper) : base(unitOfWork, userManager, mapper)
        { }


        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUserProfile([FromQuery] string id)
        {
            var result = new Result<ProfileDto>();

            var profile = await _unitOfWork.Users.GetByIdAsync(new Guid(id));
            if (profile is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(profile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet("Doctor/{id}")]
        public async Task<IActionResult> GetDoctorProfile([FromQuery] string id)
        {
            var result = new Result<DoctorProfileDto>();

            var doctorProfile = await _unitOfWork.Doctors.GetByIdAsync(new Guid(id));
            if (doctorProfile is null) return BadRequest(result.Error = PopulateError(400,
                                                              ErrorMessages.Profile.UserNotFound,
                                                              ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<DoctorProfileDto>(doctorProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetPatientProfile([FromQuery] string id)
        {
            var result = new Result<ProfileDto>();

            var patientProfile = await _unitOfWork.Patients.GetByIdAsync(new Guid(id));
            if (patientProfile is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(patientProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpPut("User/{id}")]
        public async Task<IActionResult> UpdateProfile([FromQuery]string id, [FromBody] UpdateProfileDto profileDto)
        {
            var result = new Result<UpdateProfileDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            if (profileDto is null) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.InvalidPayload,
                                                               ErrorMessages.Generic.BadRequest));

            profileDto.ModifierId = loggedInUser.Id;
            profileDto.Modified = DateTime.Now;

            var isUpdated = await _unitOfWork.Users.UpdateAsync(new Guid(id), profileDto);
            if (!isUpdated) return BadRequest(result.Error = PopulateError(500,
                                                                          ErrorMessages.Generic.SomethingWentWrong,
                                                                          ErrorMessages.Generic.UnableToProcess));
            await _unitOfWork.CompleteAsync();

            result.Content = profileDto;

            return Ok(result);
        }

        [HttpPut(("Doctor/{id}"))]
        public async Task<IActionResult> UpdateDoctorProfile([FromQuery] string id, [FromBody] UpdateDoctorProfileDto doctorProfileDto)
        {
            var result = new Result<UpdateDoctorProfileDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Profile.UserNotFound,
                                                                                   ErrorMessages.Generic.BadRequest));

            if (doctorProfileDto is null)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.InvalidPayload,
                                                               ErrorMessages.Generic.BadRequest));

            doctorProfileDto.Id = id;
            doctorProfileDto.ModifierId = loggedInUser.Id;
            doctorProfileDto.Modified = DateTime.Now;

            var isUpdated = await _unitOfWork.Doctors.UpdateAsync(doctorProfileDto);
            if (!isUpdated) return BadRequest(result.Error = PopulateError(500,
                                                                          ErrorMessages.Generic.SomethingWentWrong,
                                                                          ErrorMessages.Generic.UnableToProcess));
            await _unitOfWork.CompleteAsync();

            result.Content = doctorProfileDto;

            return Ok(result);

        }
    }
}
