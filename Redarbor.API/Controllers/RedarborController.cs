using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Application.Commands.CreateEmployee;
using Redarbor.Application.Commands.DeleteEmployee;
using Redarbor.Application.Commands.UpdateEmployee;
using Redarbor.Application.Queries.GetAllEmployees;
using Redarbor.Application.Queries.GetEmployeeById;

namespace Redarbor.API.Controllers;

[ApiController]
[Route("api/redarbor")]
public class RedarborController : ControllerBase
{
    private readonly IMediator _mediator;

    public RedarborController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery { Id = id });

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        return NoContent();
    }
}