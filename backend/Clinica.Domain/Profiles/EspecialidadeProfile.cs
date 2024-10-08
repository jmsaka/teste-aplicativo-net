namespace Clinica.Domain.Profiles;

public class EspecialidadeProfile : Profile
{
    public EspecialidadeProfile()
    {
        CreateMap<UpsertEspecialidadeCommand, EspecialidadeEntity>();
    }
}