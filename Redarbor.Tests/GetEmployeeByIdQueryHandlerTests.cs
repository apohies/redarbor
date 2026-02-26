using Moq;
using Redarbor.Application.Queries.GetEmployeeById;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class GetEmployeeByIdQueryHandlerTests
{
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldReturnEmployee_WhenExists()
    {
        // Arrange
        var employee = new Employee
        {
            Id        = 1,
            Username  = "test1",
            Email     = "test1@test.com",
            Password  = "test",
            CompanyId = 1,
            PortalId  = 1,
            RoleId    = 1,
            StatusId  = 1
        };

        _readRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(employee);

        var handler = new GetEmployeeByIdQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetEmployeeByIdQuery { Id = 1 }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("test1", result.Username);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        _readRepo.Setup(r => r.GetByIdAsync(99999)).ReturnsAsync((Employee?)null);

        var handler = new GetEmployeeByIdQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetEmployeeByIdQuery { Id = 99999 }, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}