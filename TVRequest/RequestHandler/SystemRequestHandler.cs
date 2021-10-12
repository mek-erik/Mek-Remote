using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace TVRequest.RequestHandler
{
    class SystemRequestHandler
    {
        private Actions.System system = new Actions.System();
        public void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            string[] urlParts = request.Url.AbsolutePath.TrimStart('/').Split('/');
            if (request.HttpMethod == "GET")
            {
                switch (urlParts[1])
                {
                    case "shutdown":
                        system.ShutDown();
                        Server.WriteJSON(response, new Result() { Description = "System will shut down in 10 seconds." });
                        break;
                    case "restart":
                        system.Restart();
                        Server.WriteJSON(response, new Result() { Description = "System will restarted in 10 seconds." });
                        break;
                    case "info":
                        Server.WriteJSON(response, new Actions.SysInfo());
                        break;

                    default:
                        break;
                }

            }
        }

    }
}
