using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using NUnit.Framework;

namespace Itera.Labs.AdvancedOpctopusDemo.WebApi.Tests
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void test()
        {
            var service = new HttpApiService();

            service.Start();


            service.Stop();
        }
    }
}

 
namespace SelfHost 
{ 
    public class HelloController : ApiController 
    { 
        public string Get() 
        { 
            return "Hello, world!"; 
        } 
    } 
    [TestFixture]
    class Program 
    { 
        static readonly Uri _baseAddress = new Uri("http://localhost:50231/"); 
        static readonly Uri _address = new Uri(_baseAddress, "/api/hello"); 
 
        [Test]
        static void Main(string[] args) 
        { 
            HttpSelfHostServer server = null; 
            try 
            { 
                // Set up server configuration 
                HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress); 
 
                config.Routes.MapHttpRoute( 
                    name: "DefaultApi", 
                    routeTemplate: "api/{controller}/{id}", 
                    defaults: new { id = RouteParameter.Optional } 
                ); 
 
                // Create server 
                server = new HttpSelfHostServer(config); 
 
                // Start listening 
                server.OpenAsync().Wait(); 
                Console.WriteLine("Listening on " + _baseAddress); 
 
                // Call the web API and display the result 
                HttpClient client = new HttpClient(); 
                client.GetStringAsync(_address).ContinueWith( 
                    getTask => 
                    { 
                        if (getTask.IsCanceled) 
                        { 
                            Console.WriteLine("Request was canceled"); 
                        } 
                        else if (getTask.IsFaulted) 
                        { 
                            Console.WriteLine("Request failed: {0}", getTask.Exception); 
                        } 
                        else 
                        { 
                            Console.WriteLine("Client received: {0}", getTask.Result); 
                        } 
                    }); 
                Console.ReadLine(); 
            } 
            catch (Exception e) 
            { 
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message); 
                Console.WriteLine("Hit ENTER to exit..."); 
                Console.ReadLine(); 
            } 
            finally 
            { 
                if (server != null) 
                { 
                    // Stop listening 
                    server.CloseAsync().Wait(); 
                } 
            } 
        } 
    } 
} 