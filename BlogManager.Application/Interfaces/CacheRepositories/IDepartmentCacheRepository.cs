using BlogManager.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Application.Interfaces.CacheRepositories
{
    public interface IDepartmentCacheRepository
    {
        Task<List<Department>> GetCachedListAsync();

        Task<Department> GetByIdAsync(int departmentId);
    }
}
