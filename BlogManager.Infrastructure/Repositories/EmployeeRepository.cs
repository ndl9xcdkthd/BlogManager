using BlogManager.Application.Interfaces.Repositories;
using BlogManager.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IRepositoryAsync<Employee> _repository;
        private readonly IDistributedCache _distributedCache;

        public EmployeeRepository(IDistributedCache distributedCache, IRepositoryAsync<Employee> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Employee> Employees => _repository.Entities;

        public async Task DeleteAsync(Employee employee)
        {
            await _repository.DeleteAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.GetKey(employee.Id));
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _repository.Entities.Where(p => p.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetListAsync()
        {
           return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            return employee.Id;
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.GetKey(employee.Id));
        }
    }
}
