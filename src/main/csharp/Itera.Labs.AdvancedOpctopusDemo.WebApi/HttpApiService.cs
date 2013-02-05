using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Itera.Labs.AdvancedOpctopusDemo.WebApi
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
            _settings.Load();
            _eventLog = new EventLog();
            _eventLog.Source = this.GetType().Name;
            _eventLog.Log = "Application";
            _eventLog.WriteEntry("Starting webapi server at " + _settings.BaseAddress);
            _config = new HttpSelfHostConfiguration(_settings.BaseAddress);
            _config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
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
