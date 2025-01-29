using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Payments;
using Clinic.Entities.DTOs.Outgoing;

namespace Clinic.Api.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            #region PaymentsDto ==> Payment

            CreateMap<PaymentsDto, Payment>()
                .ForMember(
                    dest => dest.Date,
                    from => from.MapFrom(dto => dto.Date)
                ).ForMember(
                    dest => dest.PaymentMethod,
                    from => from.MapFrom(dto => dto.PaymentMethod)
                ).ForMember(
                    dest => dest.AppointmentId,
                    from => from.MapFrom(dto => dto.AppointmentId)
                ).ForMember(
                    dest => dest.Notes,
                    from => from.MapFrom(dto => dto.Notes)
                ).ForMember(
                    dest => dest.Amount,
                    from => from.MapFrom(dto => dto.Amount)
                );

            #endregion PaymentsDto ==> Payment

            #region Payment ==> PaymentsResponseDto

            CreateMap<Payment, PaymentsResponseDto>()
                .ForMember(
                    dest => dest.Id,
                    from => from.MapFrom(p => p.Id)
                ).ForMember(
                    dest => dest.Date,
                    from => from.MapFrom(p => p.Date)
                ).ForMember(
                    dest => dest.PaymentMethod,
                    from => from.MapFrom(p => p.PaymentMethod)
                ).ForMember(
                    dest => dest.Amount,
                    from => from.MapFrom(p => p.Amount)
                ).ForMember(
                    dest => dest.Notes,
                    from => from.MapFrom(p => p.Notes)
                ).ForMember(
                    dest => dest.AppointmentId,
                    from => from.MapFrom(p => p.AppointmentId)
                ).ForMember(
                    dest => dest.AppointmentDate,
                    from => from.MapFrom(p => p.Appointment.Date)
                ).ForMember(
                    dest => dest.Patient,
                    from => from.MapFrom(p => p.Appointment.Patient.FullName)
                ).ForMember(
                    dest => dest.CreatorId,
                    from => from.MapFrom(p => p.CreatorId)
                ).ForMember(
                    dest => dest.Created,
                    from => from.MapFrom(p => p.Created)
                ).ForMember(
                    dest => dest.Creator,
                    from => from.MapFrom(p => p.Creator.UserName)
                ).ForMember(
                    dest => dest.ModifierId,
                    from => from.MapFrom(p => p.ModifierId)
                ).ForMember(
                    dest => dest.Modified,
                    from => from.MapFrom(p => p.Modified)
                ).ForMember(
                    dest => dest.Modifier,
                    from => from.MapFrom(p => p.Modifier.UserName)
                );

            #endregion Payment ==> PaymentsResponseDto

        }
    }
}
