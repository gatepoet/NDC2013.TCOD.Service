using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace NDC2013.TCOD.Service
{
    public partial class HttpApiService : ServiceBase
    {
        private HttpSelfHostServer _server;
        private readonly HttpSelfHostConfiguration _config;
        private readonly Settings _settings;
        private readonly EventLog _eventLog;

        public HttpApiService()
        {
            InitializeComponent();

            _settings = new Settings();
            Settings.Load();
            _eventLog = new EventLog();
            _eventLog.Source = this.GetType().Name;
            _eventLog.Log = "Application";
            _eventLog.WriteEntry("Starting webapi server at " + Settings.BaseAddress);
            _config = new HttpSelfHostConfiguration(Settings.BaseAddress);
            _config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            
            var appXmlType = _config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            _config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        public Settings Settings
        {
            get { return _settings; }
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        public void Start()
        {
            _server = new HttpSelfHostServer(_config);
            _server.OpenAsync().Wait();
        }

        protected override void OnStop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }
    }
}
