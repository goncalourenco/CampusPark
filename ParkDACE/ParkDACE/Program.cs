﻿using ParkDACE.SOAPSpotBotService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParkDACE
{
    class Program
    {
        ParkingSensorNodeDll.ParkingSensorNodeDll dll;
        public int IndexParkA { get; set; }
        public int NumberOfSpotsParkA { get; set; }
        XmlDocument configurationXml;
        string[] mStrTopicsInfo = { "spots", "parksInfo" };
        MqttClient mClient;
        public string[] LocationsParkA { get; set; }
        public string ParkA_ID { get; set; }
        public string ParkB_ID { get; set; }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Setup();
        }

        private void Setup()
        {
            configurationXml = new XmlDocument();
            configurationXml.Load("ParkingNodesConfig.xml");
            SetupMosquitto();

            ParkB_ID = configurationXml.SelectSingleNode("/parkingLocation/provider[connectionType='SOAP']/parkInfo/id").InnerText;
            ParkA_ID = configurationXml.SelectSingleNode("/parkingLocation/provider[connectionType='DLL']/parkInfo/id").InnerText;

            NumberOfSpotsParkA = Int32.Parse(configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkA_ID}']/numberOfSpots").InnerText);
            var fileNameParkA = configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkA_ID}']/geoLocationFile").InnerText;


            string strPathParkA = AppDomain.CurrentDomain.BaseDirectory.ToString() + fileNameParkA;
            LocationsParkA = ReadNxMFromExcelFile(strPathParkA, "A6", "B" + (5 + NumberOfSpotsParkA)).ToArray();

            var startTimeSpan = TimeSpan.Zero;

            var units = configurationXml.SelectSingleNode("/parkingLocation/@units").InnerText;
            TimeSpan periodTimeSpan;
            int refreshRate = Int16.Parse(configurationXml.SelectSingleNode("/parkingLocation/@refreshRate").InnerText);
            switch (units)
            {
                case "hours":
                    periodTimeSpan = TimeSpan.FromHours(refreshRate);
                    break;
                case "seconds":
                    periodTimeSpan = TimeSpan.FromSeconds(refreshRate);
                    break;
                default: //minutes
                    periodTimeSpan = TimeSpan.FromMinutes(refreshRate);
                    break;
            }


            GetAndPublishInfoForParks();

            var timer = new System.Threading.Timer((e) =>
            {
                Init();

            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadKey();
        }

        public void Init()
        {
            GetAndPublishSpotsForParkB();

            IndexParkA = 0;
            dll = new ParkingSensorNodeDll.ParkingSensorNodeDll();
            dll.Initialize(GetAndPublishSpotsForParkA, 50);
        }

        private void GetAndPublishSpotsForParkB()
        {
            ParkingSpot[] arraySpotsB;
            var fileNameParkB = configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkB_ID}']/geoLocationFile").InnerText;
            string strPathParkB = AppDomain.CurrentDomain.BaseDirectory.ToString() + fileNameParkB;
            using (SOAPSpotBotServiceClient client = new SOAPSpotBotServiceClient())
            {
                //Parques do B
                XmlNode numberOfSpotsNode = configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkB_ID}']/numberOfSpots");
                int numberOfSpots = Int32.Parse(numberOfSpotsNode.InnerText);
                arraySpotsB = client.GetParkingSpotsInfo(numberOfSpots);

                Console.WriteLine("Received Data from BOT-SpotSensors");
                foreach (ParkingSpot parkingSpot in arraySpotsB)
                {
                    Console.WriteLine("Name: " + parkingSpot.Name + Environment.NewLine
                        + "ID: " + parkingSpot.Id + Environment.NewLine
                        + "Status: " + parkingSpot.Status.Value + Environment.NewLine
                        + "Timestamp:" + parkingSpot.Status.Timestamp + Environment.NewLine
                        + "Location: " + parkingSpot.Location + Environment.NewLine);
                }

                //Preencher location (ficheiros excel)
                var locations = ReadNxMFromExcelFile(strPathParkB, "A6", "B" + (5 + arraySpotsB.Count())).ToArray();
                Console.WriteLine("Transformed data for ParkB");
                for (int i = 0; i < locations.Length; i++)
                {
                    foreach (ParkingSpot parkingSpot in arraySpotsB)
                    {
                        if (parkingSpot.Name == locations[i])
                        {
                            parkingSpot.Location = locations[++i];
                            parkingSpot.Status.Timestamp = DateTime.Parse(parkingSpot.Status.Timestamp).ToString("dd/MM/yyyy HH:mm:ss");
                            Console.WriteLine("Name: " + parkingSpot.Name + Environment.NewLine
                                + "ID: " + parkingSpot.Id + Environment.NewLine
                                + "Status: " + parkingSpot.Status.Value + Environment.NewLine
                                + "Timestamp:" + parkingSpot.Status.Timestamp + Environment.NewLine
                                + "Location: " + parkingSpot.Location + Environment.NewLine);
                            
                            //Publish
                           string spotXml = serializeParkingSpot(parkingSpot);
                           mClient.Publish(mStrTopicsInfo[0], Encoding.UTF8.GetBytes(spotXml));
                        }
                    }
                }
            }
        }

        //The callback...
        public void GetAndPublishSpotsForParkA(string str)
        {
            if (IndexParkA >= NumberOfSpotsParkA)
            {
                dll.Stop();
                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                Console.WriteLine(str);
                string[] spotInfo = str.Split(';');
                ParkingSpot parkingSpot = new ParkingSpot();
                parkingSpot.Name = spotInfo[1];
                parkingSpot.Id = spotInfo[0];
                Status status = new Status();
                status.Timestamp = spotInfo[2];
                status.Value = spotInfo[3];
                if (status.Value.Equals("0"))
                {
                    status.Value = "free";
                }
                else if (status.Value.Equals("1"))
                {
                    status.Value = "occupied";
                }
                parkingSpot.Status = status;
                parkingSpot.BatteryStatus = Convert.ToInt32(spotInfo[4]);

                for (int i = 0; i < LocationsParkA.Length; i++)
                {
                    if (parkingSpot.Name == LocationsParkA[i])
                    {
                        parkingSpot.Location = LocationsParkA[++i];
                        parkingSpot.Status.Timestamp = DateTime.Parse(parkingSpot.Status.Timestamp).ToString("dd/MM/yyyy HH:mm:ss");
                        Console.WriteLine("Name: " + parkingSpot.Name + Environment.NewLine
                        + "ID: " + parkingSpot.Id + Environment.NewLine
                        + "Status: " + parkingSpot.Status.Value + Environment.NewLine
                        + "Timestamp:" + parkingSpot.Status.Timestamp + Environment.NewLine
                        + "Location: " + parkingSpot.Location + Environment.NewLine);
                    }
                }

                IndexParkA++;

                //publish
               string spotXml = serializeParkingSpot(parkingSpot);
               mClient.Publish(mStrTopicsInfo[0], Encoding.UTF8.GetBytes(spotXml));
            }
        }

        private void GetAndPublishInfoForParks()
        {
            XmlDocument parksInfo = new XmlDocument();
            XmlElement parks = parksInfo.CreateElement("parks");

            XmlElement parkA = parksInfo.CreateElement("park");
            parkA.InnerXml = configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkA_ID}']").InnerXml;
            XmlElement timestampParkA = parksInfo.CreateElement("timestamp");
            timestampParkA.InnerText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            

            XmlElement parkB = parksInfo.CreateElement("park");
            parkB.InnerXml = configurationXml.SelectSingleNode($"/parkingLocation/provider/parkInfo[id='{ParkB_ID}']").InnerXml;
            XmlElement timestampParkB = parksInfo.CreateElement("timestamp");
            timestampParkB.InnerText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            parkA.AppendChild(timestampParkA);
            parkB.AppendChild(timestampParkB);
            parks.AppendChild(parkA);
            parks.AppendChild(parkB);
            parksInfo.AppendChild(parks);

            Console.WriteLine(parksInfo.OuterXml);
            mClient.Publish(mStrTopicsInfo[1], Encoding.UTF8.GetBytes(parksInfo.OuterXml));
        }

        private string serializeParkingSpot(ParkingSpot parkingSpot)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ParkingSpot));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, parkingSpot);
                    return sww.ToString();
                }
            }
        }

        private void SetupMosquitto()
        {
            mClient = new MqttClient("127.0.0.1");
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE};//QoS
        }

        public static List<string> ReadNxMFromExcelFile(string filename, string N, string M)
        {
            List<string> result = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            Excel.Workbook workbook = excelApp.Workbooks.Open(filename);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            //n-> linha ex A1
            //m-> coluna ex B3
            Excel.Range range = worksheet.get_Range(N, M);

            foreach (Excel.Range item in range)
            {
                result.Add(item.Value);
            }

            workbook.Close();
            excelApp.Quit();

            cleanResources(excelApp, workbook);

            return result;
        }

        private static void cleanResources(Excel.Application excelApp, Excel.Workbook workbook)
        {
            ReleaseCOMObject(workbook);
            ReleaseCOMObject(excelApp);
        }

        public static void ReleaseCOMObject(Object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}
