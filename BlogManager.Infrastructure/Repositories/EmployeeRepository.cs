using BlogManager.Application.Extensions;
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

        public Task DeleteList(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Employee>> GetActiveListAsync()
        {
            return await _repository.Entities.Where(p => p.Status == "Active" || p.Status == "In Active").ToListAsync();
        }

        public Task<List<Employee>> GetActiveListAsync(string sortColumn, string sortColumnDirection)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _repository.Entities.Where(e => e.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _repository.Entities.Where(p => p.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetDeleteListAsync()
        {
            return await _repository.Entities.Where(p => p.Status == "Delete").ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeeBySearchAsync(string searchValue)
        {
            return await _repository.Entities.Where(e => e.FristName.Contains(searchValue)
                                                      || e.LastName.Contains(searchValue)
                                                      || e.Position.Contains(searchValue)).ToListAsync();
        }

        public Task<List<Employee>> GetEmployeeBySortAsync(string searchValue)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Employee>> GetListAsync()
        {
            
            return await _repository.Entities.ToListAsync();
        }

        public async Task<List<Employee>> GetListPageAsync(DataTableAjaxPostModel model)
        {
            var take = model.length;
            var skip = model.start;

            var listEmployee = await _repository.GetAllAsync();


            var page = await _repository.Entities.Skip(skip).Take(take).ToListAsync();

            return page;
        }

        public Task<List<Employee>> GetQueryPage(int length, string search)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            return employee.Id;
        }

        public Task<List<Employee>> PageList(int pageSize, int pageNumber)
        {
            throw new System.NotImplementedException();
        }

        public async Task ReomveAsync(Employee employee)
        {
            employee.Status = "Delete";
            await _repository.UpdateAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.GetKey(employee.Id));
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.EmployeeCacheKeys.GetKey(employee.Id));
        }
    }
}
