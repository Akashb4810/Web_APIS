using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface IDoctorRepository
    {
        Task<List<DoctorClass>> GetDoctorsAsync(int? CenterId, string doctername);
        Task<int> InsertOrUpdateDoctorAsync(DoctorClass doctor);
        Task<bool> DeleteDoctorAsync(int doctorId);

    }
}
