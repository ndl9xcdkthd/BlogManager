using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Web.Areas.Delete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManager.Web.Controllers
{
    public class JsonNotAuthen : BaseController<JsonNotAuthen>
    {

        public async Task<JsonResult> GetEmployeesAsync()
        {
            var response = await _mediator.Send(new GetAllEmployeeCachedQuery());
            if (response.Succeeded)
            {
                var viewmodel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                return new JsonResult(viewmodel);
            }

            return null;
        }
    }
}
