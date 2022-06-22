using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
namespace XamForm.Class
{
    public class NetWork
    {
        static IPAddress[]  address = Dns.GetHostAddresses(Dns.GetHostName());
        public static string GetIp()
        {
            string IP = string.Empty;

            if (address != null && address[0] != null)
            {
                IP = address[0].ToString();
            }
            return IP;
        }
    }
}
