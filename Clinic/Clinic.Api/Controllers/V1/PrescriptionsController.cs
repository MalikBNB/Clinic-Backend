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


    [HttpGet("{medicalRecordId:guid}/All")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(Guid medicalRecordId)
    {
        var result = new PagedResult<PrescriptionsDto>();

        var prescriptions = await _unitOfWork.Prescriptions.GetAllAsync(p => p.MedicalRecordId == medicalRecordId);
        if (!prescriptions.Any())
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

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
        if (prescriptions.Any())
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

        foreach (var prescription in prescriptions)
            result.Content.Add(_mapper.Map<PrescriptionsDto>(prescription));

        result.ResultCount = result.Content.Count;

        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "Prescription")]
    [AllowAnonymous]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<PrescriptionsDto>();

        var prescription = await _unitOfWork.Prescriptions.FindAsync(p => p.Id == id, ["MedicalRecord"]);
        if (prescription is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

        result.Content = _mapper.Map<PrescriptionsDto>(prescription);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PrescriptionsDto prescriptionsDto)
    {
        var result = new Result<PrescriptionsDto>();

        if (!ModelState.IsValid)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.InvalidPayload, ErrorMessages.Generic.BadRequest);
            return BadRequest(result);
        }

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
            return BadRequest(result.Error = PopulateError(400,
                                                           ErrorMessages.User.UserNotFound,
                                                           ErrorMessages.Generic.ObjectNotFound));

        var newPrescription = _mapper.Map<Prescription>(prescriptionsDto);

        newPrescription.CreatorId = loggedInUser.Id;
        newPrescription.ModifierId = loggedInUser.Id;
        newPrescription.Created = DateTime.Now;
        newPrescription.Modified = DateTime.Now;

        var isAdded = await _unitOfWork.Prescriptions.AddAsync(newPrescription);
        if (!isAdded)
        {
            result.Error = PopulateError(500, ErrorMessages.Generic.SomethingWentWrong, ErrorMessages.Generic.UnableToProcess);
            return BadRequest(result);
        }

        result.Content = prescriptionsDto;

        await _unitOfWork.CompleteAsync();

        return CreatedAtRoute(routeName: "Prescription", new { newPrescription.Id }, result);
    }

    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = new Result<Prescription>();

        var prescription = await _unitOfWork.Prescriptions.FindAsync(p => p.Id == id);
        if (prescription is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

        var isDeleted = await _unitOfWork.Prescriptions.DeleteAsync(prescription);
        if (!isDeleted)
        {
            result.Error = PopulateError(500, ErrorMessages.Generic.SomethingWentWrong, ErrorMessages.Generic.UnableToProcess);
            return BadRequest(result);
        }

        await _unitOfWork.CompleteAsync();

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PrescriptionsDto prescriptionsDto)
    {
        var result = new Result<PrescriptionsDto>();

        if (!ModelState.IsValid)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.InvalidPayload, ErrorMessages.Generic.BadRequest);
            return BadRequest(result);
        }

        var loggedInUser = await GetLoggedInUserAsync();
        if (loggedInUser is null)
        {
            result.Error = PopulateError(400, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

        var prescription = await _unitOfWork.Prescriptions.FindAsync(p => p.Id == id);
        if (prescription is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return BadRequest(result);
        }

        prescription.Medication = prescriptionsDto.Medication;
        prescription.Dosage = prescriptionsDto.Dosage;
        prescription.Frequency = prescriptionsDto.Frequency;
        prescription.Instructions = prescriptionsDto.Instructions;
        prescription.StartDate = prescriptionsDto.StartDate;
        prescription.EndDate = prescriptionsDto.EndDate;
        prescription.ModifierId = loggedInUser.Id;
        prescription.Modified = DateTime.Now;

        await _unitOfWork.CompleteAsync();

        return Ok();

    }
}
