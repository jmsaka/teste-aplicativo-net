namespace Clinica.Domain.Profiles;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<UpsertPacienteCommand, PacienteEntity>()
             .ForMember(dest => dest.Atendimentos, opt => opt.Ignore());
    }
}