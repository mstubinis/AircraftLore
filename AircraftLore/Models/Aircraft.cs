using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AircraftLore.Models{
    public class Aircraft{
        public int ID { get; set; } //for the database ID (primary key)



        [StringLength(65, MinimumLength = 2), Required, Display(Name = "Aircraft Model")]
        public String ModelName { get; set; }


        [Required, RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")] //only letters allowed
        public String Country { get; set; }


        [Required]
        public String Description { get; set; }



        [Required, StringLength(65, MinimumLength = 2)]
        public String Category { get; set; }



        [Required, Display(Name = "Picture File")]
        public String PictureFile { get; set; }
    }
}
