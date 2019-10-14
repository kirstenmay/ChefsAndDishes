using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        //CHEFS
        [HttpGet("")]
        public IActionResult Chefs()
        {
            List<Chef> AllChefs = dbContext.Chefs.Include(d => d.CreatedDishes).ToList();
            foreach(Chef chef in AllChefs)
            {
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(chef.Birthday.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                chef.Age = age;
                dbContext.SaveChanges();
            }
            return View(AllChefs);
        }
        [HttpGet("AddChef")]
        public IActionResult AddChef()
        {
            return View();
        }
        [HttpPost("CreateChef")]
        public IActionResult CreateChef(Chef chef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(chef);
                dbContext.SaveChanges();
                return RedirectToAction("Chefs");
            }
            else
            {
                return View("AddChef");
            }
        }


        //DISHES
        [HttpGet("Dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes.Include(c => c.Creator).ToList();
            return View(AllDishes);
        }
        [HttpGet("AddDish")]
        public IActionResult AddDish()
        {
            NewDishViewModel viewMod =  new NewDishViewModel();
            viewMod.AllChefs = dbContext.Chefs.ToList();
            return View(viewMod);
        }
        [HttpPost("/CreateDish")]
        public IActionResult CreateDish(NewDishViewModel dish)
        {
            System.Console.WriteLine(dish);
            if(ModelState.IsValid)
            {
                dbContext.Add(dish.New_Dish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                NewDishViewModel viewMod =  new NewDishViewModel();
                viewMod.AllChefs = dbContext.Chefs.ToList();
                return View("AddDish", viewMod);
            }
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
