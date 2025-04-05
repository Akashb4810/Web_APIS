using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface IDoctorServices
    {
        Task<List<DoctorClass>> GetDoctorsAsync(int? CenterId, string doctername);
        Task<int> InsertOrUpdateDoctorAsync(DoctorClass doctor);
        Task<bool> DeleteDoctorAsync(int doctorId);


    }
}
