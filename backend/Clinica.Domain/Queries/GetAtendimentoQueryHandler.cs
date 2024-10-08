namespace Clinica.Domain.Queries;

public class GetAtendimentoQuery : IRequest<BaseResponse<ICollection<AtendimentoDto>>>
{
    public int Id { get; set; }
}

public class GetAtendimentoQueryHandler : IRequestHandler<GetAtendimentoQuery, BaseResponse<ICollection<AtendimentoDto>>>
{
    private readonly IAtendimentoRepository _repository;
    private readonly IMapper _mapper;

    public GetAtendimentoQueryHandler(IAtendimentoRepository AtendimentoRepository, IMapper mapper)
    {
        _repository = AtendimentoRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ICollection<AtendimentoDto>>> Handle(GetAtendimentoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request == null || request.Id == 0)
            {
                return new BaseResponse<ICollection<AtendimentoDto>>(await _repository.GetAllEntityAsync());
            }

            var atendimento = _mapper.Map<AtendimentoDto>(await _repository.GetByIdAsync(request.Id));

            if (atendimento == null)
            {
                return new BaseResponse<ICollection<AtendimentoDto>>(null, false, $"Atendimento com ID {request.Id} não encontrado.");
            }

            return new BaseResponse<ICollection<AtendimentoDto>>([atendimento]);
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<AtendimentoDto>>(null, false, ex.Message);
        }
    }
}
