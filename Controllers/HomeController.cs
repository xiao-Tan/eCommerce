using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Product> allProducts = dbContext.Products
            .Take(5)
            .ToList();
            List<Order> allOrders = dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            List<Customer> allCustomers = dbContext.Customers
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            ViewBag.AllProducts = allProducts;
            ViewBag.AllOrders = allOrders;
            ViewBag.AllCustomers = allCustomers;
            return View();
        }


        [HttpGet("customers")]
        public IActionResult Customers()
        {
            List<Customer> allCustomers = dbContext.Customers
            .ToList();
            ViewBag.AllCustomers = allCustomers;
            return View();
        }

        [HttpPost("addcustomer")]
        public IActionResult Addcustomer(Customer onecustomer)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Customers.Any(u => u.CustomerName == onecustomer.CustomerName))
                {
                    ModelState.AddModelError("CustomerName", "CustomerName already in use!");
                    List<Customer> allCustomers = dbContext.Customers
                    .ToList();
                    ViewBag.AllCustomers = allCustomers;
                    return View("Customers");
                }
                else
                {
                    dbContext.Customers.Add(onecustomer);
                    dbContext.SaveChanges();
                    return RedirectToAction("Customers");
                }
            }
            else
            {
                List<Customer> allCustomers = dbContext.Customers
                .ToList();
                ViewBag.AllCustomers = allCustomers;
                return View("Customers");
            }
        }

        [HttpGet("remove/{customerId}")]
        public IActionResult Removecustomer(int customerId)
        {
            Customer thisCustomer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            dbContext.Customers.Remove(thisCustomer);
            dbContext.SaveChanges();
            List<Customer> allCustomers = dbContext.Customers
                .ToList();
            ViewBag.AllCustomers = allCustomers;
            return View("Customers");
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.AllProducts = allProducts;
            return View();
        }

        [HttpPost("addproduct")]
        public IActionResult Addproduct(Product oneproduct)
        {
            if (ModelState.IsValid)
            {
                dbContext.Products.Add(oneproduct);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            else
            {
                List<Product> allProducts = dbContext.Products.ToList();
                ViewBag.AllProducts = allProducts;
                return View("Products");
            }
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            List<Order> allOrders = dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
            ViewBag.AllOrders = allOrders;
            List<Customer> allCustomers = dbContext.Customers
            .ToList();
            ViewBag.AllCustomers = allCustomers;
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.AllProducts = allProducts;
            return View();
        }

        [HttpPost("addorder")]
        public IActionResult Addorder(Order neworder)
        {
            if (ModelState.IsValid)
            {
                Product thisProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == neworder.ProductId);
                if (neworder.Orderquantity <= thisProduct.InitialQuantity)
                {
                    dbContext.Orders.Add(neworder);
                    dbContext.SaveChanges();
                    int updatedQuantity = neworder.Product.InitialQuantity - neworder.Orderquantity;
                    thisProduct.InitialQuantity = updatedQuantity;
                    thisProduct.UpdateddAt = DateTime.Now;
                    dbContext.SaveChanges();
                    return RedirectToAction("Orders");
                }
                else
                {
                    ModelState.AddModelError("Orderquantity", "Not enough items!");
                    List<Order> allOrders = dbContext.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Product)
                    .OrderByDescending(o => o.CreatedAt)
                    .ToList();
                    ViewBag.AllOrders = allOrders;
                    List<Customer> allCustomers = dbContext.Customers.ToList();
                    ViewBag.AllCustomers = allCustomers;
                    List<Product> allProducts = dbContext.Products.ToList();
                    ViewBag.AllProducts = allProducts;
                    return View("Orders");
                }
            }
            else
            {
                List<Order> allOrders = dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
                ViewBag.AllOrders = allOrders;
                List<Customer> allCustomers = dbContext.Customers.ToList();
                ViewBag.AllCustomers = allCustomers;
                List<Product> allProducts = dbContext.Products.ToList();
                ViewBag.AllProducts = allProducts;
                return View("Orders");
            }
        }

        [HttpGet("allCustomers")]
        public IActionResult AllCustomers()
        {
            List<Product> allProducts = dbContext.Products
            .Take(5)
            .ToList();
            List<Order> allOrders = dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            List<Customer> allCustomers = dbContext.Customers
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
            ViewBag.AllProducts = allProducts;
            ViewBag.AllOrders = allOrders;
            ViewBag.AllCustomers = allCustomers;
            return View("Index");
        }

        [HttpGet("moreProducts")]
        public IActionResult MoreProducts()
        {
            List<Product> allProducts = dbContext.Products
            .ToList();
            List<Order> allOrders = dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            List<Customer> allCustomers = dbContext.Customers
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            ViewBag.AllProducts = allProducts;
            ViewBag.AllOrders = allOrders;
            ViewBag.AllCustomers = allCustomers;
            return View("Index");
        }

        [HttpGet("allOrders")]
        public IActionResult AllOrders()
        {
            List<Product> allProducts = dbContext.Products
            .Take(5)
            .ToList();
            List<Order> allOrders = dbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
            List<Customer> allCustomers = dbContext.Customers
            .OrderByDescending(o => o.CreatedAt)
            .Take(3)
            .ToList();
            ViewBag.AllProducts = allProducts;
            ViewBag.AllOrders = allOrders;
            ViewBag.AllCustomers = allCustomers;
            return View("Index");
        }
    }
}
