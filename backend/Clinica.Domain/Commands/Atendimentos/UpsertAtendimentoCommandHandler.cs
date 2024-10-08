namespace Clinica.Domain.Commands;

public class UpsertAtendimentoCommand : IRequest<BaseResponse<AtendimentoEntity>>
{
    public int Id { get; set; }
    public int NumeroSequencial { get; set; }
    public DateTime DataHoraChegada { get; set; }
    public required string Status { get; set; }
    public int PacienteId { get; set; }
}

public class UpsertAtendimentoCommandHandler : IRequestHandler<UpsertAtendimentoCommand, BaseResponse<AtendimentoEntity>>
{
    private readonly IAtendimentoRepository _repository;
    private readonly IMapper _mapper;

    public UpsertAtendimentoCommandHandler(IAtendimentoRepository atendimentoRepository, IMapper mapper)
    {
        _repository = atendimentoRepository;
        _mapper = mapper;
    }

    private async Task<BaseResponse<AtendimentoEntity>> Insert(AtendimentoEntity atendimento)
    {
        await _repository.AddAsync(atendimento);
        return new BaseResponse<AtendimentoEntity>(atendimento, true);
    }

    private async Task<BaseResponse<AtendimentoEntity>> Update(AtendimentoEntity atendimento)
    {
        var existingAtendimento = await _repository.GetByIdAsync(atendimento.Id, true);

        if (existingAtendimento is null)
        {
            return new BaseResponse<AtendimentoEntity>(null, false, $"Atendimento {atendimento.Id} não encontrada.");
        }

        await _repository.UpdateAsync(atendimento);
        return new BaseResponse<AtendimentoEntity>(atendimento);
    }

    public async Task<BaseResponse<AtendimentoEntity>> Handle(UpsertAtendimentoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var atendimento = _mapper.Map<AtendimentoEntity>(request);
            return request.Id == 0 ? await Insert(atendimento) : await Update(atendimento);
        }
        catch (Exception ex)
        {
            return new BaseResponse<AtendimentoEntity>(null, false, ex.Message);
        }
    }
}