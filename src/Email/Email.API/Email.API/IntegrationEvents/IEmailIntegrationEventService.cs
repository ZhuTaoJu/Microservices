using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Email.API.IntegrationEvents
{
    public interface IEmailIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
