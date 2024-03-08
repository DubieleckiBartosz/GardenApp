using Panels.Infrastructure.Configurations;

namespace GardenApp.API.Configurations;

public static class IntegrationRegistration
{
    public static void RegisterEvents(this WebApplication app)
    {
        app.SubscribePanelsIntegrationEvents().InitializePanelsEvents();
    }
}