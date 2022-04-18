using BlogManager.Application.Interfaces.CacheRepositories;
using BlogManager.Application.Interfaces.Repositories;
using BlogManager.Domain.Entities.Catalog;
using BlogManager.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlogManager.Infrastructure.CacheRepositories
{
    public class EmployeeCacheRepository : IEmployeeCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCacheRepository(IEmployeeRepository employeeRepository, IDistributedCache distributedCache)
        {
            _employeeRepository = employeeRepository;
            _distributedCache = distributedCache;
        }

        public async Task<List<Employee>> GetByDepartmentId(int departmentId)
        {
            //string cacheKey = EmployeeCacheKeys.ListDepartmentKey(departmentId);
            //var employeeList = await _distributedCache.GetAsync<List<Employee>>(cacheKey);
            //if (employeeList == null)
            //{
            //    employeeList = _employeeRepository.GetByDepartmentIdAsync(departmentId);
            //}
            return null;
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            string cacheKey = EmployeeCacheKeys.GetKey(employeeId);
            var employee = await _distributedCache.GetAsync<Employee>(cacheKey);
            if (employee == null)
            {
                employee = await _employeeRepository.GetByIdAsync(employeeId);
                Throw.Exception.IfNull(employee, "Emplyee", "No Employee Found");
                await _distributedCache.SetAsync(cacheKey, employee);
            }

            return employee;
        }

        public async Task<List<Employee>> GetCachedListAsync()
        {
            string cacheKey = EmployeeCacheKeys.ListKey;
            var employeeList = await _distributedCache.GetAsync<List<Employee>>(cacheKey);
            if(employeeList == null)
            {
                employeeList = await _employeeRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, employeeList);
            }

            return employeeList;
        }
    }
}
