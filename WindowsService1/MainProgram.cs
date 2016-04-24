namespace WindowsService1
{
    static class MainProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {


            Service1 myService = new Service1();
            myService.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite); //keep the service alive

            // ServiceBase[] ServicesToRun;
            //   ServicesToRun = new ServiceBase[]
            //    {
            //       new Service1()
            //   };      
            //   ServiceBase.Run(ServicesToRun);

        }
        
    }
}
