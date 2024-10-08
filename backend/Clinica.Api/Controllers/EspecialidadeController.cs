namespace Clinica.Api.Controllers;

[Route("api/[controller]")]
public class EspecialidadeController : BaseController
{
    public EspecialidadeController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<ICollection<EspecialidadeEntity>>>> Get([FromQuery] int id)
    {
        return await HandleQuery(new GetEspecialidadeQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<EspecialidadeEntity>>> Post([FromBody] UpsertEspecialidadeCommand command)
    {
        return await HandleCommand(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<EspecialidadeEntity>>> Delete(int id)
    {
        return await HandleCommand(new DeleteEspecialidadeCommand() { Id = id });
    }
}
