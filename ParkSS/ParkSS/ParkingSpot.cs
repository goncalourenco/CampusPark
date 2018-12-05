using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkSS
{
    public class ParkingSpot
    {
        private string strId;
        private string strName;
        private Status stStatus;
        private string strLocation;
        private int intBatteryStatus;

        public string Id
        {
            get { return strId; }
            set { strId = value; }
        }

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public Status Status
        {
            get { return stStatus; }
            set { stStatus = value; }
        }

        public string Location
        {
            get { return strLocation; }
            set { strLocation = value; }
        }

        public int BatteryStatus
        {
            get { return intBatteryStatus; }
            set { intBatteryStatus = value; }
        }

        public ParkingSpot()
        {

        }

        public ParkingSpot(string id, string name, Status status, string location, int batteryStatus)
        {
            this.strName = name;
            this.stStatus = status;
            this.strLocation = location;
            this.intBatteryStatus = batteryStatus;
            this.strId = id;
        }
    }
}
