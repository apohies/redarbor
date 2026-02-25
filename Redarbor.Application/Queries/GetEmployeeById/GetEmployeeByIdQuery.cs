using MediatR;
using Redarbor.Application.DTOs;

namespace Redarbor.Application.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<EmployeeDto?>
{
    public int Id { get; set; }
}