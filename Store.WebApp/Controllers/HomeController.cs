using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.WebApp.Models;
using Store.DataModel;

namespace Store.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Session _session;

        public HomeController(ILogger<HomeController> logger, Session session)
        {
            _logger = logger;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCustomer()
        {
            return View();
        }
        public IActionResult AddOrder()
        {
            return View();
        }
        public IActionResult SearchOrders()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
