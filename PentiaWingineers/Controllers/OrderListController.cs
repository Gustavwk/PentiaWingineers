using Microsoft.AspNetCore.Mvc;
using PentiaWingineers.Data;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;

namespace PentiaWingineers.Controllers
{
    public class OrderListController : Controller
    {
        
        private readonly IOrderRepository orderRepository;
        public OrderListController(IOrderRepository orderRepository)
        {
          
            this.orderRepository = orderRepository;

        }

        public IActionResult Index()
        {
            List<Order> allOrders = orderRepository.GetAllOrders().ToList();
            return View(allOrders);
        }

        public async Task<IActionResult> ListAsync()
        {
            var orders = await orderRepository.fetchData();
            return View(orders);
        }

        public IActionResult Overview(){
            return View(orderRepository.GetAllOrdersSorted());
        }
    }
}
