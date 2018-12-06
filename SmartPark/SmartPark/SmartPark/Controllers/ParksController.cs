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
                        GeoLocationFile = (string)reader["GeoLocationFile"]
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

        // GET api/<controller>/5
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
                        GeoLocationFile = (string)reader["GeoLocationFile"]
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

        
        [Route("api/parks/parkingspots/{id}")]
        public IHttpActionResult GetParkingSpotForSpecificPark(string id)
        {
            List<Spot> spots = new List<Spot>();
            Spot spot = null;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from ParkingSpots where id = @id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    spot = new Spot
                    {
                        Id = (string)reader["Id"],
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

        [Route("api/parks/parkingspotswithlowbattery/{id}")]
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
                cmd.CommandText = "select * from ParkingSpots where id = @id and batterystatus = '0'";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    spot = new Spot
                    {
                        Id = (string)reader["Id"],
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

        [Route("api/parks/instantocupancyrate/{id}")]
        public IHttpActionResult GetInstantOccupancyrate(string id)
        {
            float occupancyrate = 0;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select count(*) from ParkingSpots where id = @id and value='occupied'";
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
    }
}