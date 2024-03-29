using static Panels.Application.Handlers.Commands.AddProjectImageHandler;
using static Panels.Application.Handlers.Commands.RemoveLogoHandler;

namespace GardenApp.API.Modules.Panels;

[Authorize(Roles = "Admin,Business")]
[Route("api/[controller]")]
[ApiController]
public class PanelsController : BaseController
{
    public PanelsController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Add social media link to contractor")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddLink([FromBody] AddLinkCommand command)
    {
        var response = await CommandBus.Send(command);
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Add logo to contractor")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddLogo([FromForm] AddLogoCommand command)
    {
        var response = await CommandBus.Send(command);
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Add new project image")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddProjectImage([FromForm] AddProjectImageCommand command)
    {
        var response = await CommandBus.Send(command);
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(CreateProjectResponse), 200)]
    [SwaggerOperation(Summary = "Create a new project")]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var response = await CommandBus.Send(command);
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Update project description")]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDescriptionCommand command)
    {
        var response = await CommandBus.Send(command);
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Remove logo")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> RemoveLogo()
    {
        var response = await CommandBus.Send(new RemoveLogoCommand());
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Remove project")]
    [HttpDelete("[action]/{projectId}")]
    public async Task<IActionResult> RemoveProject([FromRoute] int projectId)
    {
        var response = await CommandBus.Send(new RemoveProjectCommand(projectId));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Remove link")]
    [HttpDelete("[action]/{linkType}")]
    public async Task<IActionResult> RemoveLink([FromRoute] LinkType linkType)
    {
        var response = await CommandBus.Send(new RemoveLinkCommand(linkType));
        return Ok(response);
    }
}