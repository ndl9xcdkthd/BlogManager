using BlogManager.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Departments { get; }

        Task<List<Department>> GetListAsync();

        Task<Department> GetByIdAsync(int departmentId);

        Task<int> InserAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);
        
    }
}
