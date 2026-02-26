using Moq;
using Redarbor.Application.Queries.GetEmployeeByCredentials;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class GetEmployeeByCredentialsQueryHandlerTests
{
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldReturnEmployee_WhenCredentialsAreValid()
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

        _readRepo.Setup(r => r.GetByUsernameAndPasswordAsync("test1", "test"))
            .ReturnsAsync(employee);

        var handler = new GetEmployeeByCredentialsQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetEmployeeByCredentialsQuery
        {
            Username = "test1",
            Password = "test"
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test1", result.Username);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenCredentialsAreInvalid()
    {
        // Arrange
        _readRepo.Setup(r => r.GetByUsernameAndPasswordAsync("wronguser", "wrongpass"))
            .ReturnsAsync((Employee?)null);

        var handler = new GetEmployeeByCredentialsQueryHandler(_readRepo.Object);

        // Act
        var result = await handler.Handle(new GetEmployeeByCredentialsQuery
        {
            Username = "wronguser",
            Password = "wrongpass"
        }, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}