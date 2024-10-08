public class UpsertEspecialidadeCommandHandlerTests
{
    private readonly Mock<IEspecialidadeRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpsertEspecialidadeCommandHandler _handler;

    public UpsertEspecialidadeCommandHandlerTests()
    {
        _repositoryMock = new Mock<IEspecialidadeRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpsertEspecialidadeCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_InsertNewEspecialidade_ReturnsSuccess()
    {
        // Arrange
        var command = new UpsertEspecialidadeCommand
        {
            Id = 0,
            Nome = "Cardiologia"
        };

        var especialidadeEntity = new EspecialidadeEntity { Id = 0, Nome = "Cardiologia" };
        _mapperMock.Setup(m => m.Map<EspecialidadeEntity>(It.IsAny<UpsertEspecialidadeCommand>())).Returns(especialidadeEntity);
        _repositoryMock.Setup(r => r.TermExists(It.IsAny<string>())).ReturnsAsync(false);
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<EspecialidadeEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(especialidadeEntity, result.Result);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<EspecialidadeEntity>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<EspecialidadeEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_InsertDuplicateEspecialidade_ReturnsError()
    {
        // Arrange
        var command = new UpsertEspecialidadeCommand
        {
            Id = 0,
            Nome = "Cardiologia"
        };

        _repositoryMock.Setup(r => r.TermExists(It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Falha na operação:", result.Message);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<EspecialidadeEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UpdateExistingEspecialidade_ReturnsSuccess()
    {
        // Arrange
        var command = new UpsertEspecialidadeCommand
        {
            Id = 1,
            Nome = "Neurologia"
        };

        var especialidadeEntity = new EspecialidadeEntity { Id = 1, Nome = "Neurologia" };
        _mapperMock.Setup(m => m.Map<EspecialidadeEntity>(It.IsAny<UpsertEspecialidadeCommand>())).Returns(especialidadeEntity);
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(especialidadeEntity);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<EspecialidadeEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(especialidadeEntity, result.Result);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<EspecialidadeEntity>()), Times.Once);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<EspecialidadeEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UpdateNonExistingEspecialidade_ReturnsError()
    {
        // Arrange
        var command = new UpsertEspecialidadeCommand
        {
            Id = 1,
            Nome = "Odontologia"
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((EspecialidadeEntity)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Falha na operação:", result.Message);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<EspecialidadeEntity>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ExceptionThrown_ReturnsError()
    {
        // Arrange
        var command = new UpsertEspecialidadeCommand
        {
            Id = 1,
            Nome = "Ginecologia"
        };

        var especialidadeEntity = new EspecialidadeEntity { Id = 1, Nome = "Ginecologia" };
        _mapperMock.Setup(m => m.Map<EspecialidadeEntity>(It.IsAny<UpsertEspecialidadeCommand>())).Returns(especialidadeEntity);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<EspecialidadeEntity>())).Throws(new Exception("Erro ao atualizar a especialidade"));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal($"Especialidade {command.Nome} não encontrada.", result.Message);
    }
}
