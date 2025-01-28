using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Incoming.Users;
using Clinic.Entities.DTOs.Outgoing;

namespace Clinic.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region UserDto ==> User

            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.FirstName,
                    from => from.MapFrom(dto => $"{dto.FirstName}")
                    )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(dto => $"{dto.LastName}")
                    )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(dto => $"{dto.Email}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(dto => Convert.ToDateTime(dto.DateOfBirth))
                )
                .ForMember(
                    dest => dest.Status,
                    from => from.MapFrom(dto => 1)
                );

            #endregion UserDto ==> User

            #region User ==> ProfileDto

            CreateMap<User, ProfileDto>()
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(dto => $"{dto.Id}")
                )
                .ForMember(
                    dest => dest.FirstName,
                    from => from.MapFrom(dto => $"{dto.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(dto => $"{dto.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(dto => $"{dto.Email}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(dto => Convert.ToDateTime(dto.DateOfBirth))
                )
                .ForMember(
                    dest => dest.Address,
                    from => from.MapFrom(dto => $"{dto.Address}")
                )
                .ForMember(
                    dest => dest.Gendor,
                    from => from.MapFrom(dto => $"{dto.Gendor}")
                )
                .ForMember(
                    dest => dest.Phone,
                    from => from.MapFrom(dto => $"{dto.Phone}")
                )
                .ForMember(
                    dest => dest.Status,
                    from => from.MapFrom(dto => dto.Status)
                );

            #endregion User ==> ProfileDto
        }
    }
}
