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
        public ProfilesController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper) 
            : base(unitOfWork, userManager, mapper)
        { 
        }


        [HttpGet("User/{id:guid}")]
        public async Task<IActionResult> GetUserProfile(Guid id)
        {
            var result = new Result<ProfileDto>();

            var profile = await _unitOfWork.Users.GetByIdAsync(id);
            if (profile is null) return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(profile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet("Doctor/{id:guid}")]
        public async Task<IActionResult> GetDoctorProfile(Guid id)
        {
            var result = new Result<DoctorProfileDto>();

            var doctorProfile = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctorProfile is null) return BadRequest(result.Error = PopulateError(404,
                                                              ErrorMessages.Profile.UserNotFound,
                                                              ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<DoctorProfileDto>(doctorProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpGet("Patient/{id:guid}")]
        public async Task<IActionResult> GetPatientProfile(Guid id)
        {
            var result = new Result<ProfileDto>();

            var patientProfile = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patientProfile is null) return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<ProfileDto>(patientProfile);
            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpPut("User/{id:guid}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileDto profileDto)
        {
            var result = new Result<UpdateProfileDto>();

            if (!ModelState.IsValid)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.InvalidPayload,
                                                               ErrorMessages.Generic.BadRequest));

            //var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            //if (loggedInUser is null) 
            //    return BadRequest(result.Error = PopulateError(400,
            //                                                   ErrorMessages.Profile.UserNotFound,
            //                                                   ErrorMessages.Generic.BadRequest));

            var user = await _unitOfWork.Users.FindAsync(a => a.Id == id);
            if (user is null)
                return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            user.DateOfBirth = profileDto.DateOfBirth;
            user.Gendor = profileDto.Gendor;
            user.Address = profileDto.Address;
            user.Phone = profileDto.Phone;
            user.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            result.Content = profileDto;

            return Ok(result);
        }

        [HttpPut("Patient/{id:guid}")]
        public async Task<IActionResult> UpdatePatientProfile(Guid id, [FromBody] UpdateProfileDto profileDto)
        {
            var result = new Result<UpdateProfileDto>();

            if (!ModelState.IsValid)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.InvalidPayload,
                                                               ErrorMessages.Generic.BadRequest));

            var patient = await _unitOfWork.Patients.FindAsync(a => a.Id == id);
            if (patient is null)
                return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            patient.DateOfBirth = profileDto.DateOfBirth;
            patient.Gendor = profileDto.Gendor;
            patient.Address = profileDto.Address;
            patient.Phone = profileDto.Phone;
            patient.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            result.Content = profileDto;

            return Ok(result);
        }

        [HttpPut(("Doctor/{id:guid}"))]
        public async Task<IActionResult> UpdateDoctorProfile(Guid id, [FromBody] UpdateDoctorProfileDto doctorProfileDto)
        {
            var result = new Result<UpdateDoctorProfileDto>();

            if (!ModelState.IsValid)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.InvalidPayload,
                                                               ErrorMessages.Generic.BadRequest));

            var doctor = await _unitOfWork.Doctors.FindAsync(a => a.Id == id);
            if (doctor is null)
                return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            doctor.Specialization = doctorProfileDto.Sepecialization;
            doctor.DateOfBirth = doctorProfileDto.DateOfBirth;
            doctor.Gendor = doctorProfileDto.Gendor;
            doctor.Address = doctorProfileDto.Address;
            doctor.Phone = doctorProfileDto.Phone;
            doctor.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            result.Content = doctorProfileDto;

            return Ok(result);

        }
    }
}
