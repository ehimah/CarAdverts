using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;
using CarAdverts;
using System.Net.Http;

namespace CarAdverts.IntegrationTest
{
    public class TestClientProvider:IDisposable
    {

        public HttpClient Client { get; set; }
        private TestServer server;
        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
