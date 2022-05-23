using BlogManager.Application.Constants;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Commands.Delete;
using BlogManager.Application.Features.Employees.Commands.Update;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetAllDelete;
using BlogManager.Application.Features.Employees.Queries.GetById;
using BlogManager.Web.Abstractions;
using BlogManager.Web.Areas.Delete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManager.Web.Areas.Delete.Controller
{
    [Area("Delete")]
    public class DeleteEmployeeController : BaseController<DeleteEmployeeController>
    {
        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllDeleteQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                return View("Index", viewModel);
            }
            return null;
        }


        [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var departmensResponse = await _mediator.Send(new GetAllDepartmentCacheQuery());


                var response = await _mediator.Send(new GetEmployeeByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var employeeViewModel = _mapper.Map<EmployeeViewModel>(response.Data);
                    if (departmensResponse.Succeeded)
                    {
                        var department = _mapper.Map<List<DepartmentViewModel>>(departmensResponse.Data);
                        employeeViewModel.Departments = new SelectList(department, nameof(DepartmentViewModel.Id), nameof(DepartmentViewModel.Name), null, null);
                    }
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", employeeViewModel) });
                }
                return null;
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {


            var updateEmployeeCommand = _mapper.Map<UpdateEmployeeCommand>(employee);
            var result = await _mediator.Send(updateEmployeeCommand);
            if (result.Succeeded) _notify.Information($"Employee with ID {result.Data} Updated.");

            var response = await _mediator.Send(new GetAllEmployeeCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                var html = await _viewRenderer.RenderViewToStringAsync("Index", viewModel);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                _notify.Error(response.Message);
                return null;
            }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", employee);
                return new JsonResult(new { isValid = false, html = html });
            }
        }


        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteEmployeeSqlCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Employee with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllDeleteQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("Index", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}
