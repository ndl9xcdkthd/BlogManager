using BlogManager.API.Controllers;
using BlogManager.Application.Features.Employees.Commands.Create;
using BlogManager.Application.Features.Employees.Commands.Delete;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogManager.Api.Controllers.v1
{
    public class EmployeeController : BaseApiController<EmployeeController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employee = await _mediator.Send(new GetAllEmployeeCachedQuery());

            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery() { Id = id });

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post (CreateEmployeeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CreateEmployeeCommand command)
        {
            if (id != 0)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand { Id = id }));
        }
    }
}
