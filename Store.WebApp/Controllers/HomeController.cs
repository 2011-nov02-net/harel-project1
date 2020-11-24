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
            // present link at top to add customer
            // search bar to search for customers at top (as a form) + submit button
            // List out locations in main content with links for placing an order and viewing order history
            return View(_session.Locations.AsEnumerable());
        }
        public IActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind("Name")] CustomerModel customer)
        {
            try
            {
                _session.AddCustomer(customer.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult AddOrder(int id)
        {
            // id is the location id 
            return View(); // page form should contain customer selection dropdown
            // contain next to each customer a link to display the customer order history
        }
        public IActionResult SearchCustomer(string id)
        {
            // id is search string
            return View();
        }
        public IActionResult DisplayOrders()
        {
            return View(); // FIXME figure out how to take a general pair (int? LocationId, int? CustomerId)
            // and filter orders by one or both.
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
