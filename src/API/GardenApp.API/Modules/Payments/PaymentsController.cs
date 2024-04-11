namespace GardenApp.API.Modules.Payments;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly string _webhookSecret;
    private readonly IPaymentsService _paymentsService;
    private readonly IProcessService _processService;
    private readonly Serilog.ILogger _logger;

    public PaymentsController(
        IPaymentsService paymentsService,
        IProcessService processService,
        IConfiguration configuration,
        Serilog.ILogger logger)
    {
        _webhookSecret = configuration["StripeOptions:WebhookSecret"]!;
        _paymentsService = paymentsService;
        _processService = processService;
        _logger = logger;
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<CreateCheckoutSessionResponse>), 200)]
    [SwaggerOperation(Summary = "Creates a new payment session")]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCheckout([FromBody] CreateCheckoutSessionParameters parameters)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var response = await _paymentsService.CreateCheckoutAsync(parameters, baseUrl);
        return Ok(response);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _webhookSecret
              );

            _logger.Information($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
        }
        catch (Exception e)
        {
            _logger.Error($"Something failed {e}");
            return BadRequest();
        }

        await _processService.Process(stripeEvent);
        return Ok();
    }
}