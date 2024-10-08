namespace Clinica.Domain.Commands;

public class DeleteEspecialidadeCommand : IRequest<BaseResponse<EspecialidadeEntity>>
{
    public int Id { get; set; }
}

public class DeleteEspecialidadeCommandHandler : IRequestHandler<DeleteEspecialidadeCommand, BaseResponse<EspecialidadeEntity>>
{
    private readonly IEspecialidadeRepository _repository;

    public DeleteEspecialidadeCommandHandler(IEspecialidadeRepository especialidadeRepository)
    {
        _repository = especialidadeRepository;
    }

    private async Task<string> DeleteAsync(DeleteEspecialidadeCommand request)
    {
        string hasErrors = string.Empty;

        try
        {
            var especialidade = await _repository.GetByIdAsync(request.Id, true);

            if (especialidade is null)
            {
                hasErrors = "Especialidade não encontrada.";
            }
            else
            {
                var isDeleted = await _repository.DeleteAsync(request.Id);

                if (!isDeleted)
                {
                    hasErrors = "Especialidade não encontrada.";
                }
            }
        }
        catch (Exception)
        {
            hasErrors = "Falha ao deletar a Especialidade.";
        }

        return hasErrors;
    }

    public async Task<BaseResponse<EspecialidadeEntity>> Handle(DeleteEspecialidadeCommand request, CancellationToken cancellationToken)
    {
        string hasErrors = await DeleteAsync(request);

        return new BaseResponse<EspecialidadeEntity>(null, string.IsNullOrEmpty(hasErrors), hasErrors);
    }
}
