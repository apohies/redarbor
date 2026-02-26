using Moq;
using Redarbor.Application.Commands.DeleteEmployee;
using Redarbor.Domain.Interfaces;
using Xunit;

namespace Redarbor.Tests.Handlers;

public class DeleteEmployeeCommandHandlerTests
{
    private readonly Mock<IEmployeeWriteRepository> _writeRepo = new();
    private readonly Mock<IEmployeeReadRepository> _readRepo = new();

    [Fact]
    public async Task Handle_ShouldDeleteEmployee_WhenExists()
    {
        // Arrange
        var command = new DeleteEmployeeCommand { Id = 1 };

        _readRepo.Setup(r => r.ExistsByIdAsync(1)).ReturnsAsync(true);
        _writeRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        var handler = new DeleteEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _writeRepo.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenEmployeeNotFound()
    {
        // Arrange
        var command = new DeleteEmployeeCommand { Id = 99999 };

        _readRepo.Setup(r => r.ExistsByIdAsync(99999)).ReturnsAsync(false);

        var handler = new DeleteEmployeeCommandHandler(_writeRepo.Object, _readRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => handler.Handle(command, CancellationToken.None));
    }
}