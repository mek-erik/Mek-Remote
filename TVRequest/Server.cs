using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TVRequest
{
    class Server
    {
        
        public HttpListener httpListener;
        private int port = 8000;
        private string os;
        public Server(int port, string os)
        {
            this.port = port;
            this.os = os;
        }
        
        public void Start()
        {
            // Create a Http server and start listening for incoming connections
            httpListener = new HttpListener();
            //httpListener.Prefixes.Add("http://localhost" + port +"/");
            httpListener.Prefixes.Add("http://*:" + port + "/");
            try
            {
                httpListener.Start();
                Console.WriteLine("Server started listening on port {0}", port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Server did not start because of an error:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            httpListener.Close();

        }
        public async Task HandleIncomingConnections()
        {
            bool runServer = true;

            RequestHandler.TeamViewerRequestHandler teamViewerRequestHandler = new RequestHandler.TeamViewerRequestHandler();
            RequestHandler.ScreenshotRequestHandler screenshotRequestHandler = new RequestHandler.ScreenshotRequestHandler();
            RequestHandler.SystemRequestHandler systemRequestHandler = new RequestHandler.SystemRequestHandler();
            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await httpListener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest request = ctx.Request;
                HttpListenerResponse response = ctx.Response;


                //Request handler here
                if (request.Url.AbsolutePath.StartsWith("/tv"))
                {
                    Console.WriteLine("Handle By TVRH");
                    teamViewerRequestHandler.Handle(request, response);
                }
                else if (request.Url.AbsolutePath.StartsWith("/screenshot"))
                {
                    Console.WriteLine("Handle By Screenshot Handler");
                    screenshotRequestHandler.Handle(request, response);
                }
                else if (request.Url.AbsolutePath.StartsWith("/system"))
                {
                    Console.WriteLine("Handle By System Handler");
                    systemRequestHandler.Handle(request, response);

                }
                else
                {
                    //Result result = new Result() { Description = }
                    string json = Result.GetJSON(new Result() { Description = "ALL OK" });
                    WriteJSON(response, json);
                    //WriteResp(response, "200 OK NIGGA");
                }


            }
        }

        public void Stop()
        {

        }
        public static async void WriteResp(HttpListenerResponse resp, string text, int statusCode = 200)
        {
            byte[] data = Encoding.UTF8.GetBytes(String.Format(text));
            resp.StatusCode = statusCode;
            resp.ContentType = "text/html";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
            //return resp;
        }
        public static async void WriteJSON(HttpListenerResponse resp, string text, int statusCode = 200)
        {
            
            byte[] data = Encoding.UTF8.GetBytes(text);
            resp.StatusCode = statusCode;
            resp.ContentType = "application/json";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
            //return resp;
        }
         public static async void WriteJSON(HttpListenerResponse resp, Result result)
        {
            
            byte[] data = Encoding.UTF8.GetBytes(Result.GetJSON(result));
            resp.StatusCode = result.StatusCode;
            resp.ContentType = "application/json";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
            //return resp;
        }
         public static async void WriteJSON(HttpListenerResponse resp, Actions.SysInfo result)
        {
            
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
            
            resp.ContentType = "application/json";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
            //return resp;
        }
        public static void Redirect(HttpListenerRequest request, HttpListenerResponse response)
        {
            response.Redirect("/bin/filename.jpg");
            response.Close();
        }

    }
}
