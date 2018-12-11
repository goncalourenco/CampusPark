using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPark.Models
{
    public class Park
    {
        public String Id { get; set; }

        public String Description { get; set; }

        public int NumberOfSpecialSpots { get; set; }

        public String OperatingHours { get; set; }

        public String GeoLocationFile { get; set; }

        public int NumberOfSpots { get; set; }

        public String Timestamp { get; set; }

    }
}