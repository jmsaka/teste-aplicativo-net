namespace Clinica.Domain.Commands;

public class DeleteAtendimentoCommand : IRequest<BaseResponse<AtendimentoEntity>>
{
    public int Id { get; set; }
}

public class DeleteAtendimentoCommandHandler : IRequestHandler<DeleteAtendimentoCommand, BaseResponse<AtendimentoEntity>>
{
    private readonly IAtendimentoRepository _repository;

    public DeleteAtendimentoCommandHandler(IAtendimentoRepository AtendimentoRepository)
    {
        _repository = AtendimentoRepository;
    }

    private async Task<string> DeleteAsync(DeleteAtendimentoCommand request)
    {
        string hasErrors = string.Empty;

        try
        {
            var Atendimento = await _repository.GetByIdAsync(request.Id);

            if (Atendimento == null)
            {
                hasErrors = "Atendimento não encontrado.";
                return hasErrors;
            }

            var atendimentos = await _repository.GetPacienteTriagemAsync(request.Id);

            if (atendimentos != null && atendimentos.Count != 0)
            {
                hasErrors = "Não é possível deletar o Atendimento. Existem relacionamentos associados.";
                return hasErrors;
            }

            var isDeleted = await _repository.DeleteAsync(request.Id);

            if (!isDeleted)
            {
                hasErrors = "Falha ao deletar o Atendimento.";
            }
        }
        catch (Exception)
        {
            hasErrors = "Falha ao deletar o Atendimento.";
        }

        return hasErrors;
    }

    public async Task<BaseResponse<AtendimentoEntity>> Handle(DeleteAtendimentoCommand request, CancellationToken cancellationToken)
    {
        string hasErrors = await DeleteAsync(request);

        return new BaseResponse<AtendimentoEntity>(null, string.IsNullOrEmpty(hasErrors), hasErrors);
    }
}
