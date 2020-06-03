using System;
using EFCoreHooksConsole.Data;
using EFCoreHooksConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreHooksConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var optionsBuilder = new DbContextOptionsBuilder<EfhookdbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=EFHookDB;User Id=sa;Password=P@ssw0rd;");
            var dbContext = new EfhookdbContext(optionsBuilder.Options);

            var sd = new ShopDefault
            {
                ShopDefault_Key = "key",
                ShopDefault_CreatedDateTime = DateTime.Now,
                ShopDefault_CreatedUser = "knight",
                ShopDefault_GroupTypeDef = "Group",
                ShopDefault_ShopId = 46,
                ShopDefault_UpdatedDateTime = DateTime.Now,
                ShopDefault_UpdatedTimes = 1,
                ShopDefault_UpdatedUser = "k",
                ShopDefault_ValidFlag = true,
                ShopDefault_Value = "Value"
            };
            dbContext.Add(sd);
            dbContext.SaveChanges();
        }
    }
}