﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;

namespace Clinic.DataService.IRepositories
{
    public interface IDoctorsRepository : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllBySpeciality(string peciality);
        Task<bool> IsDoctorExists(string email);
    }
}
