using Moq;
using Redarbor.Application.Queries.GetAllEmployees;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class GetAllEmployeesQueryHandlerTests
{
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldReturnAllEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() { Id = 1, Username = "test1", Email = "test1@test.com", Password = "test", CompanyId = 1, PortalId = 1, RoleId = 1, StatusId = 1 },
            new() { Id = 2, Username = "test2", Email = "test2@test.com", Password = "test", CompanyId = 1, PortalId = 1, RoleId = 1, StatusId = 1 }
        };

        _readRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(employees);

        var handler = new GetAllEmployeesQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEmployees()
    {
        // Arrange
        _readRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Employee>());

        var handler = new GetAllEmployeesQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllEmployeesQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}