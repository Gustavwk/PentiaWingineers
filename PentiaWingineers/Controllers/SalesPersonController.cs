using Microsoft.AspNetCore.Mvc;
using PentiaWingineers.Data;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;

namespace PentiaWingineers.Controllers
{
    public class SalesPersonController : Controller
    {
        private readonly ISalesPersonRepository salesPersonRepository;
        private readonly IOrderRepository orderRepository;
        public SalesPersonController(ISalesPersonRepository salesPersonRepository, IOrderRepository orderRepository)
        {
            this.salesPersonRepository = salesPersonRepository;
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
          var dingdong = salesPersonRepository.GetAllSalesPersons().ToList();
            return View(dingdong);
        }

        public async Task<IActionResult> ListAsync()
        {
            var salesPersons = await salesPersonRepository.fetchData();
            var orderList = await orderRepository.fetchData();
            if (orderList != null) {orderRepository.UpdateOrderTable(orderList); }
            if (salesPersons!=null) {salesPersonRepository.updateSalesPersonTable(salesPersons);}
            return View(salesPersons);
        }

        public IActionResult SalesPersonStats(int id) {
            var salesPerson = salesPersonRepository.GetSalesPersonById(id);
            List<Order> salesPersonOrder = orderRepository.GetAllOrdersFromSalesPerson(salesPerson.id).ToList();
            SalesPersonOrders salesPersonOrders = new SalesPersonOrders(salesPerson,salesPersonOrder);
            return View(salesPersonOrders);
        }
    }
}
