using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Reflection;
using System.Linq;

namespace TVRequest.RequestHandler
{
    public class TeamViewerRequestHandler
    {
        public TeamViewerRequestHandler()
        {
            //if (request.HttpMethod == "GET")
            //{

            //}
            //Handle here
            
        }
        private Actions.TeamViewer teamViewer = new Actions.TeamViewer();
        public void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            string[] urlParts = request.Url.AbsolutePath.TrimStart('/').Split('/');
            if (request.HttpMethod == "GET")
            {
                Result result;
                switch (urlParts[1])
                {
                    case "start":
                        Console.WriteLine("Start TeamViewer request");
                        try
                        {
                            teamViewer.Start();
                            Server.WriteJSON(response, new Result() { Description = "Teamviewer Started succesfully" });
                        }
                        catch (Exception e)
                        {
                            result = new Result() { StatusCode = 500, Description = e.Message, Type = e.GetType().ToString()};
                            Server.WriteJSON(response, result);
                        }
                        
                        
                        break;
                    case "stop":
                        Console.WriteLine("Stop TeamViewer request");
                        teamViewer.Stop();
                        Server.WriteJSON(response, new Result() { Description = "Teamviewer Quit succesfully" });
                        break;
                    case "status":
                        Console.WriteLine("Status TeamViewer request");
                        bool isRunning = teamViewer.GetStatus();
                        string output = "Teamviewer Status: ";
                        if (isRunning)
                        {
                            output += "Running";
                        }
                        else
                        {
                            output += "Not running";
                        }
                        result = new Result() { StatusCode = 200, Description = output };
                        Server.WriteJSON(response, result);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
