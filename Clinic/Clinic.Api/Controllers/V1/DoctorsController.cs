using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpPost("Doctor")]
        //public async Task<IActionResult> AddAsync([FromBody] DoctorDto doctorDto)
        //{
        //    var result = new Result<DoctorDto>();

        //    if (doctorDto is null) return BadRequest(result.Error = PopulateError(400,
        //                                                       ErrorMessages.Generic.InvalidPayload,
        //                                                       ErrorMessages.Generic.BadRequest));

        //    var loggedInUser = await GetLoggedInUserAsync();
        //    if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
        //                                                                     ErrorMessages.User.UserNotFound,
        //                                                                     ErrorMessages.Generic.ObjectNotFound));

        //    var doctorExists = await _unitOfWork.Doctors.GetByEmailsync(doctorDto.Email) is not null;
        //    if (doctorExists) return BadRequest(result.Error = PopulateError(400,
        //                                                                     ErrorMessages.User.DoctorAlreadyExist,
        //                                                                     ErrorMessages.Generic.InvalidRequest));

        //    var newDoctor = _mapper.Map<Doctor>(doctorDto);
        //    //newDoctor.CreatorId = loggedInUser.Id;
        //    //newDoctor.ModifierId = loggedInUser.Id;
        //    newDoctor.Created = DateTime.Now;
        //    newDoctor.Modified = DateTime.Now;
            
        //    await _unitOfWork.Doctors.AddAsync(newDoctor);
        //    await _unitOfWork.CompleteAsync();

        //    result.Content = doctorDto;

        //    return CreatedAtRoute("Doctor", new { newDoctor.Id}, result);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var pagedResult = new PagedResult<Doctor>();
            var doctors = await _unitOfWork.Doctors.GetAllAsync();

            if (!doctors.Any())
            {
                pagedResult.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.BadRequest);
                return BadRequest(pagedResult);
            }

            pagedResult.Content = doctors.ToList();
            pagedResult.ResultCount = doctors.Count();

            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(string id)
        {
            var result = new Result<DoctorProfileDto>();

            var doctor = await _unitOfWork.Doctors.FindAsync(d => d.Id == new Guid(id) && d.Status == 1);
            if(doctor is null) return BadRequest(result.Error = PopulateError(404,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var mappedProfile = _mapper.Map<DoctorProfileDto>(doctor);

            result.Content = mappedProfile;
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = new Result<User>();

            var isDeleted = await _unitOfWork.Doctors.DeleteAsync(new Guid(id));
            if (!isDeleted) return BadRequest(result.Error = PopulateError(400,
                                                                        ErrorMessages.Generic.SomethingWentWrong,
                                                                        ErrorMessages.Generic.InvalidRequest));

            
            await _unitOfWork.CompleteAsync();

            return Ok();

        }

    }
}
