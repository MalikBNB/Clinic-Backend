using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Clinic.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DoctorsController : BaseController
    {
        public DoctorsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper) 
            : base(unitOfWork, userManager, mapper)
        {           
        }

        [HttpPost]
        [Route("Doctor")]
        public async Task<IActionResult> AddAsync([FromBody] DoctorDto doctorDto)
        {
            var result = new Result<DoctorDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if(loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Profile.UserNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var userDoctor = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id)) ;
            if (userDoctor is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.InvalidRequest));

            var doctorExists = await _unitOfWork.Doctors.GetByUserIdAsync(userDoctor.Id) is not null;
            if (doctorExists) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.DoctorAlreadyExist,
                                                                             ErrorMessages.Generic.InvalidRequest));

            var user = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            var newDoctor = new Doctor
            {
                UserId = user.Id,
                User = user,
                Specialization = doctorDto.Specialization
            };
            
            await _unitOfWork.Doctors.AddAsync(newDoctor);
            await _unitOfWork.CompleteAsync();

            var mappedDoctor = _mapper.Map<DoctorDto>(user);
            mappedDoctor.Specialization = doctorDto.Specialization;
            result.Content = mappedDoctor;

            return CreatedAtRoute("Doctor", new {user.Id}, result);
        }

        [HttpGet]
        [Route("Doctors")]
        public async Task<IActionResult> GetAllAsync()
        {
            var pagedResult = new PagedResult<Doctor>();
            var doctors = await _unitOfWork.Doctors.GetAllAsync();

            if (doctors.Count() == 0)
            {
                pagedResult.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.BadRequest);
                return BadRequest(pagedResult);
            }

            pagedResult.Content = doctors.ToList();
            pagedResult.ResultCount = doctors.Count();

            return Ok(pagedResult);
        }

        [HttpGet("{id}", Name = "Doctor")]
        public async Task<IActionResult> GetDoctor([FromQuery]Guid id)
        {
            var result = new Result<DoctorProfileDto>();

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if(doctor is null) return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<DoctorProfileDto>(doctor);

            result.Content = mappedProfile;
            return Ok(result);
        }

    }
}
