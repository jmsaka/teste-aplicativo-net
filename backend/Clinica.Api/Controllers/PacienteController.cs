namespace Clinica.Api.Controllers;

[Route("api/[controller]")]
public class PacienteController : BaseController
{
    public PacienteController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<ICollection<PacienteEntity>>>> Get([FromQuery] int id)
    {
        return await HandleQuery(new GetPacienteQuery() { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<PacienteEntity>>> Post([FromBody] UpsertPacienteCommand command)
    {
        return await HandleCommand(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<PacienteEntity>>> Delete(int id)
    {
        return await HandleCommand(new DeletePacienteCommand() { Id = id });
    }
}
