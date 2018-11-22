using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ProjectWebApp.Controllers.Servicio
{
    public class UtilClientAddress
    {
        private string IPAddress;

        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
    }
}