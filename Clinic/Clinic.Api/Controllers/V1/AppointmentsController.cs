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

            if (appointmentDto is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.Generic.InvalidPayload,
                                                                             ErrorMessages.Generic.BadRequest));

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            appointmentDto.status = AppointmentStatus.Confirmed;
            appointmentDto.CreatorId = loggedInUser.Id;
            appointmentDto.ModifierId = loggedInUser.Id;
            appointmentDto.Created = DateTime.Now;
            appointmentDto.Modified = DateTime.Now;

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            result.Content = appointmentDto;

            return CreatedAtRoute("Appointment", new { appointment.Id }, result);
        }

        [HttpGet("{id},{isForPatient}")]
        [Route("Patient")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(string id, bool isForPatient) // Get all appointments by patientId or doctorId
        {
            var result = new PagedResult<AppointmentResponseDto>();
            var appointments = new List<Appointment>();

            if (isForPatient)
                appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.PatientId == new Guid(id) && a.status == AppointmentStatus.Confirmed,
                                                                         ["Patient", "Doctor"]);
            else
                appointments = await _unitOfWork.Appointments.GetAllAsync(a => a.DoctorId == new Guid(id) && a.status == AppointmentStatus.Confirmed,
                                                                         ["Patient", "Doctor"]);

            if (appointments.Count == 0)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            foreach (var item in appointments)
                result.Content.Add(_mapper.Map<AppointmentResponseDto>(item));

            return Ok(result);

        }
        
        [HttpGet("{id}")]
        [Route("Appointment")]
        [AllowAnonymous]
        public async Task<IActionResult> FindAsync([FromQuery]string id)
        {
            var result = new Result<AppointmentResponseDto>();

            var appointment = await _unitOfWork.Appointments.FindAsync(new Guid(id), ["Patient", "Doctor"]);

            if (appointment is null) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            result.Content = _mapper.Map<AppointmentResponseDto>(appointment);

            return Ok(result);

        }

        [HttpPut]
        [Route("Appointment/Reschedule")]
        public async Task<IActionResult> RescheduleAsync([FromBody] AppointmentDto appointmentDto)
        {
            var result = new Result<AppointmentDto>();

            if (appointmentDto is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.Generic.InvalidPayload,
                                                                             ErrorMessages.Generic.BadRequest));

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            var oldAppointment = await _unitOfWork.Appointments.FindAsync(a => a.Id == new Guid(appointmentDto.Id)
                                                                       && a.status == AppointmentStatus.Confirmed 
                                                                       || a.status == AppointmentStatus.NoShow);

            oldAppointment.Date = appointmentDto.Date;
            oldAppointment.status = AppointmentStatus.Rescheduled;
            oldAppointment.ModifierId = loggedInUser.Id;
            oldAppointment.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Route("Appointment/Cancel")]
        public async Task<IActionResult> CancelAsync([FromQuery] string id)
        {
            var result = new Result<AppointmentDto>();

            var appointment = await _unitOfWork.Appointments.FindAsync(a => a.Id == new Guid(id));
            if (appointment is null) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));
            
            if (appointment.status == AppointmentStatus.Canceled) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.BadRequest,
                                                               ErrorMessages.Appointment.AlreadyCanceled));
            
            if (appointment.status == AppointmentStatus.Completed) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.BadRequest,
                                                               ErrorMessages.Appointment.CannotCancel));

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            appointment.status = AppointmentStatus.Canceled;
            appointment.ModifierId = loggedInUser.Id;
            appointment.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPut("{ids}")]
        [Route("Appointment/NoShow")]
        public async Task<IActionResult> SetToNoShowAsync(List<string> ids)
        {
            var result = new Result<AppointmentDto>();

            var appointments = await _unitOfWork.Appointments.GetAllAsync(a => ((IEnumerable<Guid>)ids).Contains(a.Id), null, true);
            if (appointments.Count == 0)
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.ObjectNotFound,
                                                               ErrorMessages.Generic.BadRequest));

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            foreach (var item in appointments)
            {
                if (item.Date > DateTime.Now && item.status == AppointmentStatus.Confirmed)
                {
                    item.status = AppointmentStatus.NoShow;
                    item.ModifierId = loggedInUser.Id;
                    item.Modified = DateTime.Now;
                }
            }

            await _unitOfWork.CompleteAsync();

            return Ok();
        }


    }
}
