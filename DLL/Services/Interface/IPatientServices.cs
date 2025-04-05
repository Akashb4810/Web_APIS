using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface IPatientServices
    {
        Task<int> InsertPatientType(PatientType patientType);
        Task<List<PatientType>> GetAllPatientTypes();
        Task<int> DeletePatientType(int patientTypeId);
    }
}