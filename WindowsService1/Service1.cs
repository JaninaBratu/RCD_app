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
#if DEBUG

            scanFile.processFile();
#else
            System.Timers.Timer cronTimer = new System.Timers.Timer();

            // cron behaviour
            cronTimer.Elapsed += scanProcess;

            // set the Interval to 10 seconds (10000 milliseconds).
            cronTimer.Interval = 10000;


            cronTimer.AutoReset = true;
            cronTimer.Enabled = true;
#endif
        }

        private void scanProcess(object sender, ElapsedEventArgs e)
        {
            scanFile.processFile();
        }
    }
}
