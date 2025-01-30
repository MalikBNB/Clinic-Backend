using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.MedicalRecords;
using Clinic.Entities.DTOs.Outgoing;

namespace Clinic.Api.Profiles;

public class MedicalRecordProfile : Profile
{
    public MedicalRecordProfile()
    {
        #region MedicalRecordDto ==> MedicalRecord

        CreateMap<MedicalRecordDto, MedicalRecord>()
            .ForMember(
                dest => dest.VisitDescription,
                from => from.MapFrom(dto => $"{dto.VisitDescription}")
            )
            .ForMember(
                dest => dest.Diagnosis,
                from => from.MapFrom(dto => $"{dto.Diagnosis}")
            )
            .ForMember(
                dest => dest.Notes,
                from => from.MapFrom(dto => $"{dto.Notes}")
            )
            .ForMember(
                dest => dest.AppointmentId,
                from => from.MapFrom(dto => dto.AppointmentId)
            );

        #endregion MedicalRecordDto ==> MedicalRecord

        #region MedicalRecord => MedicalRecordsResponseDto

        CreateMap<MedicalRecord, MedicalRecordsResponseDto>()
            .ForMember(
                dest => dest.VisitDescription,
                from => from.MapFrom(o => o.VisitDescription)
            )
            .ForMember(
                dest => dest.Diagnosis,
                from => from.MapFrom(o => o.Diagnosis)
            )
            .ForMember(
                dest => dest.Notes,
                from => from.MapFrom(o => o.Notes)
            )
            .ForMember(
                dest => dest.AppointmentId,
                from => from.MapFrom(o => o.AppointmentId)
            )
            .ForMember(
                dest => dest.AppointmentDate,
                from => from.MapFrom(o => o.Appointment.Date)
            )
            .ForMember(
                dest => dest.CreatorId,
                from => from.MapFrom(o => o.CreatorId)
            )
            .ForMember(
                dest => dest.ModifierId,
                from => from.MapFrom(o => o.ModifierId)
            )
            .ForMember(
                dest => dest.Created,
                from => from.MapFrom(o => o.Created)
            )
            .ForMember(
                dest => dest.Modified,
                from => from.MapFrom(o => o.Modified)
            );

        #endregion MedicalRecord => MedicalRecordsResponseDto
    }
}
