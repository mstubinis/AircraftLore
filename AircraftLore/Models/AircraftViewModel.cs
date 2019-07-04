using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AircraftLore.Models
{
    public class AircraftViewModel
    {
        public List<Aircraft> Aircrafts { get; set; }
        public SelectList Countries { get; set; }
        public SelectList Categories { get; set; }
        public string CategoryString { get; set; }
        public string CountryString { get; set; }
        public string ModelString { get; set; }

        public AircraftViewModel(){

        }
    }
}
