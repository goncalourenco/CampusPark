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
    public class SOAPSpotBotService : ISOAPSpotBotService
    {

        public List<ParkingSpot> GetParkingSpotsInfo(int numberOfSpots)
        {
            String spotsxml = AddSpots(numberOfSpots);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(spotsxml);
            XmlNodeList lst = doc.SelectNodes("//parkingSpot");
            List<ParkingSpot> parkingSpots = new List<ParkingSpot>();

            foreach (XmlNode spotNode in lst)
            {
                Status status = new Status();
                XmlNode valueNode = doc.SelectSingleNode($"/park/parkingSpot[name='{spotNode["name"].InnerText}']/status/value");
                status.Value = valueNode.InnerText;
                XmlNode valueTimestamp = doc.SelectSingleNode($"/park/parkingSpot[name='{spotNode["name"].InnerText}']/status/timestamp");
                status.Timestamp = valueTimestamp.InnerText;

                ParkingSpot parkingSpot = new ParkingSpot(
                    spotNode["id"].InnerText,
                    spotNode["name"].InnerText,
                    status,
                    spotNode["location"].InnerText,
                    Convert.ToInt32(spotNode["batteryStatus"].InnerText)
                );

                parkingSpots.Add(parkingSpot);
            }
            return parkingSpots;
        }

        private String AddSpots(int numberOfSpots)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("park");
            doc.AppendChild(root);

            string[] values = new string[] { "free", "occupied" };
            string[] bateryStatus = new string[] { "0", "1" };

            for (int i = 1; i <= numberOfSpots; i++)
            {
                XmlElement parkingSpot = doc.CreateElement("parkingSpot");

                XmlElement id = doc.CreateElement("id");
                id.InnerText = "Campus_2_B_Park2";

                XmlElement type = doc.CreateElement("type");
                type.InnerText = "ParkingSpot";

                XmlElement name = doc.CreateElement("name");
                name.InnerText = "B-" + i;

                XmlElement location = doc.CreateElement("location");
                location.InnerText = "";

                XmlElement status = doc.CreateElement("status");

                XmlElement value = doc.CreateElement("value");

                value.InnerText = GenerateRandomValueFromStringArray(values);

                XmlElement timestamp = doc.CreateElement("timestamp");
                timestamp.InnerText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                XmlElement batteryStatus = doc.CreateElement("batteryStatus");
                batteryStatus.InnerText = GenerateRandomValueFromStringArray(bateryStatus);

                root.AppendChild(parkingSpot);
                parkingSpot.AppendChild(id);
                parkingSpot.AppendChild(type);
                parkingSpot.AppendChild(name);
                parkingSpot.AppendChild(location);
                parkingSpot.AppendChild(status);
                status.AppendChild(value);
                status.AppendChild(timestamp);
                parkingSpot.AppendChild(batteryStatus);

                doc.Save(Console.Out);
            }

            return doc.OuterXml;
        }

        public String GetParkingSpotsInfoXML(int numberOfSpots)
        {
            string xmlSpots = AddSpots(numberOfSpots);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlSpots);
            return doc.OuterXml;
        }

        private string GenerateRandomValueFromStringArray(string[] values)
        {
            Random random = new Random();
            int index = random.Next(values.Length);
            return values[index];
        }
    }
}
