using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAccessoriesStore.Data;
using Microsoft.AspNetCore.Mvc;
using HomeAccessoriesStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


//stripe  refrences
using Stripe;
using System.Configuration;//read the stripe api keys from appsetting.json
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace HomeAccessoriesStore.Controllers
{

    public class ShopController : Controller
    {
        //database connection
        private readonly ApplicationDbContext _context;
        private IConfiguration _iconfiguration;

        //connect to the database
        public ShopController(ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
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
            if (HttpContext.Session.GetString("CustomerId") == null)
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
        public IActionResult Cart()
        {
            //fetch current cart for display
            var CustomerId = "";
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                CustomerId = GetCustomerId();
            }
            else
            {
                CustomerId = HttpContext.Session.GetString("CustomerId");
            }
            //query the db for this customer
            var cartItems = _context.Cart.Include(c => c.Products).Where(c => c.CustomerId == CustomerId).ToList();

            //pass the data
            return View(cartItems);
        }
        //Get /Shop/RemoveFromCart
        public IActionResult RemoveFromCart(int id)
        {
            //find the item
            var cartItem = _context.Cart.Find(id);
            //delete the product from cart
            if (cartItem != null)
            {
                _context.Cart.Remove(cartItem);
                _context.SaveChanges();
            }
            //redirect to updte cart
            return RedirectToAction("Cart");

        }



        //shop//checkout
        [Authorize]
        public IActionResult CheckOut()
        {
            return View();
        }
        //Post /shop/checkout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut([Bind("FirstName,LastName,Address,City,Province,postalCode")] Models.Orders orders)
        {
            orders.OrderDate = DateTime.Now;
            orders.CustomerId = User.Identity.Name;
            //var cartCustomer = HttpContext.Session.GetString("CustomerId");
            //var cartItems = _context.Cart.Where(c => c.CustomerId == cartCustomer);
            orders.Total = (from c in _context.Cart
                            where c.CustomerId == HttpContext.Session.GetString("CustomerId")
                            select c.Quantity * c.TotalPrice).Sum();


            //var orderTotal = (from c in cartItems select c.Quantity * c.TotalPrice).Sum();
            //orders.total = orderTotal;
            //use sessionextension obj to store the order obj in asession variable
            HttpContext.Session.SetObject("Orders", orders);


            //redirect to payment page
            return RedirectToAction("Payment");


        }

        //Get cart/payment
        public IActionResult Payment()
        {
            var order = HttpContext.Session.GetObject<Models.Orders>("Orders");
            ViewBag.total = order.Total;
            ViewBag.PublishableKey = _iconfiguration.GetSection("Stripe")["PublishableKey"];

            return View();
        }
        //Post /Shop/ProcessPayment
        [Authorize]
        [HttpPost]
        public IActionResult ProcessPayment()
        {

            var order = HttpContext.Session.GetObject<Models.Orders>("Orders");
            //get the Stripe key from the configuration
            StripeConfiguration.ApiKey = _iconfiguration.GetSection("Stripe")["SecretKey"];


            //create and submit Stripe payment
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = (long?)(order.Total*100),
                      Currency = "cad",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "Home Accessorie Purchase",
                      },
                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",

                SuccessUrl = "https://" + Request.Host + "/Shop/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Shop/Cart"
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return Json(new { id = session.Id });

        }
       // get /shop/SaveOrder
        [Authorize]
        public IActionResult SaveOrder()
        {
            var order = HttpContext.Session.GetObject<Models.Orders>("Orders");
            //save it on db
            _context.Order.Add(order);
            _context.SaveChanges();

            var cartItems = _context.Cart.Where(c => c.CustomerId == HttpContext.Session.GetString("CustomerId"));

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {


                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = (double)item.TotalPrice,
                    OrdersId = order.OrdersId
                };
                _context.OrderDetail.Add(orderDetail);
            }
            _context.SaveChanges();

            foreach (var item in cartItems)
            {
                _context.Cart.Remove(item);


            }
            _context.SaveChanges();
           


            HttpContext.Session.SetInt32("ItemCount", 0);
            return RedirectToAction("Details", "Orders", new { @id = order.OrdersId });

        }


    }

}


