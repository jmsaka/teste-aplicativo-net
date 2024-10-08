namespace Clinica.Domain.Profiles;

public class AtendimentoProfile : Profile
{
    public AtendimentoProfile()
    {
        CreateMap<UpsertAtendimentoCommand, AtendimentoEntity>()
            .ForMember(dest => dest.Paciente, opt => opt.Ignore())
            .ForMember(dest => dest.Triagem, opt => opt.Ignore());

        CreateMap<AtendimentoEntity, UpsertAtendimentoCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PacienteId, opt => opt.MapFrom(src => src.PacienteId));

        CreateMap<AtendimentoEntity, AtendimentoDto>()
            .ForMember(dest => dest.TriagemId, opt => opt.Ignore())
            .ForMember(dest => dest.PacienteId, opt => opt.MapFrom(src => src.PacienteId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DataHoraChegada, opt => opt.MapFrom(src => src.DataHoraChegada))
            .ForMember(dest => dest.NumeroSequencial, opt => opt.MapFrom(src => src.NumeroSequencial));
    }
}
