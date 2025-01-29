using Clinic.DataService.IConfiguration;
using Clinic.DataService.IRepositories;
using Clinic.DataService.Repositories;
using Clinic.Entities.DbSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public IUsersRepository Users { get; private set; }
        public IRefreshTokensRepository RefreshTokens { get; private set; }
        public IPatientsRepository Patients { get; private set; }
        public IDoctorsRepository Doctors { get; private set; }
        public IMedicalRecordRepository MedicalRecords { get; private set; }
        public IPrescriptionRepository Prescriptions { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }

        public UnitOfWork(AppDbContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("db_logs");

            Users = new UsersRepository(_context, _logger);
            RefreshTokens = new RefreshTokensRepository(_context, _logger);
            Patients = new PatientsRepository(_context, _logger);
            Doctors = new DoctorsRepository(_context, _logger);
            MedicalRecords = new MedicalRecordsRepository(_context, _logger);
            Prescriptions = new PrescriptionsRepository(_context, _logger);
            Payments = new PaymentRepository(_context, _logger);
            Appointments = new AppointmentsRepository(_context, _logger);
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
