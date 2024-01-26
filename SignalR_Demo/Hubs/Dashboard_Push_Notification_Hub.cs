using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Repository;

namespace SignalR_Demo.Hubs
{
    public class Dashboard_Push_Notification_Hub : Hub
    {
        ProductRepository productRepository;
        public Dashboard_Push_Notification_Hub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProductDbContext");
            productRepository = new ProductRepository(connectionString);
        }
        public async Task RequestSummary()
        {
            var totalCount = productRepository.ProductSummary();
            await Clients.All.SendAsync("ReceivedSummary", totalCount);
        }
    }
}
