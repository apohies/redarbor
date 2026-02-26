using Moq;
using Redarbor.Application.Commands.CreateEmployee;
using Redarbor.Domain.Entities;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class CreateEmployeeCommandHandlerTests
{
    private readonly Mock<IEmployeeWriteRepository> _writeRepo = new();
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldCreateEmployee_WhenDataIsValid()
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            CompanyId = 1,
            Email     = "test@test.com",
            Password  = "test",
            PortalId  = 1,
            RoleId    = 1,
            StatusId  = 1,
            Username  = "testuser"
        };

        _readRepo.Setup(r => r.ExistsByEmailAsync(command.Email, null)).ReturnsAsync(false);
        _readRepo.Setup(r => r.ExistsByUsernameAsync(command.Username, null)).ReturnsAsync(false);
        _writeRepo.Setup(r => r.CreateAsync(It.IsAny<Employee>())).ReturnsAsync(new Employee
        {
            Id        = 1,
            CompanyId = command.CompanyId,
            Email     = command.Email,
            Password  = command.Password,
            PortalId  = command.PortalId,
            RoleId    = command.RoleId,
            StatusId  = command.StatusId,
            Username  = command.Username
        });

        var handler = new CreateEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("testuser", result.Username);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            CompanyId = 1,
            Email     = "duplicate@test.com",
            Password  = "test",
            PortalId  = 1,
            RoleId    = 1,
            StatusId  = 1,
            Username  = "testuser"
        };

        _readRepo.Setup(r => r.ExistsByEmailAsync(command.Email, null)).ReturnsAsync(true);

        var handler = new CreateEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenUsernameAlreadyExists()
    {
        // Arrange
        var command = new CreateEmployeeCommand
        {
            CompanyId = 1,
            Email     = "test@test.com",
            Password  = "test",
            PortalId  = 1,
            RoleId    = 1,
            StatusId  = 1,
            Username  = "duplicateuser"
        };

        _readRepo.Setup(r => r.ExistsByEmailAsync(command.Email, null)).ReturnsAsync(false);
        _readRepo.Setup(r => r.ExistsByUsernameAsync(command.Username, null)).ReturnsAsync(true);

        var handler = new CreateEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None));
    }
}