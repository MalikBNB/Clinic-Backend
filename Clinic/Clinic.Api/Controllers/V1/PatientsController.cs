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


        //[HttpPost]
        //public async Task<IActionResult> AddAsync([FromBody] PatientDto patientDto)
        //{
        //    var result = new Result<PatientDto>();

        //    if (patientDto is null) return BadRequest(result.Error = PopulateError(400,
        //                                                                            ErrorMessages.Generic.InvalidPayload,
        //                                                                            ErrorMessages.Generic.BadRequest));

        //    var loggedInUser = await GetLoggedInUserAsync();
        //    if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
        //                                                                     ErrorMessages.User.UserNotFound,
        //                                                                     ErrorMessages.Generic.ObjectNotFound));

        //    var patientExists = await _unitOfWork.Patients.GetByEmailsync(patientDto.Email) is not null;
        //    if (patientExists) return BadRequest(result.Error = PopulateError(400,
        //                                                                      ErrorMessages.User.DoctorAlreadyExist,
        //                                                                      ErrorMessages.Generic.InvalidRequest));

        //    var newPatient = _mapper.Map<Patient>(patientDto);
        //    //newPatient.CreatorId = loggedInUser.Id;
        //    //newPatient.ModifierId = loggedInUser.Id;
        //    newPatient.Created = DateTime.Now;
        //    newPatient.Modified = DateTime.Now;

        //    await _unitOfWork.Patients.AddAsync(newPatient);
        //    await _unitOfWork.CompleteAsync();

        //    result.Content = patientDto;

        //    return CreatedAtRoute("Patient", new { newPatient.Id }, result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var result = new Result<ProfileDto>();

            var patient = await _unitOfWork.Patients.FindAsync(p => p.Id == new Guid(id) && p.Status == 1);
            if (patient is null) return BadRequest(result.Error = PopulateError(404,
                                                                                ErrorMessages.User.UserNotFound,
                                                                                ErrorMessages.Generic.ObjectNotFound));

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = new Result<User>();

            var patient = await _unitOfWork.Patients.FindAsync(p => p.Id == new Guid(id) && p.Status == 1);
            if (patient is null) return BadRequest(result.Error = PopulateError(400,
                                                                        ErrorMessages.User.UserNotFound,
                                                                        ErrorMessages.Generic.ObjectNotFound));

            var isDeleted = await _unitOfWork.Patients.DeleteAsync(new Guid(id));
            if (!isDeleted) 
                return BadRequest(result.Error = PopulateError(400,
                                                                        ErrorMessages.Generic.SomethingWentWrong,
                                                                        ErrorMessages.Generic.InvalidRequest));
            
            await _unitOfWork.CompleteAsync();

            return Ok();

        }
    }
}
