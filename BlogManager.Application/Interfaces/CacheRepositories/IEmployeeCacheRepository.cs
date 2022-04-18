using BlogManager.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Application.Interfaces.CacheRepositories
{
    public interface IEmployeeCacheRepository
    {
        Task<List<Employee>> GetCachedListAsync();

        Task<Employee> GetByIdAsync(int employeeId);

        Task<List<Employee>> GetByDepartmentId(int departmentId);
    }
}
