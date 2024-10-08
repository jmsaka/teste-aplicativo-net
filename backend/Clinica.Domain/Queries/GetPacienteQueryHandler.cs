namespace Clinica.Domain.Queries
{
    public class GetPacienteQuery : IRequest<BaseResponse<ICollection<PacienteEntity>>>
    {
        public int Id { get; set; }
    }

    public class GetPacienteQueryHandler : IRequestHandler<GetPacienteQuery, BaseResponse<ICollection<PacienteEntity>>>
    {
        private readonly IPacienteRepository _repository;

        public GetPacienteQueryHandler(IPacienteRepository pacienteRepository)
        {
            _repository = pacienteRepository;
        }

        public async Task<BaseResponse<ICollection<PacienteEntity>>> Handle(GetPacienteQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.Id == 0)
                {
                    return new BaseResponse<ICollection<PacienteEntity>>(await _repository.GetAllAsync());
                }

                var paciente = await _repository.GetByIdAsync(request.Id);

                if (paciente == null)
                {
                    return new BaseResponse<ICollection<PacienteEntity>>(null, false, $"Paciente com ID {request.Id} não encontrado.");
                }

                return new BaseResponse<ICollection<PacienteEntity>>([paciente]);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ICollection<PacienteEntity>>(null, false, ex.Message);
            }
        }
    }
}
