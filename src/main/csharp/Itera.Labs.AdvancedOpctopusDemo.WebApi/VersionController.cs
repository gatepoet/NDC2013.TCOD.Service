using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace NDC2013.TCOD.Service
{
    public class VersionController : ApiController
    {
        public JObject Get()
        {
            var assembly = Assembly.GetAssembly(typeof(Program));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var environment = ConfigurationManager.AppSettings["Environment"];
            return new JObject(
                new
                    {
                        Environment = environment,
                        Version = versionInfo.ProductVersion
                    });
        }
    }
}
