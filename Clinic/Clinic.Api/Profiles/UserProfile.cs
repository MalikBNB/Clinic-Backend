using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Incoming.Profile;
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
                    dest => dest.FirstName,
                    from => from.MapFrom(u => $"{u.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(u => $"{u.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(u => $"{u.Email}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(u => Convert.ToDateTime(u.DateOfBirth))
                )
                .ForMember(
                    dest => dest.Address,
                    from => from.MapFrom(u => $"{u.Address}")
                )
                .ForMember(
                    dest => dest.Gendor,
                    from => from.MapFrom(u => $"{u.Gendor}")
                )
                .ForMember(
                    dest => dest.Phone,
                    from => from.MapFrom(u => $"{u.Phone}")
                )
                .ForMember(
                    dest => dest.Status,
                    from => from.MapFrom(u => u.Status)
                );

            #endregion User ==> ProfileDto
        }
    }
}
