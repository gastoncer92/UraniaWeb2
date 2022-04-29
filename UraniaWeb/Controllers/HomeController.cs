using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UraniaWeb.Models;
using UraniaWeb.Models.ViewModels;



namespace UraniaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UraniaWebDbContext _dbContext;

        public HomeVM HomeVM { get; private set; }

        public HomeController(UraniaWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<IActionResult> Index()
        //{
         //   return View(await _dbContext.Articles.ToListAsync());
        //}

        public IActionResult Index()
        {
            HomeVM = new HomeVM()
            {
                //Slider = _dbContext. Sliders.GetAll(),
                ListaArticulos = _dbContext.Articles.G
            };
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
