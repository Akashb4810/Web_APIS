using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Services.Interface;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace DLL.Services.Implementation
{
    public class DoctorServices : IDoctorServices
    {
        public readonly IDoctorRepository _doctorRepository;
        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<List<DoctorClass>> GetDoctorsAsync(int? CenterId, string doctername)
        {
            return  await _doctorRepository.GetDoctorsAsync(CenterId,doctername);
        }

        public async Task<int> InsertOrUpdateDoctorAsync(DoctorClass doctor)
        {
            return await _doctorRepository.InsertOrUpdateDoctorAsync(doctor);
        }
        public async Task<bool> DeleteDoctorAsync(int doctorId)
        {
            return await _doctorRepository.DeleteDoctorAsync(doctorId);
        }


    }
}
