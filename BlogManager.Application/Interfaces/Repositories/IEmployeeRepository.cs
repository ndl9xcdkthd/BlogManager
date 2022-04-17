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

        Task<List<Employee>> GetListAsync();

        Task<Employee> GetByIdAsync(int employeeId);

        Task<int> InsertAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);
    }

}
