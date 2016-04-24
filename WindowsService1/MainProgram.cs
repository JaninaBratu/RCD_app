using System.ServiceProcess;

namespace WindowsService1
{
    static class MainProgram
    {

        static void Main()
        {


           Service1 myService = new Service1();
            myService.onDebug();
           System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite); //keep the service alive

          //   ServiceBase[] ServicesToRun;
            //   ServicesToRun = new ServiceBase[]
              //  {
                //   new Service1()
               //};      
               //ServiceBase.Run(ServicesToRun);

        }
        
    }
}
