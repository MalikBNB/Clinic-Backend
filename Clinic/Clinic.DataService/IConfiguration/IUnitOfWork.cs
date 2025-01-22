using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.IConfiguration
{
    public interface IUnitOfWork
    {
        IUsersRepository Users {  get; }
        IRefreshTokensRepository RefreshTokens { get; }
        IPatientsRepository Patients { get; }
        IDoctorsRepository Doctors { get; }
        IMedicalRecordRepository MedicalRecords { get; }
        IPrescriptionRepository Prescriptions { get; }
        IPaymentRepository Payments { get; }
        IAppointmentRepository Appointments { get; }

        Task CompleteAsync();
    }
}
