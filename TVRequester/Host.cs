using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TVRequester
{
    public class Host
    {
        private string hostName;

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string friendlyName;

        public string FriendlyName
        {
            get { return friendlyName; }
            set { friendlyName = value; }
        }
        private bool isOnline = false;

        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
        }
        public string Url
        {
            get
            {
                return "http://" +
               hostName +
               ":" + port + "/";
            }
        }
        public static List<Host> GetHostsFromFile(string path)
        {
            List<Host> hosts = new List<Host>();
            foreach (string line in File.ReadAllLines(path)  )
            {
                string[] hostLine = line.Split(',');
                Host host = new Host();
                host.HostName = hostLine[0];
                host.Port = Int32.Parse(hostLine[1]);
                host.FriendlyName = hostLine[2];
                hosts.Add(host);
            }
            return hosts;
        }
    }
}
