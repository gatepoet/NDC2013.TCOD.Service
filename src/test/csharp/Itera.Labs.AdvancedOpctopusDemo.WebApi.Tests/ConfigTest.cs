using System;
using System.Net.Http;
using NUnit.Framework;

namespace Itera.NDC2013.TCOD.Service.Tests
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void IntegrationTest()
        {
            var service = new HttpApiService();

            service.Start();

            try
            {
                HttpClient client = new HttpClient();
                client.GetStringAsync(service.Settings.BaseAddress).ContinueWith(
                    getTask =>
                        {
                            if (getTask.IsCanceled)
                            {
                                Assert.Fail("Request was canceled");
                            }
                            else if (getTask.IsFaulted)
                            {
                                Assert.Fail("Request failed: {0}", getTask.Exception);
                            }
                            else
                            {
                                Console.WriteLine("Client received: {0}", getTask.Result);
                            }
                        });
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            }
            finally
            {
                if (service != null)
                {
                    service.Stop();
                }
            }

        }
    }
}