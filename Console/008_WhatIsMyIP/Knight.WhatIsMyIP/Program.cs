using System;
using System.Net;
using System.Text;

namespace Knight.WhatIsMyIP
{
    class Program
    {
        static void Main(string[] args)
        {
            // 取得本機名稱
            String strHostName = Dns.GetHostName();

            // 取得本機的 IpHostEntry 類別實體
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

            StringBuilder sb = new StringBuilder();

            // 取得所有 IP 位址
            int num = 1;
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                var strIP = ipaddress.ToString();
                //// 排除IPv6
                if (strIP.Contains(":")) continue;
                sb.AppendLine("IP #" + num + ": " + strIP);
                num = num + 1;
            }

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}
