﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.WebApp.Models;
using Store.DataModel;


namespace Store.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Session _session;

        public HomeController(ILogger<HomeController> logger, Session session)
        {
            _logger = logger;
            _session = session;
        }

        public IActionResult Index()
        {
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
        /// <param>
        /// id is the location id
        /// </param>
        public IActionResult AddOrder(int id) 
        {
            var model = new AddOrderViewModel(
                _session.Locations.First(x => x.Id == id), 
                _session.Customers.ToList().AsQueryable(), 
                _session.Items.ToList().AsQueryable(), 
                OrderItem.countMax);
            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder(IFormCollection collection)
        {
            try
            {
                var orderLocation = _session.Locations.Where(location => 
                    location.Id == Convert.ToInt32(collection["LocationId"])).First();
                var orderCustomer = _session.Customers.Where(customer =>
                    customer.Id == Convert.ToInt32(collection["CustomerId"])).First();
                Dictionary<int,int> itemCounts = new();
                foreach (var skey in collection.Keys)
                {
                    if (skey.StartsWith("item_")) {
                        var myItemId = Convert.ToInt32(skey.Remove(0, 5));
                        var myItemCount = Convert.ToInt32(collection[skey]);
                        itemCounts.Add(myItemId, myItemCount);
                    }
                }
                _session.AddOrder(orderCustomer, orderLocation, itemCounts);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult SearchCustomer(string id)
        {
            // id is search string
            var customers = _session.Customers.Where(customer => customer.Name.Contains(id)).AsEnumerable();
            return View(customers);
        }
        public IActionResult LocationOrders(int id)
        {
            try
            {
                var myLocation = _session.Locations.First(x => x.Id == id);
                var myOrders   = _session.OrderHistory(myLocation);
                /*
                ViewData["Location"] = myLocation;
                ViewData["Items"] = _session.Items.Where(x => 
                    myOrders.Any(y => 
                        y.ItemCounts.ContainsKey(x.Id) )
                    ).AsEnumerable();*/
                ViewBag.location = myLocation;
                return View(myOrders);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult CustomerOrders(int id)
        {
            try
            {
                var myCustomer = _session.Customers.First(x => x.Id == id);
                var myOrders   = _session.OrderHistory(myCustomer);
                /*ViewData["Customer"] = myCustomer;
                ViewData["Items"] = _session.Items.Where(x =>
                    myOrders.Any(y =>
                        y.ItemCounts.ContainsKey(x.Id) )
                    ).AsEnumerable();*/
                ViewBag.customer = myCustomer;
                return View(myOrders);
            }
            catch (InvalidOperationException)
            {
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
