using Netrom.WindowsService.SharingFiles;
using System;
using System.ServiceProcess;
using System.Timers;


namespace Netrom.WindowsService
{

    public partial class Service1 : ServiceBase
    {

        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug() {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            InitializeCron();
        }

        protected override void OnStop()
        {
            //System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "OnStop.txt");
        }

        /// <summary>
        /// Initialize the cron
        /// </summary>
        private void InitializeCron()
        {
            new Cron();
        }

    }
}
