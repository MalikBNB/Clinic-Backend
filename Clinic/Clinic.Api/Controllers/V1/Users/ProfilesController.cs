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


        [HttpGet]
        [Route("User")]
        public async Task<IActionResult> GetProfile()
        {
            var result = new Result<ProfileDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var profile = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (profile is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(profile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet]
        [Route("Doctor")]
        public async Task<IActionResult> GetDoctorProfile()
        {
            var result = new Result<DoctorProfileDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var doctorProfile = await _unitOfWork.Doctors.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (doctorProfile is null) return BadRequest(result.Error = PopulateError(400,
                                                              ErrorMessages.Profile.UserNotFound,
                                                              ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<DoctorProfileDto>(doctorProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet]
        [Route("Patient")]
        public async Task<IActionResult> GetPatientProfile()
        {
            var result = new Result<ProfileDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var patientProfile = await _unitOfWork.Patients.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (patientProfile is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(patientProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpPut]
        [Route("User")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto profileDto)
        {
            var result = new Result<ProfileDto>();

            if (!ModelState.IsValid) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Generic.InvalidPayload,
                                                                                   ErrorMessages.Generic.BadRequest));

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Profile.UserNotFound,
                                                                                   ErrorMessages.Generic.BadRequest));

            var oldUser = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (oldUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Profile.UserNotFound,
                                                                                   ErrorMessages.Generic.BadRequest));

            oldUser.Gendor = profileDto.Gendor;
            oldUser.Address = profileDto.Address;
            oldUser.Phone = profileDto.Phone;

            var isUpdated = await _unitOfWork.Users.UpdateAsync(oldUser);
            if (!isUpdated) return BadRequest(result.Error = PopulateError(500,
                                                                          ErrorMessages.Generic.SomethingWentWrong,
                                                                          ErrorMessages.Generic.UnableToProcess));
            await _unitOfWork.CompleteAsync();

            var mappedProfile = _mapper.Map<ProfileDto>(oldUser);

            result.Content = mappedProfile;
            return Ok(result);
        }

        [HttpPut]
        [Route("Doctor")]
        public async Task<IActionResult> UpdateDoctorProfile([FromBody] UpdateDoctorProfileDto doctorProfileDto)
        {
            var result = new Result<DoctorProfileDto>();

            if (!ModelState.IsValid) return BadRequest(result.Error = PopulateError(400,
                                                                                    ErrorMessages.Generic.InvalidPayload,
                                                                                    ErrorMessages.Generic.BadRequest));

            var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Profile.UserNotFound,
                                                                                   ErrorMessages.Generic.BadRequest));

            var oldDoctor = await _unitOfWork.Doctors.GetByIdentityIdAsync(new Guid(loggedUser.Id));
            if (oldDoctor is null) return BadRequest(result.Error = PopulateError(400,
                                                                                   ErrorMessages.Profile.DoctorNotFound,
                                                                                   ErrorMessages.Generic.BadRequest));

            oldDoctor.User.Gendor = doctorProfileDto.Gendor;
            oldDoctor.User.Address = doctorProfileDto.Address;
            oldDoctor.User.Phone = doctorProfileDto.Phone;
            oldDoctor.Specialization = doctorProfileDto.Sepecialization;

            //var isUpdated = await _unitOfWork.Doctors.UpdateAsync(oldDoctor);
            //if (!isUpdated) return BadRequest(result.Error = PopulateError(500,
            //                                                              ErrorMessages.Generic.SomethingWentWrong,
            //                                                              ErrorMessages.Generic.UnableToProcess));
            await _unitOfWork.CompleteAsync();

            var mappedProfile = _mapper.Map<DoctorProfileDto>(oldDoctor);
            result.Content = mappedProfile;

            return Ok(result);

        }
    }
}
