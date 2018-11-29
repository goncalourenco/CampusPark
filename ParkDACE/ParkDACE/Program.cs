using ParkDACE.SOAPSpotBotService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParkDACE
{
    class Program
    {

        /**
         * TODO:
         * Dentro do For do arraySpotsB fazer publish para o canal "ParkB" o spot depois de preencher a localizacao
         * No metodo callback fazer publish para o canal ParkA o spot depois depois de preencher a localizacao
         **/

        ParkingSensorNodeDll.ParkingSensorNodeDll dll;
        public int IndexParkA { get; set; }

        static void Main(string[] args)
        {

            Program program = new Program();
            program.Init();
        }

        public void Init()
        {
            GetAndPublishSpotsForParkB();

            IndexParkA = 0;
            dll = new ParkingSensorNodeDll.ParkingSensorNodeDll();
            dll.Initialize(NewSensorValueFunction, 100);

            Console.ReadKey();
        }

        private void GetAndPublishSpotsForParkB()
        {
            ParkingSpot[] arraySpotsB;
            string strPathParkB = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Campus_2_B_Park2.xlsx";
            using (SOAPSpotBotServiceClient client = new SOAPSpotBotServiceClient())
            {
                //Parques do B
                arraySpotsB = client.GetParkingSpotsInfo();

                //Preencher location (ficheiros excel)
                var locations = ReadNxMFromExcelFile(strPathParkB, "B6", "B15").ToArray();
                for (int i = 0; i < locations.Length; i++)
                {
                    arraySpotsB[i].Location = locations[i];
                    //publish
                }
            }
        }

        //The callback...
        public void NewSensorValueFunction(string str)
        {
            if (IndexParkA >= 15)
            {
                dll.Stop();
            }
            else
            {
                Console.WriteLine(str);
                string[] spotInfo = str.Split(';');
                ParkingSpot parkingSpot = new ParkingSpot();
                parkingSpot.Name = spotInfo[1];

                Status status = new Status();
                status.Timestamp = spotInfo[2];
                status.Value = spotInfo[3];
                parkingSpot.Status = status;
                parkingSpot.BatteryStatus = Convert.ToInt32(spotInfo[4]);

                string strPathParkA = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Campus_2_A_Park1.xlsx";
                var locations = ReadNxMFromExcelFile(strPathParkA, "B6", "B20").ToArray();
                parkingSpot.Location = locations[IndexParkA++];
                //publis
            }
        }

        public static List<string> ReadNxMFromExcelFile(string filename, string N, string M)
        {
            List<string> result = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            Excel.Workbook workbook = excelApp.Workbooks.Open(filename);
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            //n-> linha ex A1 PODE SER AO CONTRARIO
            //m-> coluna ex B3 PODE SER AO CONTRARIO
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
