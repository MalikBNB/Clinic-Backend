using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Payments;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : BaseController
    {
        public PaymentsController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            : base(unitOfWork, userManager, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = new PagedResult<PaymentsResponseDto>();

            var payments = await _unitOfWork.Payments.GetAllAsync();

            if (payments.Any())
                result.ResultCount = payments.Count();

            foreach (var pay in payments)
                result.Content.Add(_mapper.Map<PaymentsResponseDto>(pay));

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "Payment")]
        public async Task<IActionResult> FindAsync(Guid id)
        {
            var result = new Result<PaymentsResponseDto>();

            var payment = await _unitOfWork.Payments.FindAsync(p => p.Id == id);
            if (payment is null)
            {
                result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
                return NotFound(result);
            }
            result.Content = _mapper.Map<PaymentsResponseDto>(payment);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PaymentsDto paymentsDto)
        {
            var result = new Result<PaymentsDto>();

            if (!ModelState.IsValid)
            {
                result.Error = PopulateError(400, ErrorMessages.Generic.InvalidPayload, ErrorMessages.Generic.BadRequest);
                return BadRequest(result);
            }

            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedInUser is null)
            {
                result.Error = PopulateError(400, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
                return BadRequest(result);
            }

            var newPayment = _mapper.Map<Payment>(paymentsDto);
            newPayment.CreatorId = loggedInUser.Id;
            newPayment.ModifierId = loggedInUser.Id;
            newPayment.Created = DateTime.Now;
            newPayment.Modified = DateTime.Now;

            var isAdded = await _unitOfWork.Payments.AddAsync(newPayment);
            if (!isAdded)
            {
                result.Error = PopulateError(400, ErrorMessages.Generic.SomethingWentWrong, ErrorMessages.Generic.InvalidRequest);
                return BadRequest(result);
            }

            await _unitOfWork.CompleteAsync();

            result.Content = paymentsDto;

            return CreatedAtRoute("Payment", new { newPayment.Id }, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = new Result<Payment>();

            var payment = await _unitOfWork.Payments.FindAsync(p => p.Id == id);
            if (payment is null)
            {
                result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
                return NotFound(result);
            }

            var isDeleted = await _unitOfWork.Payments.DeleteAsync(payment);
            if (!isDeleted)
            {
                result.Error = PopulateError(400, ErrorMessages.Generic.SomethingWentWrong, ErrorMessages.Generic.InvalidRequest);
                return BadRequest(result);
            }

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
