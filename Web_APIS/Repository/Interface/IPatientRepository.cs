using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<int> InsertPatientType(PatientType patientType);
        Task<List<PatientType>> GetAllPatientTypes();
        Task<int> DeletePatientType(int patientTypeId);

    }
}