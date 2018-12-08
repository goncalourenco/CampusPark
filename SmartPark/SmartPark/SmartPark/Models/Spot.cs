using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPark.Models
{
    public class Spot
    {
        public string Park_Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public string Location { get; set; }

        public int BatteryStatus { get; set; }
    }

    public class Status
    {
        public string Value { get; set; }

        public string Timestamp { get; set; }
    }
}