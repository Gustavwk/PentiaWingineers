using PentiaWingineers.Models;

namespace PentiaWingineers.Interfaces
{
   public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Dictionary<string,OrderOverviewObject> GetAllOrdersSorted();
        IEnumerable<Order> GetAllOrdersFromSalesPerson(int salesPersonId);
        Order GetOrderById(int id);
        void AddOrder(Order order);
        Task<List<Order>?> fetchData();
        void UpdateOrderTable(List<Order> orders);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void resetOrdersTable();
    }
}
