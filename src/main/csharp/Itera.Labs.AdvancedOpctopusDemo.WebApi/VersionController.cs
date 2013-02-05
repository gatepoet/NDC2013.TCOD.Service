using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Itera.Labs.AdvancedOpctopusDemo.WebApi
{
    public class VersionController : ApiController
    {
        public string Get()
        {
            var assembly = Assembly.GetAssembly(typeof(Program));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.ProductVersion;
        }
    }
}
