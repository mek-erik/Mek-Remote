using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TVRequest;
namespace TVRequest.RequestHandler
{
    class ScreenshotRequestHandler
    {
        private Actions.Screenshot screenshot = new Actions.Screenshot();
        public void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            string[] urlParts = request.Url.AbsolutePath.TrimStart('/').Split('/');
            if (request.HttpMethod == "GET")
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                switch (urlParts[1])
                {
                    case "save":
                        string[] keys = request.QueryString.AllKeys;
                        
                        string[] values = request.QueryString.GetValues("destination");
                        string destination = request.QueryString.Get("destination");
                        Console.WriteLine("Start Screenshot request");

                        string filename =   DateTime.Now.ToString("MMddyyHHmmss") + ".png";
                        
                        //Server.WriteResp(response, "Screenshot captured");
                        try
                        {
                            Console.WriteLine(folder + filename);
                            screenshot.SaveScreenshot(folder + filename);
                            Result result = new Result() { StatusCode = 200, Description = filename };
                            Server.WriteJSON(response, result);
                        }
                        catch (Exception e)
                        {
                            
                            Console.WriteLine("Error while taking screenshot: " + e.GetType().ToString());
                            Console.WriteLine(e.Message);
                            //return http error response
                            Result result = new Result() { StatusCode = 500, Description = e.Message, Type=e.GetType().ToString() };
                            Server.WriteJSON(response, result);

                        }
                        break;
                    case "download":
                        try
                        {
                            filename = urlParts[2];
                            using (var fileStream = File.OpenRead(folder + filename))
                            {
                                response.ContentType = "application/octet-stream";
                                response.ContentLength64 = (new FileInfo(folder + filename)).Length;
                                response.AddHeader(
                                    "Content-Disposition",
                                    "Attachment; filename=\"" + Path.GetFileName(filename) + "\"");
                                fileStream.CopyTo(response.OutputStream);
                            }

                            response.OutputStream.Close();
                            response = null;
                        }
                        catch (Exception e)
                        {

                            Result result = new Result() { StatusCode = 500, Description = e.Message, Type = e.GetType().ToString() };
                            Server.WriteJSON(response, result);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
