using PentiaWingineers.Models;

namespace PentiaWingineers.Interfaces
{
   public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Dictionary<string,OrderOverviewObject> GetAllOrdersSorted();
        IEnumerable<Order> GetAllOrdersFromSalesPerson(int salesPersonId);
        void AddOrder(Order order);
        Task<List<Order>?> fetchData();
        void UpdateOrderTable(List<Order> orders);
        public void deleteAllOrders();
    }
}
