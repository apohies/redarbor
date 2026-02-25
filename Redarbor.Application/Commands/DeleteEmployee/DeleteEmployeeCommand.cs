using MediatR;

namespace Redarbor.Application.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest
{
    public int Id { get; set; }
}