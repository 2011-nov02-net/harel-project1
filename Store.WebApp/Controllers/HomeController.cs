using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.WebApp.Models;

namespace Store.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _session;

        public HomeController(ILogger<HomeController> logger, IRepository session)
        {
            _logger = logger;
            _session = session;
        }

        public IActionResult Index()
        {
            return View(_session.Locations.ToList()
                .Select(l => new LocationModel(l, _session.Items)));   
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
        /// <param>
        /// id is the location id
        /// </param>
        public IActionResult AddOrder(int id)
        {
            return View(new AddOrderViewModel(
                _session.Locations.First(x => x.Id == id),
                _session.Customers.ToList().AsQueryable(),
                _session.Items.ToList().AsQueryable()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder(IFormCollection collection)
        {
            try
            {
                var LocationId = Convert.ToInt32(collection["LocationId"]);
                var orderLocation = _session.Locations.Where(location =>
                    location.Id == LocationId).First();
                var CustomerId = Convert.ToInt32(collection["CustomerId"]);
                var orderCustomer = _session.Customers.Where(customer =>
                    customer.Id == CustomerId).First();
                Dictionary<int,int> itemCounts = new();
                foreach (var skey in collection.Keys)
                {
                    if (skey.StartsWith("item_")) {
                        var myItemId = Convert.ToInt32(skey.Remove(0, 5));
                        var myItemCount = Convert.ToInt32(collection[skey]);
                        if (myItemCount > 0) itemCounts.Add(myItemId, myItemCount);
                    }
                }
                _session.AddOrder(orderCustomer, orderLocation, itemCounts); // Error on this line
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                _logger.LogError(e, "Reading Add Order Form");
                try
                {
                    var LocationId = Convert.ToInt32(collection["LocationId"]);
                    return View(new AddOrderViewModel(
                        _session.Locations.First(l => l.Id == LocationId),
                        _session.Customers.ToList().AsQueryable(),
                        _session.Items.ToList().AsQueryable()));
                }
                catch (FormatException)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }
        /// <summary>
        /// id is search string, returns customers whose name contains the string
        /// </summary>
        public IActionResult SearchCustomer(string id)
        {
            return View(_session.Customers
                .Where(customer => customer.Name.Contains(id))
                .AsEnumerable());
        }
        public IActionResult LocationOrders(int id)
        {
            try
            {
                var myLocation = _session.Locations.First(x => x.Id == id);
                //var myOrders   = _session.OrderHistory(myLocation);
                var myOrders = _session.Orders.ToList().Where(o => o.LocationId == myLocation.Id).AsEnumerable();
                // ViewData["Location"] = new LocationModel(myLocation, _session.Items);
                // ViewData["Items"] = _session.Items.ToList().Where(x =>
                //     myOrders.Any(y =>
                //         (y as IOrder).ItemCounts.ContainsKey(x.Id) )
                //     ).Select(x => new ItemModel(x)).AsEnumerable();
                // return View(myOrders);
                // ViewBag.location = myLocation;
                /*
                var myItems = Model.Select(o => o.Items)
                .Aggregate((x, y) => x.Concat(y))
                .Distinct().OrderBy(item => item.Id);
                */
                var model = new LocationOrdersViewModel(
                    new LocationModel(myLocation, _session.Items),
                    _session.Items.ToList().Where(x => myOrders.Any(y =>
                        (y as IOrder).ItemCounts.ContainsKey(x.Id) ) )
                        .Select(x => new ItemModel(x)).AsEnumerable(),
                         _session.Orders.ToList()
                            .Where(o => o.LocationId == myLocation.Id).AsEnumerable());
                return View(model);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult CustomerOrders(int id)
        {
            try
            {
                var myCustomer = _session.Customers.First(x => x.Id == id);
                //var myOrders   = _session.OrderHistory(myCustomer);
                var myOrders = _session.Orders.ToList().Where(o => o.CustomerId == myCustomer.Id).AsEnumerable();
                ViewData["Customer"] = new CustomerModel(myCustomer);
                ViewData["Items"] = _session.Items.ToList().Where(x =>
                    myOrders.Any(y =>
                        y.ItemCounts.ContainsKey(x.Id) )
                    ).Select(x => new ItemModel(x)).AsEnumerable();
                // ViewBag.customer = myCustomer;
                /*
                var myItems = Model.Select(o => o.Items)
                .Aggregate((x, y) => x.Concat(y))
                .Distinct().OrderBy(item => item.Id);
                */                
                return View(myOrders);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return RedirectToAction(nameof(Index));
            }
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
