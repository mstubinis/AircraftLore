using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AircraftLore.Models;
using Microsoft.AspNetCore.Authorization;



namespace AircraftLore.Controllers{
    public class AircraftController : Controller{

        public const string ADMINISTRATOR = "Administrator";

        private readonly AircraftDatabaseContext _aircraft_database_context;

        public AircraftController(AircraftDatabaseContext db){
            _aircraft_database_context = db;
        }

        // GET: Aircraft
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        public async Task<IActionResult> Index(string CategoryString, string CountryString, string ModelString){
            var VM = await _aircraft_database_context.GetAircraftView(CategoryString, CountryString, ModelString);
            return View(VM);
        }

        // GET: Aircraft/Details/5
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        public async Task<IActionResult> Details(int? id){
            if (id == null){
                return NotFound();
            }
            var aircraft = await _aircraft_database_context.Aircraft.FirstOrDefaultAsync(m => m.ID == id);
            if (aircraft == null){
                return NotFound();
            }
            return View(aircraft);
        }

        // GET: Aircraft/Create
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        public IActionResult Create(){
            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ModelName,Description,PictureFile,Country,Category")] Aircraft aircraft){
            if (ModelState.IsValid){
                _aircraft_database_context.Add(aircraft);
                await _aircraft_database_context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        public async Task<IActionResult> Edit(int? id){
            if (id == null){
                return NotFound();
            }
            var aircraft = await _aircraft_database_context.Aircraft.FindAsync(id);
            if (aircraft == null){
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("ID,ModelName,Description,PictureFile,Country,Category")] Aircraft aircraft){
            if (id != aircraft.ID){
                return NotFound();
            }
            if (ModelState.IsValid){
                try{
                    _aircraft_database_context.Update(aircraft);
                    await _aircraft_database_context.SaveChangesAsync();
                }catch (DbUpdateConcurrencyException){
                    if (!AircraftExists(aircraft.ID)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        public async Task<IActionResult> Delete(int? id){
            if (id == null){
                return NotFound();
            }
            var aircraft = await _aircraft_database_context.Aircraft.FirstOrDefaultAsync(m => m.ID == id);
            if (aircraft == null){
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [Authorize]
        [Authorize(Roles = ADMINISTRATOR)]
        [HttpPost, ValidateAntiForgeryToken] 
        public async Task<IActionResult> Delete(int id, bool unused){
            var aircraft = await _aircraft_database_context.Aircraft.FindAsync(id);
            _aircraft_database_context.Aircraft.Remove(aircraft);
            await _aircraft_database_context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id){
            return _aircraft_database_context.Aircraft.Any(e => e.ID == id);
        }
    }
}