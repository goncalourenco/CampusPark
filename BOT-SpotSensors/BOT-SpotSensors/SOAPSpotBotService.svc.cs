﻿using System;
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
            RemoveAllXmlChilds();
            AddSpots();
            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);
            XmlNodeList lst = doc.SelectNodes("//parkingSpot");
            List<ParkingSpot> parkingSpots = new List<ParkingSpot>();

            foreach (XmlNode spotNode in lst)
            {
                Status status = new Status();
                XmlNode valueNode = doc.SelectSingleNode($"/park/parkingSpot[name='{spotNode["name"].InnerText}']/status/value");
                status.Value = valueNode.InnerText;
                status.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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

        private void AddSpots()
        {
            XmlDocument doc = new XmlDocument();
           
            doc.Load(strPath);

            XmlNode root = doc.SelectSingleNode("/park");

            string[] values = new string[] { "free", "occupied" };
            string[] bateryStatus = new string[] { "0", "1" };

            for (int i = 0; i < 10; i++)
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
                timestamp.InnerText = "";

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

                doc.Save(strPath);
            }
        }

        public String GetParkingSpotsInfoXML()
        {
            AddSpots();
            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);
            return doc.OuterXml;
        }

        private string GenerateRandomValueFromStringArray(string[] values)
        {
            Random random = new Random();

            int index = random.Next(values.Length);

            return values[index];
        }

        private void RemoveAllXmlChilds()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(strPath);

            XmlNode root = xml.SelectSingleNode("/park");
            root.RemoveAll();

            xml.Save(strPath);
        }
    }
}
