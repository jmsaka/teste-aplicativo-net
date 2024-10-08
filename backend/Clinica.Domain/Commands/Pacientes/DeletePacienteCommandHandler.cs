namespace Clinica.Domain.Commands;

public class DeletePacienteCommand : IRequest<BaseResponse<PacienteEntity>>
{
    public int Id { get; set; }
}

public class DeletePacienteCommandHandler : IRequestHandler<DeletePacienteCommand, BaseResponse<PacienteEntity>>
{
    private readonly IPacienteRepository _repository;

    public DeletePacienteCommandHandler(IPacienteRepository pacienteRepository)
    {
        _repository = pacienteRepository;
    }

    private async Task<string> DeleteAsync(DeletePacienteCommand request)
    {
        string hasErrors = string.Empty;

        try
        {
            var paciente = await _repository.GetByIdAsync(request.Id);

            if (paciente == null)
            {
                hasErrors = "Paciente não encontrado.";
                return hasErrors;
            }

            var atendimentos = await _repository.GetPacienteAtendimentoAsync(request.Id);

            if (atendimentos != null && atendimentos.Count != 0)
            {
                hasErrors = "Não é possível deletar o paciente. Existem relacionamentos associados.";
                return hasErrors;
            }

            var isDeleted = await _repository.DeleteAsync(request.Id);

            if (!isDeleted)
            {
                hasErrors = "Falha ao deletar o paciente.";
            }
        }
        catch (Exception)
        {
            hasErrors = "Falha ao deletar o paciente.";
        }

        return hasErrors;
    }

    public async Task<BaseResponse<PacienteEntity>> Handle(DeletePacienteCommand request, CancellationToken cancellationToken)
    {
        string hasErrors = await DeleteAsync(request);

        return new BaseResponse<PacienteEntity>(null, string.IsNullOrEmpty(hasErrors), hasErrors);
    }
}
