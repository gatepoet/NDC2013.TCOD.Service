using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Itera.NDC2013.TCOD.Service
{
    public class VersionController : ApiController
    {
        public object Get()
        {
            var assembly = Assembly.GetAssembly(typeof(Program));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var environment = ConfigurationManager.AppSettings["Environment"];
            return new
                    {
                        Environment = environment,
                        Version = versionInfo.ProductVersion
                    };
        }
    }
}
