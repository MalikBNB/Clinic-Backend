using Asp.Versioning;
using AutoMapper;
using Clinic.Configuration.Messages;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Users;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers.V1.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper) 
            : base(unitOfWork, userManager, mapper) { }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pagedResult = new PagedResult<User>();

            var users = await _unitOfWork.Users.GetAllAsync();
            if (!users.Any())
            {
                pagedResult.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.BadRequest);
                return BadRequest(pagedResult);
            }

            pagedResult.Content = users.ToList();
            pagedResult.ResultCount = users.Count();

            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = new Result<ProfileDto>();

            var user = await _unitOfWork.Users.FindAsync(u => u.Id == new Guid(id) && u.Status == 1);

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

        //[HttpPost]
        //public async Task<IActionResult> AddAsync([FromBody] UserDto userDto)
        //{
        //    var result = new Result<UserDto>();

        //    var loggedInUser = await GetLoggedInUserAsync();
        //    if (loggedInUser is null) return BadRequest(result.Error = PopulateError(400,
        //                                                                             ErrorMessages.User.UserNotFound,
        //                                                                             ErrorMessages.Generic.ObjectNotFound));

        //    var mappedUser = _mapper.Map<User>(userDto);

        //    await _unitOfWork.Users.AddAsync(mappedUser);
        //    await _unitOfWork.CompleteAsync();

        //    result.Content = userDto;

        //    return CreatedAtRoute("User", new {id = mappedUser.Id }, result);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = new Result<User>();

            var isDeleted = await _unitOfWork.Users.DeleteAsync(new Guid(id));
            if (!isDeleted) 
                return BadRequest(result.Error = PopulateError(400,
                                                               ErrorMessages.Generic.SomethingWentWrong,
                                                               ErrorMessages.Generic.InvalidRequest));           

            await _unitOfWork.CompleteAsync();

            return Ok();

        }
    }
}
