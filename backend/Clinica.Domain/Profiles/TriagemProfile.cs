namespace Clinica.Domain.Profiles;

public class TriagemProfile : Profile
{
    public TriagemProfile()
    {
        CreateMap<UpsertTriagemCommand, TriagemEntity>()
            .ForMember(dest => dest.Especialidade, opt => opt.Ignore())
            .ForMember(dest => dest.Atendimento, opt => opt.Ignore());

        CreateMap<TriagemEntity, UpsertTriagemCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EspecialidadeId, opt => opt.MapFrom(src => src.EspecialidadeId))
            .ForMember(dest => dest.AtendimentoId, opt => opt.MapFrom(src => src.AtendimentoId));
    }
}
