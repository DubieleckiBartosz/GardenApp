namespace Works.Infrastructure.Clients;

internal class CachedWeatherHandler : DelegatingHandler
{
    //https://stackoverflow.com/questions/59791959/why-should-i-use-imemorycache-when-we-have-idistributedcache
    private readonly IMemoryCache _cache;

    private readonly ILogger _logger;

    public CachedWeatherHandler(IMemoryCache cache, ILogger logger)
    {
        _cache = cache;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var queryString = HttpUtility.ParseQueryString(request.RequestUri!.Query);
            var query = queryString["q"];
            var units = queryString["units"];

            var key = $"{query}-{units}";

            var cached = _cache.Get<string>(key);
            if (cached != null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(cached)
                };
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                _cache.Set(key, content, TimeSpan.FromMinutes(2));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error($"CachedWeatherHandler - something went wrong [Exception message: {ex.Message}]");
            throw;
        }
    }
}