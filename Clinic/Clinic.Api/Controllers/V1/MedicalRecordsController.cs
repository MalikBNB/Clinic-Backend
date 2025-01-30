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
using Microsoft.VisualBasic;

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
        //if (!medicalRecords.Any())
        //    return BadRequest(result.Error = PopulateError(404,
        //                                                   ErrorMessages.Generic.ObjectNotFound,
        //                                                   ErrorMessages.Generic.ObjectNotFound));

        foreach (var record in medicalRecords)
            result.Content.Add(_mapper.Map<MedicalRecordsResponseDto>(record));

        result.ResultCount = result.Content.Count;

        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "MedicalRecord")]
    [AllowAnonymous]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<MedicalRecordsResponseDto>();

        var medicalRecord = await _unitOfWork.MedicalRecords.FindAsync(o => o.Id == id, ["Appointment"]);
        if (medicalRecord is null)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        result.Content = _mapper.Map<MedicalRecordsResponseDto>(medicalRecord);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] MedicalRecordDto medicalRecordDto)
    {
        var result = new Result<MedicalRecordDto>();

        if (!ModelState.IsValid)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        var newMedicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDto);
        newMedicalRecord.CreatorId = loggedInUser.Id;
        newMedicalRecord.ModifierId = loggedInUser.Id;
        newMedicalRecord.Created = DateTime.Now;
        newMedicalRecord.Modified = DateTime.Now;

        var isAdded = await _unitOfWork.MedicalRecords.AddAsync(newMedicalRecord);
        if (!isAdded)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        result.Content = medicalRecordDto;

        return CreatedAtRoute("MedicalRecord", new { id = newMedicalRecord.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MedicalRecordDto medicalRecordDto)
    {
        var result = new Result<MedicalRecordDto>();

        if (!ModelState.IsValid)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        var medicalRecord = await _unitOfWork.MedicalRecords.FindAsync(mr => mr.Id == id);
        if (medicalRecord is null)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        medicalRecord.VisitDescription = medicalRecordDto.VisitDescription;
        medicalRecord.Diagnosis = medicalRecordDto.Diagnosis;
        medicalRecord.Notes = medicalRecordDto.Notes;
        medicalRecord.ModifierId = loggedInUser.Id;
        medicalRecord.Modified = DateTime.Now;

        await _unitOfWork.CompleteAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = new Result<MedicalRecord>();

        var medicalRecord = await _unitOfWork.MedicalRecords.FindAsync(o => o.Id == id);
        if (medicalRecord is null)
            return BadRequest(result.Error = PopulateError(400, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        var isDeleted = await _unitOfWork.MedicalRecords.DeleteAsync(medicalRecord);
        if (!isDeleted)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        return Ok();
    }
}
