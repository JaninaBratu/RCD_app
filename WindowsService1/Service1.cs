using System;
using System.ServiceProcess;
using System.Timers;


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
            CronProcess();
        }

        protected override void OnStop()
        {
            System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "OnStop.txt");
        }

        private void CronProcess()
        {

            System.Timers.Timer cronTimer = new System.Timers.Timer();

            // cron behaviour
            cronTimer.Elapsed += scanProcess;

            // set the Interval to 7 seconds (7000 milliseconds).
            cronTimer.Interval = 7000;


            cronTimer.AutoReset = true;
            cronTimer.Enabled = true;

        }

        private void scanProcess(object sender, ElapsedEventArgs e)
        {
            scanFile.processFile();
        }
    }
}
