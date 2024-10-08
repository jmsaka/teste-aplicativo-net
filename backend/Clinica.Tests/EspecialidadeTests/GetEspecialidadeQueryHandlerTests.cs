public class GetEspecialidadeQueryHandlerTests
{
    private readonly Mock<IEspecialidadeRepository> _repositoryMock;
    private readonly GetEspecialidadeQueryHandler _handler;

    public GetEspecialidadeQueryHandlerTests()
    {
        _repositoryMock = new Mock<IEspecialidadeRepository>();
        _handler = new GetEspecialidadeQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_GetAllEspecialidades_ReturnsAllEspecialidades()
    {
        // Arrange
        var especialidades = new List<EspecialidadeEntity>
        {
            new EspecialidadeEntity { Id = 1, Nome = "Cardiologia" },
            new EspecialidadeEntity { Id = 2, Nome = "Neurologia" }
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(especialidades);

        var query = new GetEspecialidadeQuery { Id = 0 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Equal(2, result.Result.Count);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_GetEspecialidadeById_ReturnsSingleEspecialidade()
    {
        // Arrange
        var especialidade = new EspecialidadeEntity { Id = 1, Nome = "Cardiologia" };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(especialidade);

        var query = new GetEspecialidadeQuery { Id = 1 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Result);
        Assert.Single(result.Result); 
        Assert.Equal(especialidade.Id, result.Result!.First().Id); 
        _repositoryMock.Verify(r => r.GetByIdAsync(1, true), Times.Once);
    }


    [Fact]
    public async Task Handle_GetEspecialidadeById_NotFound_ReturnsError()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((EspecialidadeEntity)null!);

        var query = new GetEspecialidadeQuery { Id = 999 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Result);
        Assert.Equal($"Especialidade com ID {query.Id} não encontrada.", result.Message);
        _repositoryMock.Verify(r => r.GetByIdAsync(999, true), Times.Once);
    }

    [Fact]
    public async Task Handle_ExceptionThrown_ReturnsError()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new System.Exception("Erro ao buscar especialidades"));

        var query = new GetEspecialidadeQuery { Id = 0 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Result);
        Assert.Equal("Erro ao buscar especialidades", result.Message);
    }
}
