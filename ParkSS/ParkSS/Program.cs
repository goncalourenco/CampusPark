﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ParkSS
{
    class Program
    {
        MqttClient mqttClient = null;
        string[] topics = { "spots", "parksInfo" };

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ParkSS.Properties.Settings.ConnStr"].ConnectionString;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
        }

        private void Init()
        {
            mqttClient = new MqttClient("127.0.0.1");
            mqttClient.Connect(Guid.NewGuid().ToString());
            if (!mqttClient.IsConnected)
            {
                Console.WriteLine("client not connected");
            }

            mqttClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            mqttClient.Subscribe(topics, qosLevels);
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            switch (e.Topic)
            {
                case "spots":
                    Console.WriteLine($"{e.Topic} : {Encoding.UTF8.GetString(e.Message)}" + Environment.NewLine);
                    ParkingSpot spot = DeserializeParkingSpot(Encoding.UTF8.GetString(e.Message));
                    InsertOrUpdateSpotsTable(spot);
                    break;
                case "parksInfo":
                    Console.WriteLine($"{e.Topic} : {Encoding.UTF8.GetString(e.Message)} INFO" + Environment.NewLine);
                    var parkList = DeserializeParks(Encoding.UTF8.GetString(e.Message));
                    InsertParksTable(parkList);
                    break;
            }
        }

        private void InsertParksTable(List<Park> parkList)
        {
            foreach (Park park in parkList)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Parks WHERE id=@id", connection);
                    cmd.Parameters.AddWithValue("@id", park.Id);
                    cmd.ExecuteNonQuery();
                    SqlDataReader reader = cmd.ExecuteReader();
                    bool hasRows = reader.HasRows;
                    int numberOfSpotsDB = 0;
                    if (hasRows)
                    {
                        reader.Read();
                        numberOfSpotsDB = (int)reader["NumberOfSpots"];
                    }
                    reader.Close();
                    if (!hasRows)
                    {
                        cmd = new SqlCommand("INSERT INTO Parks(Id, Description, NumberOfSpecialSpots, OperatingHours, GeoLocationFile, NumberOfSpots, Timestamp) VALUES(@id, @description, @numberOfSpecialSpots, @operatingHours, @geoLocationFile, @numberOfSpots, @timestamp)", connection);
                        cmd.Parameters.AddWithValue("@id", park.Id);
                        cmd.Parameters.AddWithValue("@description", park.Description);
                        cmd.Parameters.AddWithValue("@numberOfSpecialSpots", park.NumberOfSpecialSpots);
                        cmd.Parameters.AddWithValue("@operatingHours", park.OperatingHours);
                        cmd.Parameters.AddWithValue("@geoLocationFile", park.GeoLocationFile);
                        cmd.Parameters.AddWithValue("@numberOfSpots", park.NumberOfSpots);
                        cmd.Parameters.AddWithValue("@timestamp", park.Timestamp);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        if(park.NumberOfSpots != numberOfSpotsDB)
                        {
                            cmd = new SqlCommand("UPDATE Parks SET NumberOfSpots=@numberofspots, Timestamp=@timestamp WHERE Id=@id", connection);
                            cmd.Parameters.AddWithValue("@numberOfSpots", park.NumberOfSpots);
                            cmd.Parameters.AddWithValue("@timestamp", park.Timestamp);
                            cmd.Parameters.AddWithValue("@id", park.Id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void InsertOrUpdateSpotsTable(ParkingSpot spot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ParkingSpots WHERE Name=@name", connection);
                cmd.Parameters.AddWithValue("@name", spot.Name);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                bool hasRows = reader.HasRows;
                reader.Close();
                if (!hasRows)
                {
                    cmd = new SqlCommand("INSERT INTO ParkingSpots(Name, Park_Id, Location, BatteryStatus, Timestamp, Value) VALUES(@name, @id, @location, @batteryStatus, @timestamp, @value)", connection);
                    cmd.Parameters.AddWithValue("@id", spot.Id);
                    cmd.Parameters.AddWithValue("@location", spot.Location);
                    cmd.Parameters.AddWithValue("@batteryStatus", spot.BatteryStatus);
                    cmd.Parameters.AddWithValue("@timestamp", spot.Status.Timestamp);
                    cmd.Parameters.AddWithValue("@value", spot.Status.Value);
                    cmd.Parameters.AddWithValue("@name", spot.Name);
                    cmd.ExecuteNonQuery();

                    string timestamp = spot.Status.Timestamp;
                    timestamp = timestamp.Replace("/", "-");
                    string[] dateAndTime = timestamp.Split(' ');
                    timestamp = dateAndTime[0];
                    string[] time = dateAndTime[1].Split(':');
                    string hour = time[0];
                    string minute = time[1];

                    cmd = new SqlCommand("INSERT INTO SpotsHistory(Name, Timestamp, Value, Hour, Minute) VALUES(@name, @timestamp, @value, @hour, @minute)", connection);
                    cmd.Parameters.AddWithValue("@timestamp", timestamp);
                    cmd.Parameters.AddWithValue("@value", spot.Status.Value);
                    cmd.Parameters.AddWithValue("@name", spot.Name);
                    cmd.Parameters.AddWithValue("@hour", hour);
                    cmd.Parameters.AddWithValue("@minute", minute);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    /**
                     * Se o campo alterado for value, copiar linha para o historico e só depois fazer update 
                     * */
                    cmd = new SqlCommand("SELECT Name, Timestamp, Value FROM ParkingSpots WHERE Name=@name", connection);
                    cmd.Parameters.AddWithValue("@name", spot.Name);
                    cmd.ExecuteNonQuery();
                    SqlDataReader valueFromDB = cmd.ExecuteReader();
                    valueFromDB.Read();
                    string value = valueFromDB.GetString(2), timestamp = spot.Status.Timestamp, name = valueFromDB.GetString(0);
                    timestamp = timestamp.Replace("/","-");
                    string[] dateAndTime = timestamp.Split(' ');
                    timestamp = dateAndTime[0];
                    string hour = dateAndTime[1].Substring(0, 2);                 
                    string minute = dateAndTime[1].Substring(3, 2);
                    valueFromDB.Close();

                    if (value != spot.Status.Value)
                    {                    
                        cmd = new SqlCommand("INSERT INTO SpotsHistory(Name, Timestamp, Value, Hour, Minute) VALUES(@name, @timestamp, @value, @hour, @minute)", connection);
                        cmd.Parameters.AddWithValue("@timestamp", timestamp);
                        cmd.Parameters.AddWithValue("@value", spot.Status.Value);
                        cmd.Parameters.AddWithValue("@name", spot.Name);
                        cmd.Parameters.AddWithValue("@hour", hour);
                        cmd.Parameters.AddWithValue("@minute", minute);
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new SqlCommand("UPDATE ParkingSpots SET BatteryStatus=@batteryStatus, Timestamp=@timestamp, Value=@value WHERE Name=@name", connection);
                    cmd.Parameters.AddWithValue("@batteryStatus", spot.BatteryStatus);
                    cmd.Parameters.AddWithValue("@timestamp", spot.Status.Timestamp);
                    cmd.Parameters.AddWithValue("@value", spot.Status.Value);
                    cmd.Parameters.AddWithValue("@name", spot.Name);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private List<Park> DeserializeParks(String xml)
        {
            List<Park> parks = new List<Park>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList list = doc.SelectNodes("//park");        
            XmlSerializer serializer = new XmlSerializer(typeof(Park));
        
            foreach (XmlNode parkNode in list)
            {
                Park park = new Park();
                park.Id = parkNode["id"].InnerText;
                park.Description = parkNode["description"].InnerText;
                park.NumberOfSpecialSpots = Int16.Parse(parkNode["numberOfSpecialSpots"].InnerText);
                park.OperatingHours = parkNode["operatingHours"].InnerText;
                park.GeoLocationFile = parkNode["geoLocationFile"].InnerText;
                park.NumberOfSpots = Int16.Parse(parkNode["numberOfSpots"].InnerText);
                park.Timestamp = parkNode["timestamp"].InnerText;
                parks.Add(park);
            }
            return parks;
        }

        private ParkingSpot DeserializeParkingSpot(String xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ParkingSpot));
            using (TextReader reader = new StringReader(xml))
            {
                return (ParkingSpot)serializer.Deserialize(reader);
            }       
        }
    }
}
