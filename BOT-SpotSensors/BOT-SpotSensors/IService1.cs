using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BOT_SpotSensors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<ParkingSpot> GetParkingSpotsInfo();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ParkingSpot
    {
       
        private string strName;
        private Status stStatus;
        private string strLocation;
        private int intBatteryStatus;

        [DataMember]
        public string name
        {
            get { return strName; }
            set { strName = value; }
        }

        [DataMember]
        public Status status
        {
            get { return  stStatus; }
            set {  stStatus = value; }
        }

        [DataMember]
        public string location
        {
            get { return strLocation; }
            set { strLocation = value; }
        }

      
        [DataMember]
        public int batteryStatus
        {
            get { return intBatteryStatus; }
            set { intBatteryStatus = value; }
        }

        public ParkingSpot()
        {

        }

        public ParkingSpot(string name, Status status, string location, int batteryStatus)
        {
            this.strName = name;
            this.stStatus = status;
            this.strLocation = location;
            this.intBatteryStatus = batteryStatus;
        }
    }

    [DataContract]
    public class Status
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public string timestamp { get; set; }

        public Status()
        {

        }
    }
}
