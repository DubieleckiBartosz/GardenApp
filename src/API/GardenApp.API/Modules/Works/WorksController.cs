namespace GardenApp.API.Modules.Works;

[Authorize(Roles = "Admin,Business")]
[Route("api/[controller]")]
[ApiController]
public class WorksController : BaseController
{
    public WorksController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<GetGardeningWorksResponse>), 200)]
    [SwaggerOperation(Summary = "Gets views of garden work")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetGardeningWorks()
    {
        var response = await QueryBus.Send(new GetGardeningWorksQuery());
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<GardeningWorkDetailsViewModel>), 200)]
    [SwaggerOperation(Summary = "Gets garden work details")]
    [HttpGet("[action]/{gardeningWorkId}")]
    public async Task<IActionResult> GetGardeningWorkDetails([FromRoute] int gardeningWorkId)
    {
        var response = await QueryBus.Send(new GetGardeningWorkDetailsQuery(gardeningWorkId));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<AddGardeningWorkResponse>), 200)]
    [SwaggerOperation(Summary = "Creating new gardening work")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddGardeningWork([FromBody] AddGardeningWorkParameters parameters)
    {
        var response = await CommandBus.Send(AddGardeningWorkCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Updates the status of gardening work")]
    [HttpPut("GardeningWork/[action]")]
    public async Task<IActionResult> UpdateStatus([FromBody] GardeningWorkUpdateStatusParameters parameters)
    {
        var response = await CommandBus.Send(GardeningWorkUpdateStatusCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Updates the planned end date of gardening work")]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdatePlannedEndDate([FromBody] UpdatePlannedEndDateParameters parameters)
    {
        var response = await CommandBus.Send(UpdatePlannedEndDateCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Updates the planned start date of gardening work")]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdatePlannedStartDate([FromBody] UpdatePlannedStartDateParameters parameters)
    {
        var response = await CommandBus.Send(UpdatePlannedStartDateCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Adds a new work item")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddWorkItem([FromBody] AddWorkItemParameters parameters)
    {
        var response = await CommandBus.Send(AddWorkItemCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<AddTimeWeatherRecordResponse>), 200)]
    [SwaggerOperation(Summary = "Adds a new time log to work")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddTimeLog([FromBody] AddTimeWeatherRecordParameters parameters)
    {
        var response = await CommandBus.Send(AddTimeWeatherRecordCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Updates the status of work item")]
    [HttpPut("WorkItem/[action]")]
    public async Task<IActionResult> UpdateStatus([FromBody] WorkItemUpdateStatusParameters parameters)
    {
        var response = await CommandBus.Send(WorkItemUpdateStatusCommand.Create(parameters));
        return Ok(response);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Updates the work time log")]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateTimeLog([FromBody] UpdateTimeWeatherRecordParameters parameters)
    {
        var response = await CommandBus.Send(UpdateTimeWeatherRecordCommand.Create(parameters));
        return Ok(response);
    }
}