using System.Configuration;

namespace NDC2013.TCOD.Service
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
