using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TVRequest
{
    class TeamViewerWin : ITeamViewer
    {
        private static string path = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
        private static string friendly = "TeamViewer";

        static public void GetStatus()
        {
            
        }

        public bool IsRunning()
        {
            return (Process.GetProcessesByName(friendly).Length > 0);
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            Process.Start(path);
        }

        public void Stop()
        {
            Process[] processes = Process.GetProcessesByName(friendly);
            foreach (Process proc in processes)
            {
                proc.Kill(true);
            }
        }
    }
}
