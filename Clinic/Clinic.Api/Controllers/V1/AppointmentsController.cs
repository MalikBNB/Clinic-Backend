using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.Data;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Appointments;
using Clinic.Entities.DTOs.Incoming.Patients;
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
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            : base(unitOfWork, userManager, mapper)
        {

        }


        [HttpPost]
        [Route("Appointment")]
        public async Task<IActionResult> AddAsync([FromBody] AppointmentDto appointmentDto)
        {
            var result = new Result<AppointmentDto>();

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                                     ErrorMessages.User.UserNotFound,
                                                                                     ErrorMessages.Generic.InvalidRequest));

            var user = await _unitOfWork.Users.GetByIdentityIdAsync(new Guid(loggedInUser.Id));
            if (user is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.InvalidRequest));

            appointmentDto.CreatorId = user.Id;
            appointmentDto.ModifierId = user.Id;
            appointmentDto.Created = DateTime.Now;
            appointmentDto.Modified = DateTime.Now;

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            result.Content = appointmentDto;

            return CreatedAtRoute("Appointment", new { appointment.Id }, result);
        }

        [HttpGet("{id}, {isForPatient}")]
        public async Task<IActionResult> GetAppointmentAsync(Guid id, bool isForPatient)
        {
            var result = new PagedResult<AppointmentResponseDto>();
            var appointments = new List<Appointment>();
            if (isForPatient)
                appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.PatientId == id && a.status == AppointmentStatus.Confirmed,
                                                                         ["Patient", "Doctor"]);
            else
                appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.DoctorId == id && a.status == AppointmentStatus.Confirmed,
                                                                         ["Patient", "Doctor", "User"]);

            foreach (var item in appointments)
                result.Content.Add(_mapper.Map<AppointmentResponseDto>(item));

            return Ok(result);

        }
        
        [HttpGet("{id}")]
        [Route("Appointment")]
        public async Task<IActionResult> FindAsync([FromQuery]Guid id)
        {
            var result = new Result<AppointmentResponseDto>();

            var appointment = await _unitOfWork.Appointments.FindAsync(id, ["Patient", "Doctor"]);

            result.Content = _mapper.Map<AppointmentResponseDto>(appointment);

            return Ok(result);

        }
    }
}
