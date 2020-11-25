using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAccessoriesStore.Data;
using Microsoft.AspNetCore.Mvc;
using HomeAccessoriesStore.Models;
using Microsoft.AspNetCore.Http;
namespace HomeAccessoriesStore.Controllers
{

    public class ShopController : Controller
    {
        //database connection
        private readonly ApplicationDbContext _context;
        //connect to the database
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //get list of category
            var category = _context.Category.OrderBy(c => c.Name).ToList();
            return View(category);
        }

        public IActionResult ShopDetails(int id)
        {
            var products = _context.Product.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
            ViewBag.category = _context.Category.Find(id).Name.ToString();
            return View(products);
        }

        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            //query the db for the the product price
            var price = _context.Product.Find(ProductId).Price;
            // get current Date and time
            var currentDateTime = DateTime.Now;
            //cutomerid variable
            var CustomerId = GetCustomerId();
            //create and save a new cart object
            var cart = new Cart
            {
                ProductId = ProductId,
                Quantity = Quantity,
                TotalPrice = (decimal)price,
                Date = currentDateTime,
                CustomerId = CustomerId




            };
            _context.Cart.Add(cart);
            _context.SaveChanges();
            //redirect to cart view
            return RedirectToAction("Cart");

    }
        private string GetCustomerId()
        {
            //check session for existing customerid
            if(HttpContext.Session.GetString("CustomerId")==null)
            {
                //if dont have customer id in the session checked if customer is logged in
                var CustomerId = "";
                //if customer is logged in use their email as the customerid
                if (User.Identity.IsAuthenticated)
                {
                    CustomerId = User.Identity.Name;//Name=email
                }
                else
                //if the customer is anonymous use guid to craete a new identifier
                {
                    CustomerId = Guid.NewGuid().ToString();
                }
                //now store the customerid in a session variable
                HttpContext.Session.SetString("CustomerId", CustomerId);

            }
            //return the session variable
            return HttpContext.Session.GetString("CustomerId");

        }


        //shop/cart
        //public IActionResult Cart()
        //{
        //    //fetch current cart for display
        //    var CustomerId = "";
        //    if(HttpContext.Session.GetString("CustomerId")==null)
        //    {
        //        CustomerId = GetCustomerId();
        //    }
        //    else
        //    {
        //        CustomerId = HttpContext.Session.GetString("CustomerId");
        //    }

        //    var cartItems = _context.Cart.Where(c => c.CustomerId == CustomerId).ToList();
        //        return View(cartItems);
        //}

    }
}

