using Microsoft.AspNetCore.Mvc;
using PustokTask1.DAL;
using PustokTask1.Models;
using PustokTask1.ViewModels;
using System.Diagnostics;

namespace PustokTask1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sliders=_context.Sliders.ToList()
            };
            return View(homeViewModel);


        }   }
}