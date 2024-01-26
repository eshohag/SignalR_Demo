using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using SignalR_Demo.Hubs;
using SignalR_Demo.Models;
using System.Data;

namespace SignalR_Demo.Repository
{
    public class Repository
    {
        string connectionString = String.Empty;
        public Repository()
        {
            connectionString = "server=DESKTOP-2FUD2MJ;Initial Catalog= ProductsDB;MultipleActiveResultSets=True;integrated security=true";
        }

        public List<Product> GetAllMessages()
        {
            var messages = new List<Product>();
            SqlConnection con = new SqlConnection(connectionString);
            using (var cmd = new SqlCommand(@"SELECT  [Id]
                                                      ,[Name]
                                                      ,[Category]
                                                      ,[Price]
                                                  FROM [ProductsDB].[dbo].[Products]", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    messages.Add(item: new Product
                    {
                        Id = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        Name = ds.Tables[0].Rows[i][1].ToString(),
                        Category = ds.Tables[0].Rows[i][2].ToString(),
                        Price = Convert.ToDecimal(ds.Tables[i].Rows[i][3]),
                    });
                }
            }
            return messages;
        }
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table.  
        {
            if (e.Type == SqlNotificationType.Change)
            {
                DashboardHub.
            }
        }
    }
}
