using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.MedicalRecords;
using Clinic.Entities.DTOs.Incoming.Prescriptions;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1;
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PrescriptionsController : BaseController
{
    public PrescriptionsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        : base(unitOfWork, userManager, mapper)
    {
    }


    [HttpGet("{medicalRecordId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(string medicalRecordId)
    {
        var result = new PagedResult<PrescriptionsDto>();

        var prescriptions = await _unitOfWork.Prescriptions.GetAllAsync(p => p.MedicalRecordId == new Guid(medicalRecordId));
        if (prescriptions.Count() == 0)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        foreach (var prescription in prescriptions)
            result.Content.Add(_mapper.Map<PrescriptionsDto>(prescription));

        result.ResultCount = result.Content.Count;

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = new PagedResult<PrescriptionsDto>();

        var prescriptions = await _unitOfWork.Prescriptions.GetAllAsync();
        if (prescriptions.Count() == 0)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        foreach (var prescription in prescriptions)
            result.Content.Add(_mapper.Map<PrescriptionsDto>(prescription));

        result.ResultCount = result.Content.Count;

        return Ok(result);
    }

    [HttpGet("{id}", Name = "Prescription")]
    [AllowAnonymous]
    public async Task<IActionResult> FindAsync(string id)
    {
        var result = new Result<PrescriptionsDto>();

        var prescription = await _unitOfWork.Prescriptions.FindAsync(p => p.Id == new Guid(id), ["MedicalRecord"]);
        if (prescription is null)
            return BadRequest(result.Error = PopulateError(404,
                                                           ErrorMessages.Generic.ObjectNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        result.Content = _mapper.Map<PrescriptionsDto>(prescription);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PrescriptionsDto prescriptionsDto)
    {
        var result = new Result<PrescriptionsDto>();

        if (prescriptionsDto is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        //prescriptionsDto.CreatorId = loggedInUser.Id;
        //prescriptionsDto.ModifierId = loggedInUser.Id;
        //prescriptionsDto.Created = DateTime.Now;
        //prescriptionsDto.Modified = DateTime.Now;

        //var newPrescription = _mapper.Map<Prescription>(prescriptionsDto);

        var newPrescription = new Prescription
        {
            MedicalRecordId = new Guid(prescriptionsDto.MedicalRecordId),
            Medication = prescriptionsDto.Medication,
            Dosage = prescriptionsDto.Dosage,
            Frequency = prescriptionsDto.Frequency,
            Instructions = prescriptionsDto.Instructions,
            StartDate = prescriptionsDto.StartDate,
            EndDate = prescriptionsDto.EndDate,
            CreatorId = loggedInUser.Id,
            ModifierId = loggedInUser.Id,
            Created = DateTime.Now,
            Modified = DateTime.Now,
        };

        var isAdded = await _unitOfWork.Prescriptions.AddAsync(newPrescription);
        if(!isAdded)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        result.Content = prescriptionsDto;

        await _unitOfWork.CompleteAsync();

        return CreatedAtRoute(routeName: "Prescription", new { newPrescription.Id }, result);
    }

    [HttpDelete("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        var result = new Result<Prescription>();

        var isDeleted = await _unitOfWork.Prescriptions.DeleteAsync(new Guid(id));
        if (!isDeleted)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));
        await _unitOfWork.CompleteAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] PrescriptionsDto prescriptionsDto)
    {
        var result = new Result<PrescriptionsDto>();

        if (prescriptionsDto is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.Generic.InvalidPayload,
                                                           ErrorMessages.Generic.BadRequest));

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        prescriptionsDto.Id = id;
        prescriptionsDto.ModifierId = loggedInUser.Id;
        prescriptionsDto.Modified = DateTime.Now;

        var isUpdated = await _unitOfWork.Prescriptions.UpdateAsync(prescriptionsDto);
        if (!isUpdated)
            return BadRequest(result.Error = PopulateError(500,
                                                           ErrorMessages.Generic.SomethingWentWrong,
                                                           ErrorMessages.Generic.UnableToProcess));

        await _unitOfWork.CompleteAsync();

        return Ok();

    }
}
