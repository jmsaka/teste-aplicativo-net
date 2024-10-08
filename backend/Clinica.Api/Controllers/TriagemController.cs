namespace Clinica.Api.Controllers;

[Route("api/[controller]")]
public class TriagemController : BaseController
{
    public TriagemController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<ICollection<TriagemEntity>>>> Get([FromQuery] int id)
    {
        return await HandleQuery(new GetTriagemQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<TriagemEntity>>> Post([FromBody] UpsertTriagemCommand command)
    {
        return await HandleCommand(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<TriagemEntity>>> Delete(int id)
    {
        return await HandleCommand(new DeleteTriagemCommand() { Id = id });
    }
}
