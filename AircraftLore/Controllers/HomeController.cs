using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AircraftLore.Models;

namespace AircraftLore.Controllers{
    public class HomeController : Controller{

        private readonly AircraftDatabaseContext _aircraft_database_context;

        public HomeController(AircraftDatabaseContext db){
            _aircraft_database_context = db;
        }
        // GET: Home/
        public async Task<IActionResult> Index(string CategoryString, string CountryString, string ModelString){
            var VM = await _aircraft_database_context.GetAircraftView(CategoryString, CountryString, ModelString);
            return View(VM);
        }
        // GET: Home/Privacy
        public IActionResult Privacy(){
            return View();
        }
        // GET: Home/Aircraft
        public async Task<IActionResult> Aircraft(string CategoryString, string CountryString, string ModelString){
            var VM = await _aircraft_database_context.GetAircraftView(CategoryString, CountryString, ModelString);
            return View(VM);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(){
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
