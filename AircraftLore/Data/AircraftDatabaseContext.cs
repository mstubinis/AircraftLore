using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AircraftLore.Models{
    public class AircraftDatabaseContext : DbContext{

        //handles the task of connecting to the database and mapping objects to database records
        public AircraftDatabaseContext(DbContextOptions<AircraftDatabaseContext> options): base(options)
        {
        }

        public DbSet<AircraftLore.Models.Aircraft> Aircraft { get; set; }

        public async Task<AircraftViewModel> GetAircraftView(string CategoryString, string CountryString, string ModelString){
            // LINQ https://docs.microsoft.com/en-us/dotnet/standard/using-linq

            var categoryQuery =
                from m in Aircraft
                orderby m.Category
                select m.Category;

            var countryQuery =
                from m in Aircraft
                orderby m.Country
                select m.Country;

            var aircrafts =
                from m in Aircraft
                select m;

            if (!String.IsNullOrEmpty(CategoryString)){
                aircrafts = aircrafts.Where(s => s.Category.Contains(CategoryString));
            }
            if (!String.IsNullOrEmpty(CountryString)){
                aircrafts = aircrafts.Where(s => s.Country.Contains(CountryString));
            }
            if (!String.IsNullOrEmpty(ModelString)){
                aircrafts = aircrafts.Where(s => s.ModelName.Contains(ModelString));
            }


            var processCountries = new SelectList(await countryQuery.Distinct().ToListAsync());
            foreach (var country in processCountries){
                country.Text = Path.GetFileNameWithoutExtension(country.Text);
            }
            var Aircraft_ViewModel = new AircraftViewModel();
            Aircraft_ViewModel.Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());


            Aircraft_ViewModel.Countries = processCountries;
            Aircraft_ViewModel.Aircrafts = await aircrafts.ToListAsync();

            Aircraft_ViewModel.CategoryString = CategoryString;
            Aircraft_ViewModel.CountryString = CountryString;
            Aircraft_ViewModel.ModelString = ModelString;

            return Aircraft_ViewModel;
        }
    }
}
