using Microsoft.AspNetCore.Mvc;
using PentiaWingineers.Interfaces;
using PentiaWingineers.Models;
using System.Diagnostics;

namespace PentiaWingineers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ISalesPersonRepository _salesPersonRepository;

        public HomeController(ILogger<HomeController> logger, ISalesPersonRepository salesPersonRepository, IOrderRepository orderRepository) 
        {
            _logger = logger;
            _orderRepository = orderRepository; 
            _salesPersonRepository = salesPersonRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult restart()
        {
            _orderRepository.deleteAllOrders();
            _salesPersonRepository.deleteAllSalesPersons();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}