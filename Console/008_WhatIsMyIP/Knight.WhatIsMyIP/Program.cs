using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Knight.WhatIsMyIP
{
    internal class Program
    {
        private static void Main(string[] args)
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

            // 發送通知
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var notifyService = new NotifyService(config, httpClientFactory);
            notifyService.Send(sb.ToString()).Wait();

            Console.WriteLine(sb.ToString());
            Console.ReadLine();
        }
    }
}