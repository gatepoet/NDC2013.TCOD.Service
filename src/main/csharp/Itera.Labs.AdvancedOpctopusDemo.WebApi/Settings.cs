using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itera.Labs.AdvancedOpctopusDemo.WebApi
{
    public class Settings
    {
        public void Load(string configPath = null)
        {
            if (configPath == null)
            {

                _hostname = ConfigurationManager.AppSettings["HostName"];
                _port = ConfigurationManager.AppSettings["Port"];
            }
        }

        private string _hostname;
        public string Hostname { get { return _hostname; } }
    
        public string _port;
        public string Port { get { return _port; } }

        public string BaseAddress { get { return string.Format("http://{0}:{1}/", Hostname, Port); } }
    }
}
