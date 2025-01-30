using AutoMapper;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Prescriptions;

namespace Clinic.Api.Profiles;

public class PrescriptionsProfile : Profile
{
    public PrescriptionsProfile()
    {
        #region Prescription ==> PrescriptionsDto

        CreateMap<Prescription, PrescriptionsDto>()
            .ForMember(
                dest => dest.Medication,
                from => from.MapFrom(p => p.Medication)
            )
            .ForMember(
                dest => dest.Dosage,
                from => from.MapFrom(p => p.Dosage)
            )
            .ForMember(
                dest => dest.Frequency,
                from => from.MapFrom(p => p.Frequency)
            )
            .ForMember(
                dest => dest.StartDate,
                from => from.MapFrom(p => p.StartDate)
            )
            .ForMember(
                dest => dest.EndDate,
                from => from.MapFrom(p => p.EndDate)
            )
            .ForMember(
                dest => dest.Instructions,
                from => from.MapFrom(p => p.Instructions)
            )
            .ForMember(
                dest => dest.MedicalRecordId,
                from => from.MapFrom(p => p.MedicalRecordId)
            );

        #endregion Prescriptions ==> PrescriptionsDto

        #region PrescriptionsDto ==> Prescriptions

        CreateMap<PrescriptionsDto, Prescription>()
            .ForMember(
                dest => dest.Medication,
                from => from.MapFrom(p => p.Medication)
            )
            .ForMember(
                dest => dest.Dosage,
                from => from.MapFrom(p => p.Dosage)
            )
            .ForMember(
                dest => dest.Frequency,
                from => from.MapFrom(p => p.Frequency)
            )
            .ForMember(
                dest => dest.StartDate,
                from => from.MapFrom(p => p.StartDate)
            )
            .ForMember(
                dest => dest.EndDate,
                from => from.MapFrom(p => p.EndDate)
            )
            .ForMember(
                dest => dest.Instructions,
                from => from.MapFrom(p => p.Instructions)
            )
            .ForMember(
                dest => dest.MedicalRecordId,
                from => from.MapFrom(p => p.MedicalRecordId)
            );

        #endregion Prescriptions ==> PrescriptionsDto
    }
}
