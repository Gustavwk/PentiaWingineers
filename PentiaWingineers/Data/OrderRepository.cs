using Dapper;
using Newtonsoft.Json;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        public void AddOrder(Order order)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {

                var query = "INSERT OR REPLACE INTO " +
                            "Orders (id, orderName, orderPrice, orderDate, salesPersonId)  " +
                            "VALUES (@id, @orderName, @orderPrice, @orderDate, @salesPersonId)";
                cnn.Execute(query, order);
            }
        }


        public async Task<List<Order>?> fetchData()
        {
            HttpClient httpClient = new HttpClient();
            var apiKey = configuration["Api:MyApiKey"];
            String baseUrl = configuration["Api:baseUrl"];
            httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        
            HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/OrderLines");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(jsonString);

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


        public IEnumerable<Order> GetAllOrders()
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "SELECT * FROM Orders";
                var output = cnn.Query<Order>(query);
                return output;
            }
        }

        public IEnumerable<Order> GetAllOrdersFromSalesPerson(int salesPersonId)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "SELECT * FROM orders where salesPersonId = @salesPersonId";
                var output = cnn.Query<Order>(query, new { salesPersonId });
                return output;
            }
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public void resetOrdersTable()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderTable(List<Order> orders)
        {
            foreach (var o in orders)
            {
                AddOrder(o);
            }
        }

        public Dictionary<string,OrderOverviewObject> GetAllOrdersSorted()
        {
            string year = "";
            string mounth = "";
            StringBuilder sb = new StringBuilder();
            var allOrders = GetAllOrders();
            var orderOverViewObjects = new Dictionary<string,OrderOverviewObject>();
            foreach (var o in allOrders)
            {
                Regex regexYear = new Regex(@"\d{4}");
                Regex regexMounth = new Regex(@"-(\d{2})-");
                Match matchYear = regexYear.Match(o.orderDate);
                Match matchMounth = regexMounth.Match(o.orderDate);
                year = matchYear.Value;   
                mounth = matchMounth.Value;
                mounth = mounth.Remove(mounth.Length - 1, 1);
                sb.Append(year);
                sb.Append(mounth);
                if (!orderOverViewObjects.ContainsKey(sb.ToString())){
                    var currentObject = new OrderOverviewObject(new List<Order>(), mounth, year);
                    currentObject.orders.Add(o);
                    orderOverViewObjects[sb.ToString()] = currentObject;
                } else {
                    orderOverViewObjects[sb.ToString()].orders.Add(o);
                }
                sb.Clear();
            }
             return orderOverViewObjects;
        }
    }
}
