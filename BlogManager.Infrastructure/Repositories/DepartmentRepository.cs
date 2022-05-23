using BlogManager.Application.Interfaces.Repositories;
using BlogManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IRepositoryAsync<Department> _repository;
        private readonly IDistributedCache _distributedCache;

        public DepartmentRepository(IDistributedCache distributedCache, IRepositoryAsync<Department> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Department> Departments => _repository.Entities;

        public async Task DeleteAsync(Department department)
        {
            
            await _repository.DeleteAsync(department);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.GetKey(department.Id));
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.GetKey(department.Id));
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            return await _repository.Entities.Where(d => d.Id == departmentId).FirstOrDefaultAsync();
        }

        public Task<List<Department>> GetListActiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Department>> GetListAsync()
        {
            return _repository.Entities.ToListAsync();
        }

        public async Task<int> InserAsync(Department department)
        {
            await _repository.AddAsync(department);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            return department.Id;
        }

        public async Task RemoveAsync(Department department)
        {
            department.Status = "Delete";
            await _repository.UpdateAsync(department);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.GetKey(department.Id));
        }

        public async Task UpdateAsync(Department department)
        {
            await _repository.UpdateAsync(department);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.GetKey(department.Id));
        }
    }
}
