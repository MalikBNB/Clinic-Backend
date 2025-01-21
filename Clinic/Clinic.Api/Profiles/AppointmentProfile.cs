using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Appointments;
using Clinic.Entities.DTOs.Outgoing;

namespace Clinic.Api.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            #region AppointmentDto ==> Appointment

            CreateMap<AppointmentDto, Appointment>().
                ForMember(
                    dest => dest.Date,
                    from => from.MapFrom(dto => dto.Date)
                ).
                ForMember(
                    dest => dest.status,
                    from => from.MapFrom(dto => dto.status)
                )
                .ForMember(
                    dest => dest.PatientId,
                    from => from.MapFrom(dto => dto.PatientId)
                )
                .ForMember(
                    dest => dest.DoctorId,
                    from => from.MapFrom(dto => dto.DoctorId)
                )
                .ForMember(
                    dest => dest.CreatorId,
                    from => from.MapFrom(a => a.CreatorId)
                )
                .ForMember(
                    dest => dest.Created,
                    from => from.MapFrom(a => a.Created)
                )
                .ForMember(
                    dest => dest.ModifierId,
                    from => from.MapFrom(a => a.ModifierId)
                )
                .ForMember(
                    dest => dest.Modified,
                    from => from.MapFrom(a => a.Modified)
                );

            #endregion AppointmentDto ==> Appointment

            #region Appointment ==> AppointmentResponseDto

            CreateMap<Appointment, AppointmentResponseDto>().
                ForMember(
                    dest => dest.Date,
                    from => from.MapFrom(a => a.Date)
                ).
                ForMember(
                    dest => dest.status,
                    from => from.MapFrom(a => a.status)
                )
                .ForMember(
                    dest => dest.PatientId,
                    from => from.MapFrom(a => a.PatientId)
                )
                .ForMember(
                    dest => dest.Patient,
                    from => from.MapFrom(a => a.Patient)
                )
                .ForMember(
                    dest => dest.DoctorId,
                    from => from.MapFrom(a => a.DoctorId)
                )
                .ForMember(
                    dest => dest.Doctor,
                    from => from.MapFrom(a => a.Doctor)
                )
                .ForMember(
                    dest => dest.CreatorId,
                    from => from.MapFrom(a => a.CreatorId)
                )
                .ForMember(
                    dest => dest.Creator,
                    from => from.MapFrom(a => a.Creator)
                )
                .ForMember(
                    dest => dest.Created,
                    from => from.MapFrom(a => a.Created)
                )
                .ForMember(
                    dest => dest.ModifierId,
                    from => from.MapFrom(a => a.ModifierId)
                )
                .ForMember(
                    dest => dest.Modifier,
                    from => from.MapFrom(a => a.Modifier)
                )
                .ForMember(
                    dest => dest.Modified,
                    from => from.MapFrom(a => a.Modified)
                );

            #endregion Appointment ==> AppointmentResponseDto
        }
    }
}
