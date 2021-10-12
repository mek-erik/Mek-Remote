using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TVRequest.Actions
{
    class System
    {
        private string shutdownCommand = "shutdown";
        private string restartCommand = "shutdown /r /f /t 10";
        private Process shutdown = new Process();
        private Process restart = new Process();
        public System()
        {
            switch (OS.GetOS())
            {
                case "mac":
                    shutdown.StartInfo.FileName = "shutdown";
                    shutdown.StartInfo.Arguments = "-h +10";
                    restart.StartInfo.FileName = "shutdown";
                    restart.StartInfo.Arguments = "-r +10";
                    break;
                case "windows":
                    shutdown.StartInfo.FileName = "shutdown";
                    shutdown.StartInfo.Arguments = "/s /f /t 10";
                    restart.StartInfo.FileName = "shutdown";
                    restart.StartInfo.Arguments = "/r /f /t 10";
                    break;
                default:
                    break;
            }
        }
        public void ShutDown()
        {
            shutdown.Start();

        }
        public void Restart()
        {
            restart.Start();
        }
    }
}
