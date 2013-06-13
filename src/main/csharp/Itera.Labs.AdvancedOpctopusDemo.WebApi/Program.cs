using System.ServiceProcess;

namespace NDC2013.TCOD.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new HttpApiService(),  
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
