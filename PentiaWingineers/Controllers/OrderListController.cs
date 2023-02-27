using Microsoft.AspNetCore.Mvc;
using PentiaWingineers.Data;
using PentiaWingineers.Interfaces;

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
            return View();
        }

        public async Task<IActionResult> ListAsync()
        {
            var orders = await orderRepository.fetchData();
            return View(orders);
        }
    }
}
