using DLL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;
using Web_APIS.Repository.Interface;

namespace DLL.Services.Implementation
{
    public class TestServices:ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestServices(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<List<DepartmentType>> GetDepartmentType(int? departmentTypeId)
        {
            return await _testRepository.GetDepartmentType(departmentTypeId);
        }

        public async Task<bool> InsertDepartmentTypeAsync(DepartmentType departmentType)
        {
            return await _testRepository.InsertUpdateDepartmentTypeAsync(departmentType);
        }
        public async Task<bool> InsertUpdateSampleType(SampleType sampleType)
        {
            return await _testRepository.InsertUpdateSampleType(sampleType);
        }

        public async Task<List<SampleType>> GetSampleType(int? sampleTypeId)
        {
            return await _testRepository.GetSampleType(sampleTypeId);
        }
    }
}
