using Dapper;
using Mysqlx.Crud;
using Newtonsoft.Json;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace PentiaWingineers.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public OrderRepository(IConfiguration configuration)
        {
      
            this.configuration = configuration;
            this.connectionString = configuration["ConnectionStrings:PentiaWingineersContext"];

        }
        public void AddOrder(Models.Order order)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {

                var query = "INSERT OR REPLACE INTO " +
                            "Orders (id, orderName, orderPrice, orderDate, salesPersonId)  " +
                            "VALUES (@id, @orderName, @orderPrice, @orderDate, @salesPersonId)";
                cnn.Execute(query, order);
            }
        }


        public async Task<List<Models.Order>?> fetchData()
        {
            HttpClient httpClient = new HttpClient();
            var apiKey = configuration["Api:MyApiKey"];
            String baseUrl = configuration["Api:baseUrl"];
            httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        
            HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/OrderLines");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Models.Order>>(jsonString);

                return orders;
            }
            else
            {
                Console.WriteLine("An error has occured during http-request" + "C:" + response.StatusCode);
                // Handle error response
                return null;
            }
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Models.Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Order> GetAllOrdersFromSalesPerson(int salesPersonId)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "SELECT * FROM orders where salesPersonId = @salesPersonId";
                var output = cnn.Query<Models.Order>(query, new { salesPersonId });
                return output;
            }
        }

        public Models.Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public void resetOrdersTable()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Models.Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderTable(List<Models.Order> orders)
        {
            foreach (var o in orders)
            {
                AddOrder(o);
            }
        }
    }
}
