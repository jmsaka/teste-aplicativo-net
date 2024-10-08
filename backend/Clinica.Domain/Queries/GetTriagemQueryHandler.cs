namespace Clinica.Domain.Queries
{
    public class GetTriagemQuery : IRequest<BaseResponse<ICollection<TriagemEntity>>>
    {
        public int Id { get; set; }
    }

    public class GetTriagemQueryHandler : IRequestHandler<GetTriagemQuery, BaseResponse<ICollection<TriagemEntity>>>
    {
        private readonly ITriagemRepository _repository;

        public GetTriagemQueryHandler(ITriagemRepository TriagemRepository)
        {
            _repository = TriagemRepository;
        }

        public async Task<BaseResponse<ICollection<TriagemEntity>>> Handle(GetTriagemQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Id == 0)
                {
                    return new BaseResponse<ICollection<TriagemEntity>>(await _repository.GetAllAsync());
                }

                var Triagem = await _repository.GetByIdAsync(request.Id);

                if (Triagem == null)
                {
                    return new BaseResponse<ICollection<TriagemEntity>>(null, false, $"Triagem com ID {request.Id} não encontrado.");
                }

                return new BaseResponse<ICollection<TriagemEntity>>([Triagem]);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ICollection<TriagemEntity>>(null, false, ex.Message);
            }
        }
    }
}
