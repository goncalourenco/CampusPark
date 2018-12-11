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
    public class ParksController : ApiController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SmartPark.Properties.Settings.ConnString"].ConnectionString;
       
        // GET api/Parks
        public IHttpActionResult Get()
        {
            List<Park> parks = new List<Park>();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Parks", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Park park = new Park
                    {
                        Id = (string)reader["Id"],
                        Description = (string)reader["Description"],
                        NumberOfSpecialSpots = (int)reader["NumberOfSpecialSpots"],
                        OperatingHours = (string)reader["OperatingHours"],
                        GeoLocationFile = (string)reader["GeoLocationFile"],
                        NumberOfSpots = (int)reader["NumberOfSpots"],
                        Timestamp = (string)reader["Timestamp"]
                    };
                    parks.Add(park);
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

            return Ok(parks);
        }

        // GET api/parks/{id}
        public IHttpActionResult Get(string id)
        {
            Park park = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Parks where id = @id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    park = new Park
                    {
                        Id = (string)reader["Id"],
                        Description = (string)reader["Description"],
                        NumberOfSpecialSpots = (int)reader["NumberOfSpecialSpots"],
                        OperatingHours = (string)reader["OperatingHours"],
                        GeoLocationFile = (string)reader["GeoLocationFile"],
                        NumberOfSpots = (int)reader["NumberOfSpots"],
                        Timestamp = (string)reader["Timestamp"]
                    };
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

            return Ok(park);
        }

        
        [Route("api/parks/{id}/parkingspots")]
        public IHttpActionResult GetParkingSpotsForSpecificPark(string id)
        {
            List<Spot> spots = new List<Spot>();
            Spot spot = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from ParkingSpots where park_id = @id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
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

        [Route("api/parks/{id}/lowbatteryparkingspots")]
        public IHttpActionResult GetParkingSpotsWithLowBattery(string id)
        {
            List<Spot> spots = new List<Spot>();
            Spot spot = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from ParkingSpots where park_id = @id and batterystatus = '0'";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
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

        [Route("api/parks/{id}/instantocupancyrate")]
        public IHttpActionResult GetInstantOccupancyrate(string id)
        {
            float occupancyrate = 0;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select count(*) from ParkingSpots where park_id = @id and value='occupied'";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
                int number_of_ocupied_spots =  (int)cmd.ExecuteScalar();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "select numberofspots from parks where id = @id";
                cmd1.Connection = conn;
                cmd1.Parameters.AddWithValue("id", id);
                int number_of_spots = (int)cmd1.ExecuteScalar();
                occupancyrate = ((float)number_of_ocupied_spots / number_of_spots)*100;
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

            return Ok(occupancyrate);
        }

        [Route("api/parks/{id}/freespots/{timestamp}/{hour}/{minute}")]
        public IHttpActionResult GetFreeSpots(string id, string timestamp, string hour, string minute)
        {
            Spot spot = null;
            List<Spot> spots = new List<Spot>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select ps.name, park_id, location, BatteryStatus, CONCAT(sh.Timestamp, ' ', sh.Hour, ':', sh.Minute) as TimeStamp, sh.Value from ParkingSpots ps join SpotsHistory sh on ps.name = sh.name where sh.Timestamp = @timestamp and sh.Hour = @hour and sh.Minute = @minute and sh.value = 'free' and ps.park_id = @park_id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("timestamp", timestamp);
                cmd.Parameters.AddWithValue("hour", hour);
                cmd.Parameters.AddWithValue("minute", minute);
                cmd.Parameters.AddWithValue("park_id", id);
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
                    spots.Add(spot);
                }
                reader.Close();
                conn.Close();
                if (spots.Count == 0)
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

            return Ok(spots);
        }

        [Route("api/parks/{id}/spotsstatus/{timestamp}/{hour}/{minute}")]
        public IHttpActionResult GetSpotsStatus(string id, string timestamp, string hour, string minute)
        {
            List<String> status = new List<String>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select sh.name, sh.Value from ParkingSpots ps join SpotsHistory sh on ps.name = sh.name where sh.Timestamp = @timestamp and sh.Hour = @hour and sh.Minute = @minute and ps.park_id = @park_id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("timestamp", timestamp);
                cmd.Parameters.AddWithValue("hour", hour);
                cmd.Parameters.AddWithValue("minute", minute);
                cmd.Parameters.AddWithValue("park_id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status.Add(reader.GetString(0) + " - " + reader.GetString(1));
                }
                reader.Close();
                conn.Close();
                if (status.Count == 0)
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

            return Ok(status);
        }

        [Route("api/parks/{id}/spotsstatus/{timestamp_start}/{hour_start}/{minute_start}/{timestamp_end}/{hour_end}/{minute_end}")]
        public IHttpActionResult GetSpotsStatusForAGivenPeriod(string id, string timestamp_start, string hour_start, string minute_start, string timestamp_end, string hour_end, string minute_end)
        {
            List<String> status = new List<String>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select sh.Name, sh.Value, CONCAT(sh.Timestamp, ' ', sh.Hour, ':', sh.Minute) as Timestamp from ParkingSpots ps join SpotsHistory sh on ps.name = sh.name where ps.park_id = @park_id and CAST(CONCAT(sh.Timestamp, ' ', sh.Hour, ':', sh.Minute) AS datetime) >= CAST(CONCAT(@timestamp_start, ' ', @hour_start, ':', @minute_start) AS datetime) and CAST(CONCAT(sh.Timestamp, ' ', sh.Hour, ':', sh.Minute) AS datetime) <= CAST(CONCAT(@timestamp_end, ' ', @hour_end, ':', @minute_end) AS datetime)";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("park_id", id);
                cmd.Parameters.AddWithValue("timestamp_start", timestamp_start);
                cmd.Parameters.AddWithValue("hour_start", hour_start);
                cmd.Parameters.AddWithValue("minute_start", minute_start);
                cmd.Parameters.AddWithValue("timestamp_end", timestamp_end);
                cmd.Parameters.AddWithValue("hour_end", hour_end);
                cmd.Parameters.AddWithValue("minute_end", minute_end);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status.Add(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2));
                }
                reader.Close();
                conn.Close();
                if (status.Count == 0)
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

            return Ok(status);
        }
    }
}