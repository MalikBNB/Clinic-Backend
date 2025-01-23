using Asp.Versioning;
using AutoMapper;
using Clinic.Authentication.Configuration;
using Clinic.Authentication.Models.DTOs.Incoming;
using Clinic.Authentication.Models.DTOs.Outgoing;
using Clinic.Authentication.Models.Interfaces;
using Clinic.Configuration.Messages;
using Clinic.DataService.Data;
using Clinic.DataService.IConfiguration;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Outgoing;
using Clinic.Entities.Global.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clinic.Api.Controllers.V1.Users
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private IdentityUser _IdentityUser;

        public AccountsController(IUnitOfWork unitOfWork,
                                  UserManager<IdentityUser> userManager,
                                  TokenValidationParameters tokenValidationParameters,
                                  IMapper mapper,
                                  IOptionsMonitor<JwtConfig> optionsMonitor) : base(unitOfWork, userManager, mapper)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
        }

        [HttpPost]
        [Route("User/Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Generic.InvalidPayload }
                });
            }

            var userExist = await _userManager.FindByEmailAsync(registrationDto.Email);
            if (userExist is not null)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Register.EmailInUse }
                });
            }

            var createIdentityUser = await CreateIdentityUser(registrationDto);
            if (!createIdentityUser.Succeeded)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = createIdentityUser.Errors.Select(e => e.Description).ToList()
                });
            }

            var newUser = new User
            {
                IdentityId = new Guid(_IdentityUser.Id),
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                DateOfBirth = DateTime.Now,
                Address = "",
                Gendor = "",
                Phone = "",
                Status = 1,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            var tokenData = await GenerateToken(_IdentityUser);

            return Ok(new UserRegistrationResponseDto
            {
                Success = true,
                Token = tokenData.JwtToken,
                RefreshToken = tokenData.RefreshToken,
            });
        }

        [HttpPost]
        [Route("Patient/Register")]
        public async Task<IActionResult> Register([FromBody] PatientRegistrationRequestDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Generic.InvalidPayload }
                });
            }

            var userExist = await _userManager.FindByEmailAsync(registrationDto.Email);
            if (userExist is not null)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Register.EmailInUse }
                });
            }

            var createIdentityUser = await CreateIdentityUser(registrationDto);
            if (!createIdentityUser.Succeeded)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = createIdentityUser.Errors.Select(e => e.Description).ToList()
                });
            }

            var patient = new Patient
            {
                IdentityId = new Guid(_IdentityUser.Id),
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                DateOfBirth = DateTime.Now,
                Address = "",
                Gendor = "",
                Phone = "",
                Status = 1,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            var tokenData = await GenerateToken(_IdentityUser);

            return Ok(new UserRegistrationResponseDto
            {
                Success = true,
                Token = tokenData.JwtToken,
                RefreshToken = tokenData.RefreshToken,
            });
        }

        [HttpPost]
        [Route("Doctor/Register")]
        public async Task<IActionResult> Register([FromBody] DoctorRegistrationRequestDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Generic.InvalidPayload }
                });
            }

            var userExist = await _userManager.FindByEmailAsync(registrationDto.Email);
            if (userExist is not null)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Register.EmailInUse }
                });
            }

            var createIdentityUser = await CreateIdentityUser(registrationDto);
            if (!createIdentityUser.Succeeded)
            {
                return BadRequest(new UserRegistrationResponseDto
                {
                    Success = false,
                    Errors = createIdentityUser.Errors.Select(e => e.Description).ToList()
                });
            }

            var doctor = new Doctor
            {
                IdentityId = new Guid(_IdentityUser.Id),
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                DateOfBirth = DateTime.Now,
                Address = "",
                Gendor = "",
                Phone = "",
                Specialization = registrationDto.Specialization,
                Status = 1,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            var tokenData = await GenerateToken(_IdentityUser);

            return Ok(new UserRegistrationResponseDto
            {
                Success = true,
                Token = tokenData.JwtToken,
                RefreshToken = tokenData.RefreshToken,
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserLoginResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Generic.InvalidPayload }
                });

            var userExists = await _userManager.FindByEmailAsync(loginDto.Email);
            if (userExists is null)
                return BadRequest(new UserLoginResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Login.InvalidAuthentication }
                });

            var isPasswordValid = await _userManager.CheckPasswordAsync(userExists, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest(new UserLoginResponseDto
                {
                    Success = false,
                    Errors = new List<string> { ErrorMessages.Login.InvalidAuthentication }
                });

            var tokenData = await GenerateToken(userExists);
            return Ok(new UserLoginResponseDto
            {
                Success = true,
                Token = tokenData.JwtToken,
                RefreshToken = tokenData.RefreshToken,
            });
        }


        private async Task<IdentityResult> CreateIdentityUser(IUserRegistrationRequest registrationDto)
        {
            _IdentityUser = new IdentityUser
            {
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                EmailConfirmed = true
            };

            return await _userManager.CreateAsync(_IdentityUser, registrationDto.Password);
        }

        private async Task<TokenData> GenerateToken(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim("Id", user.Id),
                   new Claim(ClaimTypes.NameIdentifier, user.Id),
                   new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.Now.Add(_jwtConfig.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtHandler.WriteToken(token);

            var refreshToken = new RefreshToken
            {
                Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
                UserId = user.Id,
                IsRevoked = false,
                IsUsed = false,
                Status = 1,
                JwtId = token.Id,
                ExpiryDate = DateTime.Now.AddMonths(6),
                CreatorId = user.Id,
                ModifierId = user.Id,
                Created = DateTime.Now,
                Modified = DateTime.Now,
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.CompleteAsync();

            var tokenData = new TokenData
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token
            };

            return tokenData;
        }

        private string RandomStringGenerator(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
