using System;
using System.Net;

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

            // 取得所有 IP 位址
            int num = 1;
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                Console.WriteLine("IP #" + num + ": " + ipaddress.ToString());
                num = num + 1;
            }

            Console.ReadLine();
        }
    }
}
