using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManager.Infrastructure.CacheKeys
{
    public static class EmployeeCacheKeys
    {
        public static string ListKey => "EmployeeList";
        public static string ListDepartmentKey(int departmentId) => $"EmployeeList-{departmentId}";

        public static string SelectListKey => "EmployeeSelectList";

        public static string GetKey(int employeeId) => $"Employee-{employeeId}";

        public static string GetDetailsKey(int employeeId) => $"EmployeeDetails-{employeeId}";
    }
}
