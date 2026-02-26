using Moq;
using Redarbor.Application.Commands.UpdateEmployee;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class UpdateEmployeeCommandHandlerTests
{
    private readonly Mock<IEmployeeWriteRepository> _writeRepo = new();
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldUpdateEmployee_WhenDataIsValid()
    {
        // Arrange
        var command = new UpdateEmployeeCommand
        {
            Id        = 1,
            CompanyId = 1,
            Email     = "updated@test.com",
            Password  = "test",
            PortalId  = 1,
            RoleId    = 1,
            StatusId  = 1,
            Username  = "test1updated"
        };

        _readRepo.Setup(r => r.ExistsByIdAsync(1)).ReturnsAsync(true);
        _readRepo.Setup(r => r.ExistsByEmailAsync(command.Email, 1)).ReturnsAsync(false);
        _readRepo.Setup(r => r.ExistsByUsernameAsync(command.Username, 1)).ReturnsAsync(false);
        _writeRepo.Setup(r => r.UpdateAsync(It.IsAny<Redarbor.Domain.Entities.Employee>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _writeRepo.Verify(r => r.UpdateAsync(It.IsAny<Redarbor.Domain.Entities.Employee>()), Times.Once);
    }

    
   
}