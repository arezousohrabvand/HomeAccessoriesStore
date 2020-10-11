using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAccessoriesStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccessoriesStore.Controllers
{
    public class FurnitureController : Controller
    {
        public IActionResult Index()
        {
            var categories = new List<Category>();
            for(var i = 1; i <= 10; i++)
            {
                categories.Add(new Category { Id = i, Name = "Category" + i.ToString() });
             
            }
            return View(categories);
        }
        public IActionResult Details(string category)
        {
            ViewBag.category = category;
            return View();
        }
        //Furniture /AddCategory
        public IActionResult AddCategory()
        {
            //load a form to capture an obj from user
            return View();
        }
    }
}
