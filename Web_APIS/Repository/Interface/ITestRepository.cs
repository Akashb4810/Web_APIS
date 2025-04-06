using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_APIS.Models;

namespace Web_APIS.Repository.Interface
{
    public interface ITestRepository
    {
        Task<bool> InsertUpdateDepartmentTypeAsync(DepartmentType departmentType);
        Task<bool> InsertUpdateRoportFormatTypeAsync(ReportFormatType reportFormatType);
        Task<List<DepartmentType>> GetDepartmentType(int? departmentTypeId);
        Task<bool> DeleteDepartmentType(int departmentTypeId);
        Task<bool> InsertUpdateSampleType(SampleType sampleType);
        Task<List<SampleType>> GetSampleType(int? sampleTypeId);
        Task<bool> DeleteSampleType(int sampleTypeId);
        Task<List<ReportFormatType>> GetReportFormatType(int? reportFormatType);
        Task<bool> DeleteReportFormatType(int reportFormatType);
        Task<Dictionary<bool, string>> InsertUpdateTestAsync(Test test);
        Task<List<Test>> GetTestAsync(int? testID);
        Task<bool> DeleteTestAsync(int testID);

    }
}
