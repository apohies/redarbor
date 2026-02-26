using MediatR;
using Redarbor.Domain.Entities;

namespace Redarbor.Application.Queries.GetEmployeeByCredentials;

public class GetEmployeeByCredentialsQuery : IRequest<Employee?>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}