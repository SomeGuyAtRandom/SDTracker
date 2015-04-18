using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public partial class Place
    {
        public Place() { }

        public int Id { get; set; }
        public string FiveDigit { get; set; }
        public int DistrictId { get; set; }
        public string PlaceLocation { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
