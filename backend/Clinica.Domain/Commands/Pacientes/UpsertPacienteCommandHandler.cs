namespace Clinica.Domain.Commands;

public class UpsertPacienteCommand : IRequest<BaseResponse<PacienteEntity>>
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Sexo { get; set; }
    public string? Email { get; set; }
}

public class UpsertPacienteCommandHandler : IRequestHandler<UpsertPacienteCommand, BaseResponse<PacienteEntity>>
{
    private readonly IPacienteRepository _repository;
    private readonly IMapper _mapper;

    public UpsertPacienteCommandHandler(IPacienteRepository pacienteRepository, IMapper mapper)
    {
        _repository = pacienteRepository;
        _mapper = mapper;
    }

    private async Task<BaseResponse<PacienteEntity>> Insert(PacienteEntity paciente)
    {
        await _repository.AddAsync(paciente);
        return new BaseResponse<PacienteEntity>(paciente, true);
    }

    private async Task<BaseResponse<PacienteEntity>> Update(PacienteEntity paciente)
    {
        var existingPaciente = await _repository.GetByIdAsync(paciente.Id, true);

        if (existingPaciente is null)
        {
            return new BaseResponse<PacienteEntity>(null, false, $"Paciente com ID {paciente.Id} não encontrado.");
        }

        await _repository.UpdateAsync(paciente);
        return new BaseResponse<PacienteEntity>(existingPaciente);
    }

    public async Task<BaseResponse<PacienteEntity>> Handle(UpsertPacienteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var paciente = _mapper.Map<PacienteEntity>(request);
            return request.Id == 0 ? await Insert(paciente) : await Update(paciente);
        }
        catch (Exception ex)
        {
            return new BaseResponse<PacienteEntity>(null, false, $"Falha na operação: {ex.Message}");
        }
    }
}