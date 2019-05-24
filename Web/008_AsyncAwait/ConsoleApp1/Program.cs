using System;
using System.Net.Http;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<Md5VsSha256>();

            var summary = BenchmarkRunner.Run<Program>();

            Console.ReadLine();
        }

        [Benchmark]
        public void SendRequest()
        {
            var url = "http://localhost:5000/api/values/getdata";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = new HttpClient();

            var response = httpClient.SendAsync(httpRequestMessage);
        }
    }

    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }
}