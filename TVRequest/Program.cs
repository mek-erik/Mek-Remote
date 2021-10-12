using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
namespace TVRequest
{
    class Program
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static string networkUrl = "http://*:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "200 OK";
        public static string teamviewerExe = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
        private static string os = "windows";
        

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(new string('*', Console.WindowWidth));
            Console.WriteLine("TeamViewer Request Server");
            Console.WriteLine("By Erik Van Reusel");
            Console.WriteLine(new string('*', Console.WindowWidth));

            Console.WriteLine();
            Console.WriteLine("System OS is: {0}",Actions.OS.GetOS().ToUpper());
            Console.WriteLine();
            Actions.SysInfo sysInfo = new Actions.SysInfo();

            Console.WriteLine("Hostname:");
            Console.WriteLine(sysInfo.Hostname);
            Console.WriteLine("IP adresses (local):");
            sysInfo.IPAddresses().ForEach(ip => Console.WriteLine(ip));


            Server server = new Server(8000, os);
            server.Start();
            //// Create a Http server and start listening for incoming connections
            //listener = new HttpListener();
            //listener.Prefixes.Add(url);
            //listener.Prefixes.Add(networkUrl);
            //listener.Start();
            //Console.WriteLine("Listening for connections on {0}", url);

            //// Handle requests
            //Task listenTask = HandleIncomingConnections();
            //listenTask.GetAwaiter().GetResult();

            //// Close the listener
            //listener.Close();
        }



        //public static async Task HandleIncomingConnections()
        //{
        //    bool runServer = true;
           

        //    // While a user hasn't visited the `shutdown` url, keep on handling requests
        //    while (runServer)
        //    {
        //        // Will wait here until we hear from a connection
        //        HttpListenerContext ctx = await listener.GetContextAsync();

        //        // Peel out the requests and response objects
        //        HttpListenerRequest req = ctx.Request;
        //        HttpListenerResponse resp = ctx.Response;

        //        // Print out some info about the request
        //        Console.WriteLine("Request #: {0}", ++requestCount);
        //        Console.WriteLine(req.Url.ToString());
        //        Console.WriteLine(req.HttpMethod);
        //        Console.WriteLine(req.UserHostName);
        //        Console.WriteLine(req.UserAgent);
        //        Console.WriteLine();

        //        // requestHandler.HandleRequests(req, resp);


        //        // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
        //        // Make sure we don't increment the page views counter if `favicon.ico` is requested
        //        #region Old code
        //        //if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/opentv"))
        //        //{
        //        //    Process proc = Process.Start(teamviewerExe);
                    

        //        //    Console.WriteLine("Teamviewer Startup Requested");
        //        //    WriteResp(resp, "Teamviewer started.");
        //        //    Console.WriteLine("----" + proc.MainWindowHandle);

        //        //}
        //        //else if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/status"))
        //        //{
        //        //    Process[] tv = Process.GetProcessesByName("TeamViewer");
        //        //    WriteResp(resp, tv.Length.ToString());

        //        //}
        //        // else if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/screenshot"))
        //        //{
        //        //    TakeScreenShot();
        //        //    Console.WriteLine("Screenshot Requested");
        //        //    WriteResp(resp, "Screenshot captured.");
        //        //}
                
        //        //else if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/shutdown"))
        //        //{
        //        //    Console.WriteLine("Shutdown requested");
        //        //    //runServer = false;
        //        //    Process.Start("firefox https:google.com/");
        //        //    resp.Close();
        //        //}

        //        //else if ((req.HttpMethod == "GET"))
        //        //{
        //        //    // Write the response info
        //        //    byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData));
        //        //    resp.ContentType = "text/html";
        //        //    resp.ContentEncoding = Encoding.UTF8;
        //        //    resp.ContentLength64 = data.LongLength;

        //        //    // Write out to the response stream (asynchronously), then close it
        //        //    await resp.OutputStream.WriteAsync(data, 0, data.Length);
        //        //    resp.Close();
        //        //}
        //        #endregion Oldcode
        //    }
        //}
        

    }
}
