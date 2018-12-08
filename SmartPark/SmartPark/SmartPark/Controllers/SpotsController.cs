using SmartPark.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartPark.Controllers
{
    public class SpotsController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SmartPark.Properties.Settings.ConnString"].ConnectionString;

        [Route("api/spots/lowbatteryparkingspots")]
        public IHttpActionResult GetLowBatteryParkingSpots()
        {
            List<Spot> spots = new List<Spot>();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from ParkingSpots where BatteryStatus = '0'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Spot spot = new Spot
                    {
                        Park_Id = (string)reader["Park_Id"],
                        Name = (string)reader["Name"],
                        Status = new Status
                        {
                            Value = (string)reader["Value"],
                            Timestamp = (string)reader["Timestamp"]
                        },
                        Location = (string)reader["Location"],
                        BatteryStatus = (int)reader["BatteryStatus"]
                    };
                    spots.Add(spot);
                }
                reader.Close();
                conn.Close();


            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }

            return Ok(spots);
        }

        
        [Route("api/spots/{id}/{timestamp}/{hour}/{minute}")]
        public IHttpActionResult GetSpot(string id, string timestamp, string hour, string minute)
        {
            Spot spot = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select ps.name, park_id, location, BatteryStatus, CONCAT(sh.Timestamp, ' ', sh.Hour, ':', sh.Minute) as TimeStamp, sh.Value from ParkingSpots ps join SpotsHistory sh on ps.name = sh.name where ps.name = @name and sh.Timestamp = @timestamp and sh.Hour = @hour and sh.Minute = @minute";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("name", id);
                cmd.Parameters.AddWithValue("timestamp", timestamp);
                cmd.Parameters.AddWithValue("hour", hour);
                cmd.Parameters.AddWithValue("minute", minute);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    spot = new Spot
                    {
                        Park_Id = (string)reader["Park_Id"],
                        Name = (string)reader["Name"],
                        Status = new Status
                        {
                            Value = (string)reader["Value"],
                            Timestamp = (string)reader["Timestamp"]
                        },
                        Location = (string)reader["Location"],
                        BatteryStatus = (int)reader["BatteryStatus"]
                    };
                }
                reader.Close();
                conn.Close();
                if (spot == null)
                {
                   return NotFound();
                }
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }

            return Ok(spot);
        }
    }
}