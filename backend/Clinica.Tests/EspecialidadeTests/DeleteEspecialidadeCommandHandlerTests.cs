public class DeleteEspecialidadeCommandHandlerTests
{
    private readonly Mock<IEspecialidadeRepository> _repositoryMock;
    private readonly DeleteEspecialidadeCommandHandler _handler;

    public DeleteEspecialidadeCommandHandlerTests()
    {
        _repositoryMock = new Mock<IEspecialidadeRepository>();
        _handler = new DeleteEspecialidadeCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeleteExistingEspecialidade_ReturnsSuccess()
    {
        // Arrange
        var especialidade = new EspecialidadeEntity { Id = 1, Nome = "Cardiologia" };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), true)).ReturnsAsync(especialidade);
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        var command = new DeleteEspecialidadeCommand { Id = 1 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Null(result.Result);
        Assert.Empty(result.Message);
        _repositoryMock.Verify(r => r.GetByIdAsync(1, true), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_EspecialidadeNotFound_ReturnsError()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), true)).ReturnsAsync((EspecialidadeEntity?)null!);

        var command = new DeleteEspecialidadeCommand { Id = 999 }; 

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Result);
        Assert.Equal("Especialidade não encontrada.", result.Message);
        _repositoryMock.Verify(r => r.GetByIdAsync(999, true), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_FailedToDeleteEspecialidade_ReturnsError()
    {
        // Arrange
        var especialidade = new EspecialidadeEntity { Id = 1, Nome = "Cardiologia" };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), true)).ReturnsAsync(especialidade);
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

        var command = new DeleteEspecialidadeCommand { Id = 1 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Result);
        Assert.Contains("Especialidade não encontrada.", result.Message);
        _repositoryMock.Verify(r => r.GetByIdAsync(1, true), Times.Once);
        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_ExceptionThrown_ReturnsError()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(),true)).ThrowsAsync(new System.Exception("Erro ao deletar especialidade"));

        var command = new DeleteEspecialidadeCommand { Id = 1 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Result);
        Assert.Contains("Falha ao deletar a Especialidade.", result.Message);
    }
}
