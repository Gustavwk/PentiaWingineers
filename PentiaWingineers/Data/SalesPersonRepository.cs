using Dapper;
using Newtonsoft.Json;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;
using System.Data;
using System.Data.SQLite;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace PentiaWingineers.Data
{

    public class SalesPersonRepository : ISalesPersonRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
       
        

        public SalesPersonRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration["ConnectionStrings:PentiaWingineersContext"];

        }
        public void AddSalesPerson(SalesPerson salesPerson)
        { 
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {

                var query = "INSERT OR REPLACE INTO " +
                            "salesperson (id, name, hireDate, address, city, zipCode)  " +
                            "VALUES (@id, @name, @hireDate, @address, @city, @zipCode)";
                cnn.Execute(query, salesPerson);
            }
        }

        public void DeleteSalesPerson(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "DELETE FROM salesperson WHERE id = @id";
                cnn.Execute(query, new { id });
            }
        }

        public async Task<List<SalesPerson>?> fetchData()
        {
            HttpClient httpClient = new HttpClient();
            var apiKey = configuration["Api:MyApiKey"];
            String baseUrl = configuration["Api:baseUrl"];
            httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);

            HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/SalesPersons");
           

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var salesPersons = JsonConvert.DeserializeObject<List<SalesPerson>>(jsonString);

                return salesPersons;
            }
            else
            {
                Console.WriteLine("An error has occured during http-request" + "C:" + response.StatusCode);
                // Handle error response
                return null;
            }
        }

        public IEnumerable<SalesPerson> GetAllSalesPersons()
        {
            
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "select * from salesperson";
                var output = cnn.Query<SalesPerson>(query);
                return output;
            }
            
        }

        public void resetSalesTable()
        {
            throw new NotImplementedException();
        }

   

        public void deleteSalesPersonById(int id) {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "DELETE FROM salesperson WHERE id = @id";
                cnn.QueryFirstOrDefault<SalesPerson>(query, new { id });
            }
        }

        public void UpdateSalesPerson(Order order)
        {
            throw new NotImplementedException();
        }

        public void updateSalesPersonTable(List<SalesPerson> salesPersons) {
            foreach (var o in salesPersons)
            {
                if (GetSalesPersonById(o.id) == null) { }
                AddSalesPerson(o);
            }
        }

        public void UpdateSalesPerson(SalesPerson salesPerson)
        {
            throw new NotImplementedException();
        }

        public SalesPerson GetSalesPersonById(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(connectionString))
            {
                var query = "SELECT * FROM salesperson WHERE id = @id";
                return cnn.QueryFirstOrDefault<SalesPerson>(query, new { id });
            }
        }

        public IEnumerable<Order> GetOrdersFromSalesPersonId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
