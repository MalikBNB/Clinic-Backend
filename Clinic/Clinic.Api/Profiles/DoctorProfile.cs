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
            #region DoctorDto ==> Doctor

            CreateMap<DoctorDto, Doctor>()
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
                    dest => dest.Specialization,
                    from => from.MapFrom(dto => $"{dto.Specialization}")
                )
                .ForMember(
                    dest => dest.Status,
                    from => from.MapFrom(dto => 1)
                );

            #endregion DoctorDto ==> Doctor

            #region Doctor ==> DoctorProfileDto

            CreateMap<Doctor, DoctorProfileDto>()
                .ForMember(
                    dest => dest.FirstName,
                    from => from.MapFrom(d => $"{d.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    from => from.MapFrom(d => $"{d.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    from => from.MapFrom(d => $"{d.Email}")
                )
                .ForMember(
                    dest => dest.Specialization,
                    from => from.MapFrom(d => $"{d.Specialization}")
                )
                .ForMember(
                    dest => dest.DateOfBirth,
                    from => from.MapFrom(d => Convert.ToDateTime(d.DateOfBirth))
                )
                .ForMember(
                    dest => dest.Address,
                    from => from.MapFrom(d => $"{d.Address}")
                )
                .ForMember(
                    dest => dest.Gendor,
                    from => from.MapFrom(d => $"{d.Gendor}")
                )
                .ForMember(
                    dest => dest.Phone,
                    from => from.MapFrom(d => $"{d.Phone}")
                );

            #endregion Doctor ==> DoctorProfileDto

        }
    }
}
