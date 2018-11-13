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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SOAPSpotBotService : ISOAPSpotBotService
    {
        string strPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_data\\soap_bot.xml";

        public List<ParkingSpot> GetParkingSpotsInfo()
        {          
            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);
            XmlNodeList lst = doc.SelectNodes("//parkingSpot");
            List<ParkingSpot> parkingSpots = new List<ParkingSpot>();

            foreach (XmlNode spotNode in lst)
            {
                Status status = new Status();
                XmlNode valueNode = doc.SelectSingleNode($"/park/parkingSpot[name='{spotNode["name"].InnerText}']/status/value");
                status.value = valueNode.InnerText;
                status.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                ParkingSpot parkingSpot = new ParkingSpot(
                    spotNode["name"].InnerText,
                    status,
                    spotNode["location"].InnerText,
                    Convert.ToInt32(spotNode["batteryStatus"].InnerText)
                );

                parkingSpots.Add(parkingSpot);
            }

            return parkingSpots;
        }
    }
}
