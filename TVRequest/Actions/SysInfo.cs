using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
namespace TVRequest.Actions
{
    class SysInfo
    {

        public List<string> IPAddresses()
        {
                List<string> ips =  new List<IPAddress>(Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                  .Select(ip => ip.ToString()).ToList();
            return ips;
        }



        private string hostname;

        public string Hostname
        {
            get { return Dns.GetHostName(); }
            //set { hostname = value; }
        }


    }
}
