// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var options = new DbContextOptionsBuilder<AdventureWorksLT2022Context>()
    .UseSqlServer("Data Source=192.168.120.112;Initial Catalog=AdventureWorksLT2022;User ID=sa;Password=P@ssw0rd;TrustServerCertificate=true;")
    //.LogTo(Console.WriteLine)
    //.LogTo(Console.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuted })
    .LogTo(Console.WriteLine, LogLevel.Debug, DbContextLoggerOptions.SingleLine)
    .EnableSensitiveDataLogging()
    ;

var db = new AdventureWorksLT2022Context(options.Options);

Console.WriteLine(db.Customer.TagWith("Mr").Count(x => x.Title == "Mr."));
Console.WriteLine(db.Customer.TagWith("Ms").Count(x => x.Title == "Ms."));
Console.WriteLine(db.Customer.TagWith("Total").Count());
Console.WriteLine(db.Customer.IgnoreQueryFilters().TagWith("Ignore Total").Count());

// 將Product資料表的資料轉換為JSON格式
Console.WriteLine(JsonSerializer.Serialize(db.Product.First()));