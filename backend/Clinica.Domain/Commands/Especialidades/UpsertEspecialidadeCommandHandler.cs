namespace Clinica.Domain.Commands;

public class UpsertEspecialidadeCommand : IRequest<BaseResponse<EspecialidadeEntity>>
{
    public int Id { get; set; }
    public required string Nome { get; set; }
}

public class UpsertEspecialidadeCommandHandler : IRequestHandler<UpsertEspecialidadeCommand, BaseResponse<EspecialidadeEntity>>
{
    private readonly IEspecialidadeRepository _repository;
    private readonly IMapper _mapper;

    public UpsertEspecialidadeCommandHandler(IEspecialidadeRepository especialidadeRepository, IMapper mapper)
    {
        _repository = especialidadeRepository;
        _mapper = mapper;
    }

    private async Task<BaseResponse<EspecialidadeEntity>> Insert(EspecialidadeEntity especialidade)
    {
        var existingEspecialidade = await _repository.TermExists(especialidade.Nome);

        if (existingEspecialidade)
        {
            return new BaseResponse<EspecialidadeEntity>(null, false, $"Especialidade {especialidade.Nome} já existe.");
        }

        await _repository.AddAsync(especialidade);
        return new BaseResponse<EspecialidadeEntity>(especialidade, true);
    }

    private async Task<BaseResponse<EspecialidadeEntity>> Update(EspecialidadeEntity especialidade)
    {
        var existingEspecialidade = await _repository.GetByIdAsync(especialidade.Id, true);

        if (existingEspecialidade is null)
        {
            return new BaseResponse<EspecialidadeEntity>(null, false, $"Especialidade {especialidade.Nome} não encontrada.");
        }

        await _repository.UpdateAsync(especialidade);
        return new BaseResponse<EspecialidadeEntity>(especialidade);
    }

    public async Task<BaseResponse<EspecialidadeEntity>> Handle(UpsertEspecialidadeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var especialidade = _mapper.Map<EspecialidadeEntity>(request);
            return request.Id == 0 ? await Insert(especialidade) : await Update(especialidade);
        }
        catch (Exception ex)
        {
            return new BaseResponse<EspecialidadeEntity>(null, false, $"Falha na operação: {ex.Message}");
        }
    }
}
