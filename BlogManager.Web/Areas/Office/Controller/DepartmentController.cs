using BlogManager.Application.Features.Departments.Commands.Create;
using BlogManager.Application.Features.Departments.Commands.Delete;
using BlogManager.Application.Features.Departments.Commands.Update;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Departments.Queries.GetById;
using BlogManager.Web.Abstractions;
using BlogManager.Web.Areas.Office.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManager.Web.Areas.Office.Controller
{
    [Area("Office")]
    public class DepartmentController : BaseController<DepartmentController>
    {
        public IActionResult Index()
        {
            var model = new DepartmentViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllDepartmentCacheQuery());
            if (response.Succeeded)
            {
                var viewmodel = _mapper.Map<List<DepartmentViewModel>>(response.Data);
                return PartialView("_ViewAll", viewmodel);
            }

            return null;
        }

        public async Task<JsonResult> OnGetCrateOrEdit(int id = 0)
        {
            var departmentResponse = await _mediator.Send(new GetAllDepartmentCacheQuery());
            if(id == 0)
            {
                var departmentViewModel = new DepartmentViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", departmentViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id});
                if (response.Succeeded)
                {
                    var departmentViewModel = _mapper.Map<DepartmentViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", departmentViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    var createBarandCommand = _mapper.Map<CreateDepartmentCommand>(department);
                    var result = await _mediator.Send(createBarandCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Department with Id {result.Data} created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateDepartmentCommand = _mapper.Map<UpdateDepartmentCommand>(department);
                    var result = await _mediator.Send(updateDepartmentCommand);
                    if (result.Succeeded) _notify.Information($"Department with id {result.Data} updated.");
                }

                var reponse = await _mediator.Send(new GetAllDepartmentCacheQuery());

                if (reponse.Succeeded)
                {
                    var viewModel = _mapper.Map<List<DepartmentViewModel>>(reponse.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new {isValid = true, html = html});
                }

                else
                {
                    _notify.Error(reponse.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", department);
                return new JsonResult(new {isValid = false, html =html});
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteDepartmentCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Brand with Id {id} deleted.");
                var response = await _mediator.Send(new GetAllDepartmentCacheQuery());
                if(response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<DepartmentViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll",viewModel);
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
