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

        public async Task<bool> DeleteDepartmentType(int departmentTypeId)
        {
            return await _testRepository.DeleteDepartmentType(departmentTypeId);
        }
        public async Task<bool> DeleteSampleType(int sampleTypeId)
        {
            return await _testRepository.DeleteSampleType(sampleTypeId);
        }

        public async Task<bool> InsertReportFormatTypeAsync(ReportFormatType reportFormatType)
        {
            return await _testRepository.InsertUpdateRoportFormatTypeAsync(reportFormatType);
        }

        public async Task<List<ReportFormatType>> GetReportFormatType(int? sampleTypeId)
        {
            return await _testRepository.GetReportFormatType(sampleTypeId);
        }

        public async Task<bool> DeleteReportFormatType(int departmentTypeId)
        {
            return await _testRepository.DeleteReportFormatType(departmentTypeId);
        }

        public async Task<Dictionary<bool, string>> InsertUpdateTestAsync(Test test)
        {
            return await _testRepository.InsertUpdateTestAsync(test);
        }

        public async Task<List<Test>> GetTestAsync(int? testID)
        {
            return await _testRepository.GetTestAsync(testID);
        }

        public async Task<bool> DeleteTestAsync(int testID)
        {
            return await _testRepository.DeleteTestAsync(testID);
        }
    }
}
