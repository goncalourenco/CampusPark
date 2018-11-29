using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace BOT_SpotSensors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISOAPSpotBotService
    {
        [OperationContract]
        List<ParkingSpot> GetParkingSpotsInfo();

        [OperationContract]
        String GetParkingSpotsInfoXML();
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
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        [DataMember]
        public Status Status
        {
            get { return  stStatus; }
            set {  stStatus = value; }
        }

        [DataMember]
        public string Location
        {
            get { return strLocation; }
            set { strLocation = value; }
        }

      
        [DataMember]
        public int BatteryStatus
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
        public string Value { get; set; }
        [DataMember]
        public string Timestamp { get; set; }

        public Status()
        {

        }
    }
}
