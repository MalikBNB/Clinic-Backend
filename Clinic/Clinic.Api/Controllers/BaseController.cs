using AutoMapper;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;

        public UserManager<IdentityUser> _userManager;

        public readonly IMapper _mapper;

        public BaseController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        internal Error PopulateError(int code, string type, string message)
        {
            return new Error
            {
                Code = code,
                Type = type,
                Message = message
            };
        }
    }
}
