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

            System.Timers.Timer aTimer = new System.Timers.Timer();

            // hook up the Elapsed event for the timer.
            aTimer.Elapsed += scanProcess;

            // set the Interval to 20 seconds (20000 milliseconds).
            aTimer.Interval = 20000;

            // have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

        }

        private void scanProcess(object sender, ElapsedEventArgs e)
        {
            scanFile.processFile();
        }
    }
}
