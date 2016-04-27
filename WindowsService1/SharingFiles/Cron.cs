using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Netrom.WindowsService.SharingFiles
{
    /// <summary>
    /// This class is responsible for initialize and start the cron
    /// </summary>
    public class Cron
    {
        const int DEFAULT_INTERVAL = 10000; //the default interval is 10 seconds (10000 milliseconds)

#if !DEBUG
        private Timer _cron; //a Timer object, used for trigger the scan event depending on the specified range
#endif

        public Cron(int interval = DEFAULT_INTERVAL)
        {
#if DEBUG

            StartSharing();
#else
            _cron = new Timer();

            _cron.Elapsed += StartScaningEventArgs;//initializing the event which must be triggered

            _cron.Interval = interval;

            _cron.AutoReset = true;
            _cron.Enabled = true;
#endif
        }

        private void StartScaningEventArgs(object sender, ElapsedEventArgs e)
        {
            StartSharing();

        }

        /// <summary>
        /// Start Sharing Process
        /// </summary>
        private void StartSharing()
        {
            Sharing shareObject = new Sharing();
            shareObject.StartSharing();
        }
    }
}
