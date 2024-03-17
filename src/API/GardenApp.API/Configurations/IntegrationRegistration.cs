namespace GardenApp.API.Configurations;

public static class IntegrationRegistration
{
    public static void PanelsIntegrationRegistration(this WebApplication app)
    {
        app.SubscribePanelsIntegrationEvents().InitializePanelsEvents().RegisterClient();
    }
}