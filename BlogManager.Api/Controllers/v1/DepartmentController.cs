using BlogManager.API.Controllers;
using BlogManager.Application.Features.Departments.Commands.Create;
using BlogManager.Application.Features.Departments.Commands.Delete;
using BlogManager.Application.Features.Departments.Commands.Update;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Departments.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogManager.Api.Controllers.v1
{
    public class DepartmentController : BaseApiController<DepartmentController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var department = await _mediator.Send(new GetAllDepartmentCacheQuery());

            return Ok(department);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDepartmentCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDepartmentCommand { Id = id }));
        }
    }
}
