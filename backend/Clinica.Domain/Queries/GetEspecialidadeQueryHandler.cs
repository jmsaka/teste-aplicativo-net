namespace Clinica.Domain.Queries;

public class GetEspecialidadeQuery : IRequest<BaseResponse<ICollection<EspecialidadeEntity>>>
{
    public int Id { get; set; }
}

public class GetEspecialidadeQueryHandler : IRequestHandler<GetEspecialidadeQuery, BaseResponse<ICollection<EspecialidadeEntity>>>
{
    private readonly IEspecialidadeRepository _repository;

    public GetEspecialidadeQueryHandler(IEspecialidadeRepository especialidadeRepository)
    {
        _repository = especialidadeRepository;
    }

    public async Task<BaseResponse<ICollection<EspecialidadeEntity>>> Handle(GetEspecialidadeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request == null || request.Id == 0)
            {
                return new BaseResponse<ICollection<EspecialidadeEntity>>(await _repository.GetAllAsync());
            }

            var especialidade = await _repository.GetByIdAsync(request.Id, true);

            if (especialidade == null)
            {
                return new BaseResponse<ICollection<EspecialidadeEntity>>(null, false, $"Especialidade com ID {request.Id} não encontrada.");
            }

            return new BaseResponse<ICollection<EspecialidadeEntity>>([especialidade]);
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<EspecialidadeEntity>>(null, false, ex.Message);
        }
    }
}