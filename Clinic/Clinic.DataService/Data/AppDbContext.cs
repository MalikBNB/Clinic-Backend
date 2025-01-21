using Clinic.Entities.DbSets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DataService.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<User> Users {  get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens {  get; set; }
        public virtual DbSet<Patient> Patients {  get; set; }
        public virtual DbSet<Doctor> Doctors {  get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords {  get; set; }
        public virtual DbSet<Prescription> Prescriptions {  get; set; }
        public virtual DbSet<Payment> Payments {  get; set; }
        public virtual DbSet<Appointment> Appointments {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Doctor>().HasOne(o => o.User)
            //       .WithOne(o => o.Doctor)
            //       .HasForeignKey<Doctor>(o => o.UserId)
            //       .IsRequired()
            //       .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Doctor>().HasIndex(o => o.UserId).IsUnique(); // Impossible that UserId be repeated in Doctors Table 

            //builder.Entity<Patient>().HasOne(o => o.User)
            //    .WithOne(o => o.Patient)
            //    .HasForeignKey<Patient>(o => o.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);  
            //builder.Entity<Patient>().HasIndex(o => o.UserId).IsUnique();

            //builder.Entity<Appointment>().HasOne(o => o.Patient)
            //    .WithMany(o => o.Appointments)
            //    .HasForeignKey(o => o.PatientId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Appointment>().HasOne(o => o.Doctor)
            //    .WithMany(o => o.Appointments)
            //    .HasForeignKey(o => o.DoctorId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);
            
            //builder.Entity<Appointment>().HasOne(o => o.Creator)
            //    .WithMany(o => o.Appointments)
            //    .HasForeignKey(o => o.CreatorId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Prescription>().HasOne(o => o.MedicalRecord)
            //    .WithOne(o => o.Prescription)
            //    .HasForeignKey<Prescription>(o => o.MedicalRecordId)
            //    .IsRequired().OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Prescription>().HasIndex(o => o.MedicalRecordId).IsUnique();

            //builder.Entity<MedicalRecord>().HasOne(o => o.Appointment)
            //    .WithOne(o => o.MedicalRecord)
            //    .HasForeignKey<MedicalRecord>(o => o.AppointmentId)
            //    .IsRequired();
            //builder.Entity<MedicalRecord>().HasIndex(o => o.AppointmentId).IsUnique();

            //builder.Entity<Payment>().HasOne(o => o.Appointment)
            //    .WithOne(o => o.Payment)
            //    .HasForeignKey<Payment>(o => o.AppointmentId)
            //    .IsRequired();
            //builder.Entity<Payment>().HasIndex(o => o.AppointmentId).IsUnique();


            base.OnModelCreating(builder);
        }

    }
}
