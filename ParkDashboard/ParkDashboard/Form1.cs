using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using RestSharp;
namespace ParkDashboard
{
    public partial class Form1 : Form
    {
        string baseURI = "http://localhost:59227/api/";
        RestClient restClient;

        public Form1()
        {
            InitializeComponent();
            comboBoxPark.SelectedIndex = 0; //defaut value
            comboBoxPark.DropDownStyle = ComboBoxStyle.DropDownList; //not editable        
        }

        private void buttonSpecificParkDetails_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            ///api/parks/{id} 
            restClient = new RestClient(baseURI + "parks/" + parkID);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                Park park = jSer.Deserialize<Park>(response.Content.ToString());
                richTextBox.AppendText(parkID);
                richTextBox.AppendText("\nDescription : " + park.Description);
                richTextBox.AppendText("\nNumber of Spots : " + park.NumberOfSpots);
                richTextBox.AppendText("\nOperating Hours : " + park.OperatingHours);
                richTextBox.AppendText("\nNumber of Special Spots : " + park.NumberOfSpecialSpots);
                richTextBox.AppendText("\nGeoLocation File: " + park.GeoLocationFile + "\n\n");
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
            
        }

        private void buttonOccupancyRate_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            ///api/parks/{id}/instantocupancyrate 
            restClient = new RestClient(baseURI + "parks/" + parkID + "/instantocupancyrate");

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                richTextBox.AppendText("Occupacy rate for " + comboBoxPark.SelectedItem + " : " + response.Content + "%\n\n");
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonSpecificLowBatterySensors_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            ///api/parks/{id}/lowbatteryparkingspots 
            restClient = new RestClient(baseURI + "parks/" + parkID + "/lowbatteryparkingspots");
   
            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<Spot> spots = jSer.Deserialize<List<Spot>>(response.Content.ToString());
              
                richTextBox.AppendText("Sensors that need batery replacement for " + comboBoxPark.SelectedItem + "\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText("Name: " + spot.Name + "\n");
                }
                           
                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonAllSpotsForAPark_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            ///api/parks/{id}/parkingspots 
            restClient = new RestClient(baseURI + "parks/" + parkID + "/parkingspots");

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<Spot> spots = jSer.Deserialize<List<Spot>>(response.Content.ToString());
              
                richTextBox.AppendText("Sensors in " + comboBoxPark.SelectedItem + "\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText("Name: " + spot.Name + "\n");
                }
                
                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void PrintErrorMessage(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.NotFound)
            {
                richTextBox.AppendText("No record(s) found. \n");
            }
            else
            {
                richTextBox.AppendText("Error connecting to client. \n");
            }
        }

        private string GetParkIdFromCombobox()
        {           
            if (comboBoxPark.SelectedItem.ToString() == "Park A")
            {
                return "Campus_2_A_Park1";
            }
            else 
            {
                return "Campus_2_B_Park2";
            }            
        }

        private void buttonStatusGivenPeriod_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            var timestamp_start = dateTimePickerStartingDate.Value.ToString("dd-MM-yyyy");
            var timestamp_end = dateTimePickerEndingDay.Value.ToString("dd-MM-yyyy");
            Regex pattern = new Regex("^(([0]?[0-9]|1[0-2]):[0-5][0-9])|((1[3-9]|2[0-3]):[0-5][0-9])$");
            if (!(pattern.IsMatch(textBoxEndingHour.Text.Trim()) && pattern.IsMatch(textBoxStartingHour.Text.Trim())))
            {
                MessageBox.Show("Please insert the time in the following format: hh:mm");
                return;

            }
            string[] timeStart = textBoxStartingHour.Text.Trim().Split(':');
            string[] timeEnd = textBoxEndingHour.Text.Trim().Split(':');
            ///api/parks/{id}/spotsstatus/{ timestamp_start}/{ hour_start}/{ minute_start}/{ timestamp_end}/{ hour_end}/{ minute_end}
            var uri = baseURI + "parks/" + parkID + "/spotsstatus/" + timestamp_start + "/" + timeStart[0] + "/" + timeStart[1] + "/" + timestamp_end + "/" + timeEnd[0] + "/" + timeEnd[1];
            restClient = new RestClient(uri);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<string> spots = jSer.Deserialize<List<string>>(response.Content.ToString());
               
                richTextBox.AppendText("All status for this period:\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText(spot + "\n");
                }
                
                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonStatusGivenMoment_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            var timestamp = dateTimePickerGivenMoment.Value.ToString("dd-MM-yyyy");
            Regex pattern = new Regex("^(([0]?[0-9]|1[0-2]):[0-5][0-9])|((1[3-9]|2[0-3]):[0-5][0-9])$");
            if (!pattern.IsMatch(textBoxGivenMomentHour.Text.Trim()))
            {
                MessageBox.Show("Please insert the time in the following format: hh:mm");
                return;

            }
            string[] time = textBoxGivenMomentHour.Text.Trim().Split(':');
            ///api/parks/{id}/spotsstatus/{timestamp}/{hour}/{minute}
            restClient = new RestClient(baseURI + "parks/" + parkID + "/spotsstatus/" + timestamp + "/" + time[0] + "/" + time[1]);
            var request = new RestRequest();

            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<string> spots = jSer.Deserialize<List<string>>(response.Content.ToString());

                richTextBox.AppendText("All status for this moment:\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText(spot + "\n");
                }

                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonSpecificParkFreeSpots_Click(object sender, EventArgs e)
        {
            var parkID = GetParkIdFromCombobox();
            var timestamp = dateTimePickerGivenMoment.Value.ToString("dd-MM-yyyy");
            Regex pattern = new Regex("^(([0]?[0-9]|1[0-2]):[0-5][0-9])|((1[3-9]|2[0-3]):[0-5][0-9])$");
            if (!pattern.IsMatch(textBoxGivenMomentHour.Text.Trim()))
            {
                MessageBox.Show("Please insert the time in the following format: hh:mm");
                return;

            }           
            string[] time = textBoxGivenMomentHour.Text.Trim().Split(':');
            ///api/parks/{id}/freespots/{timestamp}/{ hour}/{minute}
            restClient = new RestClient(baseURI + "parks/" + parkID + "/freespots/" + timestamp + "/" + time[0] + "/" + time[1]);
            var request = new RestRequest();

            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<Spot> spots = jSer.Deserialize<List<Spot>>(response.Content.ToString());

                richTextBox.AppendText("All free spots for this moment:\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText(spot.Name +" - " + spot.Status.Value + "\n");
                }

                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonGetAllParks_Click(object sender, EventArgs e)
        {
            ///api/parks
            restClient = new RestClient(baseURI + "/parks");
            var request = new RestRequest();

            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<Park> parks = jSer.Deserialize<List<Park>>(response.Content.ToString());

                richTextBox.AppendText("All parks in the plataform:\n");
                foreach (var park in parks)
                {
                    richTextBox.AppendText(park.Id + " - " + park.NumberOfSpots + " spots\n");
                }

                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonAllSensorsLowBattery_Click(object sender, EventArgs e)
        {
            ///api/spots/lowbatteryparkingspots 
            restClient = new RestClient(baseURI + "spots/lowbatteryparkingspots");
            var request = new RestRequest();

            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");
            
            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                List<Spot> spots = jSer.Deserialize<List<Spot>>(response.Content.ToString());
                richTextBox.AppendText("Sensors that need battery replacement:\n");
                foreach (var spot in spots)
                {
                    richTextBox.AppendText(spot.Name + "\n");
                }

                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }

        private void buttonInfoSpotGivenMoment_Click(object sender, EventArgs e)
        {
            var timestamp = dateTimePickerGivenMomentSpot.Value.ToString("dd-MM-yyyy");
            Regex pattern = new Regex("^(([0]?[0-9]|1[0-2]):[0-5][0-9])|((1[3-9]|2[0-3]):[0-5][0-9])$");
            if (!pattern.IsMatch(textBoxGivenMomentHourSpot.Text.Trim()))
            {
                MessageBox.Show("Please insert the time in the following format: hh:mm");
                return;

            }       
            if (textBoxSpotName.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the spot name text");
            }
            string[] time = textBoxGivenMomentHourSpot.Text.Trim().Split(':');
            string spotID = textBoxSpotName.Text.Trim();
            ///api/spots/{id}/{timestamp}/{hour}/{minute} 
            restClient = new RestClient(baseURI + "spots/" + spotID + "/" + timestamp + "/" + time[0] + "/" + time[1]);
            var request = new RestRequest();

            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JavaScriptSerializer jSer = new JavaScriptSerializer();
                Spot spot = jSer.Deserialize<Spot>(response.Content.ToString());
                var batteryStatus = spot.BatteryStatus == 0 ? "Low" : "Good";

                richTextBox.AppendText("Detailed information for spot " + spot.Name + "\n" + 
                    "\nPark: " + spot.Park_Id +
                    "\nLocation: "+ spot.Location +
                    "\nBaterryStatus: " + batteryStatus +
                    "\nValue: " + spot.Status.Value +
                    "\nTimestamp: " + spot.Status.Timestamp + "\n\n");
                

                richTextBox.AppendText(Environment.NewLine);
            }
            else
            {
                PrintErrorMessage(response.StatusCode);
            }
        }
    }
}
