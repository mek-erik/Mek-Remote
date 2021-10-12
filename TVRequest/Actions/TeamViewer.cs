using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;

namespace TVRequest.Actions
{
    class TeamViewer
    {
        public TeamViewer()
        {
            switch (OS.GetOS())
            {
                case "mac":
                    //command = "open /Applications/TeamViewer.app";
                    startInfo.FileName = "open";
                    startInfo.Arguments = "/Applications/TeamViewer.app";
                    break;
                case "windows":
                    //command = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
                    startInfo.FileName = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
                    
                    break;
                default:
                    break;
            }
        }
        private string command;
        private string friendly = "TeamViewer";
        ProcessStartInfo startInfo = new ProcessStartInfo();
        private Process teamViewer = new Process();
        
        
        public void Start()
        {
            try
            {
                //Process proc = Process.Start(command);
                Process proc = Process.Start(startInfo);
                if (OS.GetOS() == "windows")
                {

                    Process.Start(startInfo);
                }


            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public bool GetStatus()
        {
            return Process.GetProcessesByName(friendly).Length > 0;
        }
        public void Stop()
        {
            Process[] procs = Process.GetProcessesByName(friendly);
            foreach (Process proc in procs)
            {
                proc.Kill();
            }
        }
    }
}
