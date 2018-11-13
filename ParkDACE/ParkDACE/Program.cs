using ParkDACE.SOAPSpotBotService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ParkDACE
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingSpot[] arraySpots;
            string strPathParkB = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"Campus_2_B_Park2.xlsx";
            using (SOAPSpotBotServiceClient client = new SOAPSpotBotServiceClient())
            {
                //Parques do B
                arraySpots = client.GetParkingSpotsInfo();
                
                //Preencher location (ficheiros excel)
                var locations = ReadNxMFromExcelFile(strPathParkB, "B6", "B15").ToArray();
                //var locations = ReadNxMFromExcelFile(strPathParkB, "B15", "B6");            
                for (int i = 0; i < locations.Length; i++ )
                {
                    arraySpots[i].location = locations[i];                 
                }

                foreach (var spot in arraySpots)
                {
                    Console.WriteLine(spot.location);
                }


            }
            
            Console.ReadKey();
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
