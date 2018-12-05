using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkSS
{
    class Park
    {
        private String id;

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private int numberOfSpecialSpots;

        public int MyProperty
        {
            get { return numberOfSpecialSpots; }
            set { numberOfSpecialSpots = value; }
        }

        private String operatingHours;

        public String OperatingHours
        {
            get { return operatingHours; }
            set { operatingHours = value; }
        }

        private String geoLocationFile;

        public String GeoLocationFile
        {
            get { return geoLocationFile; }
            set { geoLocationFile = value; }
        }

        public Park(string id, string description, int numberOfSpecialSpots, string operatingHours, string geoLocationFile)
        {
            this.id = id;
            this.description = description;
            this.numberOfSpecialSpots = numberOfSpecialSpots;
            this.operatingHours = operatingHours;
            this.geoLocationFile = geoLocationFile;
        }

        public Park()
        {
        }
    }
}
