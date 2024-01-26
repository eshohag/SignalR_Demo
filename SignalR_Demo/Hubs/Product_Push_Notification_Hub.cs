using Microsoft.AspNetCore.SignalR;
using SignalR_Demo.Repository;

namespace SignalR_Demo.Hubs
{
    public class Product_Push_Notification_Hub : Hub
    {
        ProductRepository productRepository;
        public Product_Push_Notification_Hub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProductDbContext");
            productRepository = new ProductRepository(connectionString);
        }
        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);
        }
    }
}
