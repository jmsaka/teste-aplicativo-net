namespace Clinica.Domain.Commands;

public class DeleteTriagemCommand : IRequest<BaseResponse<TriagemEntity>>
{
    public int Id { get; set; }
}

public class DeleteTriagemCommandHandler : IRequestHandler<DeleteTriagemCommand, BaseResponse<TriagemEntity>>
{
    private readonly ITriagemRepository _repository;

    public DeleteTriagemCommandHandler(ITriagemRepository TriagemRepository)
    {
        _repository = TriagemRepository;
    }

    private async Task<string> DeleteAsync(DeleteTriagemCommand request)
    {
        string hasErrors = string.Empty;

        try
        {
            var Triagem = await _repository.GetByIdAsync(request.Id);

            if (Triagem == null)
            {
                hasErrors = "Triagem não encontrado.";
                return hasErrors;
            }

            var triagens = await _repository.GetEspecialidadeAtendimentoAsync(request.Id);

            if (triagens != null && triagens.Count != 0)
            {
                hasErrors = "Não é possível deletar o Triagem. Existem relacionamentos associados.";
                return hasErrors;
            }

            var isDeleted = await _repository.DeleteAsync(request.Id);

            if (!isDeleted)
            {
                hasErrors = "Falha ao deletar o Triagem.";
            }
        }
        catch (Exception)
        {
            hasErrors = "Falha ao deletar o Triagem.";
        }

        return hasErrors;
    }

    public async Task<BaseResponse<TriagemEntity>> Handle(DeleteTriagemCommand request, CancellationToken cancellationToken)
    {
        string hasErrors = await DeleteAsync(request);

        return new BaseResponse<TriagemEntity>(null, string.IsNullOrEmpty(hasErrors), hasErrors);
    }
}
