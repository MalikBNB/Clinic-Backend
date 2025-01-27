using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.MedicalRecords;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1;
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MedicalRecordsController : BaseController
{
    public MedicalRecordsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        : base(unitOfWork, userManager, mapper)
    {
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = new PagedResult<MedicalRecordsResponseDto>();

        var medicalRecords = await _unitOfWork.MedicalRecords.GetAllAsync(["Appointment"]);
        if (medicalRecords.Count() == 0)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        foreach(var record in medicalRecords)
            result.Content.Add(_mapper.Map<MedicalRecordsResponseDto>(record));

        result.ResultCount = result.Content.Count;

        return Ok(result);
    }
    
    [HttpGet("{id}", Name = "MedicalRecord")]
    [AllowAnonymous]
    public async Task<IActionResult> FindAsync([FromQuery] string id)
    {
        var result = new Result<MedicalRecordsResponseDto>();

        var medicalRecord = await _unitOfWork.MedicalRecords.FindAsync(o => o.Id == new Guid(id), ["Appointment"]);
        if (medicalRecord is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        result.Content = _mapper.Map<MedicalRecordsResponseDto>(medicalRecord);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] MedicalRecordDto medicalRecordDto)
    {
        var result = new Result<MedicalRecordDto>();

        if (medicalRecordDto is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        medicalRecordDto.CreatorId = loggedInUser.Id;
        medicalRecordDto.ModifierId = loggedInUser.Id;
        medicalRecordDto.Created = DateTime.Now;
        medicalRecordDto.Modified = DateTime.Now;

        //var newMedicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);
        var newMedicalRecord = new MedicalRecord
        {
            VisitDescription = medicalRecordDto.VisitDescription,
            Diagnosis = medicalRecordDto.Diagnosis,
            Notes = medicalRecordDto.Notes,
            AppointmentId = new Guid(medicalRecordDto.AppointmentId),
            CreatorId = loggedInUser.Id,
            ModifierId = loggedInUser.Id,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        var isAdded = await _unitOfWork.MedicalRecords.AddAsync(newMedicalRecord);
        if (!isAdded)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        result.Content = medicalRecordDto;

        return CreatedAtRoute("MedicalRecord", new { id = newMedicalRecord.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] MedicalRecordDto medicalRecordDto)
    {
        var result = new Result<MedicalRecordDto>();

        if (medicalRecordDto is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        medicalRecordDto.Id = id;
        medicalRecordDto.ModifierId = loggedInUser.Id;
        medicalRecordDto.Modified = DateTime.Now;

        var isUpdated = await _unitOfWork.MedicalRecords.UpdateAsync(medicalRecordDto);
        if (!isUpdated)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = new Result<MedicalRecord>();

        var medicalRecord = await _unitOfWork.MedicalRecords.FindAsync(o => o.Id == new Guid(id));
        if(medicalRecord is null)
            return BadRequest(result.Error = PopulateError(400, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound));

        var isDeleted = await _unitOfWork.MedicalRecords.DeleteAsync(medicalRecord);
        if(!isDeleted)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        return Ok();
    }
}
