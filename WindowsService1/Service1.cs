using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        ScanFile scanFile = new ScanFile();

        public Service1()
        {
            InitializeComponent();
        }


        public void onDebug() {
            OnStart(null);
        }


        protected override void OnStart(string[] args)
        {
           scanFile.processFile();
        }

        protected override void OnStop()
        {
            System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "OnStop.txt");
        }
    }
}
