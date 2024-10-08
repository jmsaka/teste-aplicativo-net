namespace Clinica.Domain.Commands;

public class UpsertTriagemCommand : IRequest<BaseResponse<TriagemEntity>>
{
    public int Id { get; set; }
    public required string Sintoma { get; set; }
    public required string PressaoArterial { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }
    public int EspecialidadeId { get; set; }
    public int AtendimentoId { get; set; }
}

public class UpsertTriagemCommandHandler : IRequestHandler<UpsertTriagemCommand, BaseResponse<TriagemEntity>>
{
    private readonly ITriagemRepository _repository;
    private readonly IMapper _mapper;

    public UpsertTriagemCommandHandler(ITriagemRepository triagemRepository, IMapper mapper)
    {
        _repository = triagemRepository;
        _mapper = mapper;
    }

    private async Task<BaseResponse<TriagemEntity>> Insert(TriagemEntity triagem)
    {
        try
        {
            await _repository.AddAsync(triagem);
            return new BaseResponse<TriagemEntity>(triagem, true);
        }
        catch (Exception e)
        {
            return new BaseResponse<TriagemEntity>(null, false, JsonConvert.SerializeObject(e));
        }

    }

    private async Task<BaseResponse<TriagemEntity>> Update(TriagemEntity triagem)
    {
        var existingtriagem = await _repository.GetByIdAsync(triagem.Id, true);

        if (existingtriagem is null)
        {
            return new BaseResponse<TriagemEntity>(null, false, $"triagem com ID {triagem.Id} não encontrado.");
        }

        await _repository.UpdateAsync(triagem);
        return new BaseResponse<TriagemEntity>(triagem);
    }

    public async Task<BaseResponse<TriagemEntity>> Handle(UpsertTriagemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var triagem = _mapper.Map<TriagemEntity>(request);
            return request.Id == 0 ? await Insert(triagem) : await Update(triagem);
        }
        catch (Exception ex)
        {
            return new BaseResponse<TriagemEntity>(null, false, ex.Message);
        }
    }
}
