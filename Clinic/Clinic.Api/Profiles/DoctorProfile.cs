using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Doctors;
using Clinic.Entities.DTOs.Outgoing;

namespace Clinic.Api.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<User, DoctorDto>()
                .ForMember(
                    dest => dest.FirstName,
                    from => from.MapFrom(user => $"{user.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(user => $"{user.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(user => $"{user.Email}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(user => Convert.ToDateTime(user.DateOfBirth))
                );

            CreateMap<Doctor, DoctorProfileDto>()
                .ForMember(
                    dest => dest.FirstName,
                    from => from.MapFrom(dto => $"{dto.User.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(dto => $"{dto.User.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(dto => $"{dto.User.Email}")
                )
                .ForMember(
                    dest => dest.Specialization,
                    from => from.MapFrom(dto => $"{dto.Specialization}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(dto => Convert.ToDateTime(dto.User.DateOfBirth))
                )
                .ForMember(
                    dest => dest.Address,
                    from => from.MapFrom(dto => $"{dto.User.Address}")
                )
                .ForMember(
                    dest => dest.Gendor,
                    from => from.MapFrom(dto => $"{dto.User.Gendor}")
                )
                .ForMember(
                    dest => dest.Phone,
                    from => from.MapFrom(dto => $"{dto.User.Phone}")
                );

        }
    }
}
