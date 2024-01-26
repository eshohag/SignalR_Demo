using SignalR_Demo.Controllers;
using SignalR_Demo.Hubs;
using SignalR_Demo.Models;
using TableDependency.SqlClient;

namespace SignalR_Demo.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency : ISubscribeTableDependency
    {
        private SqlTableDependency<Product> tableDependency;
        private readonly Product_Push_Notification_Hub _productHub;
        private readonly Dashboard_Push_Notification_Hub _dashboardHub;
        private readonly ILogger<SubscribeProductTableDependency> _logger;

        public SubscribeProductTableDependency(Product_Push_Notification_Hub productHub, Dashboard_Push_Notification_Hub dashboardHub,ILogger<SubscribeProductTableDependency> logger)
        {
            this._productHub = productHub;
            this._dashboardHub = dashboardHub;
            this._logger = logger;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Product>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _dashboardHub.RequestSummary();
                _productHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            var message = $"{nameof(Product)} SqlTableDependency error: {e.Error.Message}";
            _logger.LogError(message);
        }
    }
}
