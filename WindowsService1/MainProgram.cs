using System.ServiceProcess;

namespace Netrom.WindowsService
{
    static class MainProgram
    {

        static void Main()
        {

#if DEBUG
        Service1 myService = new Service1();
        myService.OnDebug();
        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite); //keep the service alive
#else
        ServiceBase[] ServicesToRun;
        ServicesToRun = new ServiceBase[]
        {
            new Service1()
        };      
        ServiceBase.Run(ServicesToRun);
#endif
        }

    }
}
