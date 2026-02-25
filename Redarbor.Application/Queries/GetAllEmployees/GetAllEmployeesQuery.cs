using MediatR;
using Redarbor.Application.DTOs;

namespace Redarbor.Application.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
{
}