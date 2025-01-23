using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Users;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork,
                               UserManager<IdentityUser> userManager,
                               IMapper mapper) : base(unitOfWork, userManager, mapper) { }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagedResult = new PagedResult<User>();

            var users = await _unitOfWork.Users.GetAllAsync();
            if (users.Any())
            {
                pagedResult.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.BadRequest);
                return BadRequest(pagedResult);
            }

            pagedResult.Content = users.ToList();
            pagedResult.ResultCount = users.Count();

            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        [Route("User")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = new Result<ProfileDto>();

            var user = await _unitOfWork.Users.GetByIdAsync(new Guid(id));

            if (user is null)
            {
                result.Error = PopulateError(404,
                                             ErrorMessages.User.UserNotFound,
                                             ErrorMessages.Generic.ObjectNotFound);

                return BadRequest(result);
            }

            var mappedProfile = _mapper.Map<ProfileDto>(user);

            result.Content = mappedProfile;

            return Ok(result);
        }

        [HttpPost("User")]
        public async Task<IActionResult> AddAsync([FromBody] UserDto userDto)
        {
            var result = new Result<UserDto>();

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            var mappedUser = _mapper.Map<User>(userDto);

            await _unitOfWork.Users.AddAsync(mappedUser);
            await _unitOfWork.CompleteAsync();

            result.Content = userDto;

            return CreatedAtRoute("User", new { mappedUser.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id)
        {
            var result = new Result<User>();

            var loggedInUser = await GetLoggedInUserAsync();
            if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
                                                                             ErrorMessages.User.UserNotFound,
                                                                             ErrorMessages.Generic.ObjectNotFound));

            var user = await _unitOfWork.Users.GetByIdAsync(new Guid(id));
            if (user is null) return BadRequest(result.Error = PopulateError(400,
                                                                        ErrorMessages.User.UserNotFound,
                                                                        ErrorMessages.Generic.ObjectNotFound));

            user.Status = 0;
            user.Modified = DateTime.Now;
            await _unitOfWork.CompleteAsync();

            return Ok();

        }
    }
}
