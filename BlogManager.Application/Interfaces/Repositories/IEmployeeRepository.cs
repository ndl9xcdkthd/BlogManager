using BlogManager.Application.Extensions;
using BlogManager.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }

        Task<List<Employee>> GetListPageAsync(DataTableAjaxPostModel model);
        Task<List<Employee>> GetListAsync();

        Task<List<Employee>> GetActiveListAsync();
        Task<List<Employee>> GetActiveListAsync(string sortColumn, string sortColumnDirection);

        Task<List<Employee>> GetQueryPage(int length, string search);

        Task<List<Employee>> PageList(int pageSize, int pageNumber);

        Task<List<Employee>> GetDeleteListAsync();

        Task<Employee> GetByIdAsync(int employeeId);

        Task<List<Employee>> GetByDepartmentIdAsync(int departmentId);

        Task<List<Employee>> GetEmployeeBySearchAsync(string searchValue);

        Task<List<Employee>> GetEmployeeBySortAsync(string searchValue);

        Task ReomveAsync(Employee employee);

        Task<int> InsertAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);

        Task DeleteList (List<Employee> employees);
    }

}
