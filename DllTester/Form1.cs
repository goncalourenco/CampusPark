using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllTester
{
    public partial class Form1 : Form
    {
        private BackgroundWorker bw = new BackgroundWorker();

        private ParkingSensorNodeDll.ParkingSensorNodeDll dll = null;

        public void DoWork(object sender, DoWorkEventArgs e)
        {
                dll.Initialize(NewSensorValueFunction, 3000);
        }

        
        public Form1()
        {
            InitializeComponent();
            bw.DoWork += new DoWorkEventHandler(DoWork);

        }

        //The callback...
        public void NewSensorValueFunction(string str)
        {
            //To have access to the listbox that is in other thread (Form)
            this.BeginInvoke((MethodInvoker)delegate
            {
                listBox1.Items.Add(str);
            }); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dll = new ParkingSensorNodeDll.ParkingSensorNodeDll();
            bw.RunWorkerAsync();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dll.Stop(); 
        }

    }
}
