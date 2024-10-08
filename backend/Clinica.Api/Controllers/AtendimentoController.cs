namespace Clinica.Api.Controllers;

[Route("api/[controller]")]
public class AtendimentoController : BaseController
{
    public AtendimentoController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<ICollection<AtendimentoDto>>>> Get([FromQuery] int id)
    {
        return await HandleQuery(new GetAtendimentoQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<AtendimentoEntity>>> Post([FromBody] UpsertAtendimentoCommand command)
    {
        return await HandleCommand(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<AtendimentoEntity>>> Delete(int id)
    {
        return await HandleCommand(new DeleteAtendimentoCommand() { Id = id });
    }
}
