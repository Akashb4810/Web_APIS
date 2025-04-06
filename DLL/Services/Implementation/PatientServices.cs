using DLL.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Implementaion;
using Web_APIS.Repository.Interface;
namespace DLL.Services.Implementation
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientRepository _patientRepository;

        public PatientServices(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<int> InsertPatientType(PatientType patientType)
        {
            return await _patientRepository.InsertPatientType(patientType);
        }

        public async Task<List<PatientType>> GetAllPatientTypes()
        {
            return await _patientRepository.GetAllPatientTypes();
        }

        public async Task<int> DeletePatientType(int patientTypeId)
        {
            return await _patientRepository.DeletePatientType(patientTypeId);
        }

    }
}
