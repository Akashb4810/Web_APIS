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
        Task<List<DepartmentType>> GetDepartmentType(int? departmentTypeId);
        Task<bool> InsertUpdateSampleType(SampleType sampleType);
        Task<List<SampleType>> GetSampleType(int? sampleTypeId);
    }
}
