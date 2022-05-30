using BlogManager.Application.Constants;
using BlogManager.Application.Extensions;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Commands.Create;
using BlogManager.Application.Features.Employees.Commands.Delete;
using BlogManager.Application.Features.Employees.Commands.Update;
using BlogManager.Application.Features.Employees.Queries.GetAllActive;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetAllDelete;
using BlogManager.Application.Features.Employees.Queries.GetAllJs;
using BlogManager.Application.Features.Employees.Queries.GetByDepartmentId;
using BlogManager.Application.Features.Employees.Queries.GetById;
using BlogManager.Web.Abstractions;
using BlogManager.Web.Areas.Office.Models;
using BlogManager.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManager.Web.Areas.Office.Controller
{
    [Area("Office")]
    public class EmployeeController : BaseController<EmployeeController>
    {
        public IActionResult Index()
        {
            var model = new EmployeeViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllActiveQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        [Authorize(Policy = Permissions.Users.View)]
        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var departmensResponse = await _mediator.Send(new GetAllDepartmentCacheQuery());
            
            if (id == 0)
            {
                var employeeViewModel = new EmployeeViewModel();
                if (departmensResponse.Succeeded)
                {
                    var departmentViewModel = _mapper.Map<List<DepartmentViewModel>>(departmensResponse.Data);
                    employeeViewModel.Departments = new SelectList(departmentViewModel, nameof(DepartmentViewModel.Id), nameof(DepartmentViewModel.Name), null, null);
                }
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", employeeViewModel) });
            }
            else
            {
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
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createEmployeeCommand = _mapper.Map<CreateEmployeeCommand>(employee);
                    var result = await _mediator.Send(createEmployeeCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Employee with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateEmployeeCommand = _mapper.Map<UpdateEmployeeCommand>(employee);
                    var result = await _mediator.Send(updateEmployeeCommand);
                    if (result.Succeeded) _notify.Information($"Employee with ID {result.Data} Updated.");
                }
                if(Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    var image = file.OptimizeImageSize(700, 700);
                    await _mediator.Send(new UpdateEmployeeImageCommand() { Id = id, Image = image });
                }
                var response = await _mediator.Send(new GetAllActiveQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
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
        public async Task<JsonResult> GetEmployeesAsync(DataTableAjaxPostModel model)
        {
            var response = await _mediator.Send(new GetAllJsQuery() { model = model});

            var viewmodel = _mapper.Map<List<EmployeeViewModel>>(response.Item1);
                 

            return Json(new
            {
                draw = model.draw,
                recordsTotal = response.Item2,
                recordsFiltered = response.Item2,
                data = viewmodel
            });
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Employee with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllActiveQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<EmployeeViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
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

