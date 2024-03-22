namespace GardenApp.API.Modules.Offers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OffersController : BaseController
{
    public OffersController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
    {
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response), 200)]
    [SwaggerOperation(Summary = "Completion of the garden offer")]
    [HttpPut("[action]")]
    public async Task<IActionResult> CompleteOffer([FromBody] CompleteOfferCommand command)
    {
        var result = await CommandBus.Send(command);
        return Ok(result);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<AddGardenOfferItemResponse>), 200)]
    [SwaggerOperation(Summary = "Adding a new item to the garden offer")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddGardenOfferItem([FromBody] AddGardenOfferItemCommand command)
    {
        var result = await CommandBus.Send(command);
        return Ok(result);
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<CreateGardenOfferResponse>), 200)]
    [SwaggerOperation(Summary = "Creating a new garden offer")]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateGardenOffer([FromBody] CreateGardenOfferCommand command)
    {
        var result = await CommandBus.Send(command);
        return Ok(result);
    }
}