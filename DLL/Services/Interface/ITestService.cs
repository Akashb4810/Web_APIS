using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace DLL.Services.Interface
{
    public interface ITestService
    {
        Task<bool> InsertDepartmentTypeAsync(DepartmentType departmentType);
        Task<bool> InsertReportFormatTypeAsync(ReportFormatType reportFormatType);
        Task<List<DepartmentType>> GetDepartmentType(int? departmentTypeId);
        Task<bool> DeleteDepartmentType(int departmentTypeId);
        Task<bool> InsertUpdateSampleType(SampleType sampleType);
        Task<List<SampleType>> GetSampleType(int? sampleTypeId);
        Task<bool> DeleteSampleType(int departmentTypeId);
        Task<List<ReportFormatType>> GetReportFormatType(int? sampleTypeId);
        Task<bool> DeleteReportFormatType(int departmentTypeId);
        Task<Dictionary<bool, string>> InsertUpdateTestAsync(Test test);
        Task<List<Test>> GetTestAsync(int? testID);
        Task<bool> DeleteTestAsync(int testID);
    }
}
