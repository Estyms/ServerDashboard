using NHttp;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace ServerDashboard
{
    class Server
    {

        HttpServer httpServer = null;
        public Server(ServerInfos sInfos)
        {
            // Creating the request Parser
            RequestParser rp = new RequestParser(sInfos);

            // Creating Server
            httpServer = new HttpServer();

            // Implemeting the Request Reveived event
            httpServer.RequestReceived += (s, rEvent) =>
            {
                // Getting the path of the URL request
                String path = rEvent.Context.Request.Path.ToString();
                
                // Spliting on each /
                Char[] splitList = { '/' };
                String[] pathList = path.Split(splitList);

                // Remove any empty string
                pathList = pathList.Where(val => val != "").ToArray();
                
                // Get path length
                int length = pathList.Length;
                rEvent.Response.Headers.Set("Content-Type", "text/html");
                // Writing to the Web browser
                using (var writer = new StreamWriter(rEvent.Response.OutputStream))
                {

                    // Veryfying if the path is valid
                    if (length != 1) writer.Write("Invalid Path Provided");
                    else
                    {
                        // Executing the request
                        rp.Parse(writer, pathList[0]);
                    }
                }
            };
        }
        public void Start()
        {
            // Setting the Endpoint of the HTTP server
            httpServer.EndPoint = new IPEndPoint(IPAddress.Loopback, 8888);

            // Starting the Server
            httpServer.Start();
        }

        public void Stop()
        {
            // Stop the server
            httpServer.Dispose();
            httpServer.Stop();
        }
    }
}