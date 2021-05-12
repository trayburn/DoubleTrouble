using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DoubleTrouble
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbcCore = serviceProvider.GetRequiredService<DbContextCore>();
                var dbc6 = serviceProvider.GetRequiredService<DbContext6>();

                Console.WriteLine("Clearing all existing records from the DB using Core");
                foreach (DemoEntity demoEntity in dbcCore.Demo)
                {
                    dbcCore.Remove(demoEntity);
                }
                dbcCore.SaveChanges();

                Console.WriteLine($"Records in Demo according to dbcCore : {dbcCore.Demo.Count()}");
                Console.WriteLine($"Records in Demo according to dbc6 : {dbc6.Demo.Count()}");

                Console.WriteLine("Write 4 entities using Core");
                dbcCore.Add(new DemoEntity { Name = "Tim Rayburn" });
                dbcCore.Add(new DemoEntity { Name = "Devlin Liles" });
                dbcCore.Add(new DemoEntity { Name = "Bill Gates" });
                dbcCore.Add(new DemoEntity { Name = "Mads Torgersen" });
                dbcCore.SaveChanges();

                Console.WriteLine($"Records in Demo according to dbcCore : {dbcCore.Demo.Count()}");
                Console.WriteLine($"Records in Demo according to dbc6 : {dbc6.Demo.Count()}");

                Console.WriteLine("Write 4 entities using EF6");
                dbc6.Demo.Add(new DemoEntity { Name = "Tim Rayburn EF6" });
                dbc6.Demo.Add(new DemoEntity { Name = "Devlin Liles EF6" });
                dbc6.Demo.Add(new DemoEntity { Name = "Bill Gates EF6" });
                dbc6.Demo.Add(new DemoEntity { Name = "Mads Torgersen EF6" });
                dbc6.SaveChanges();

                Console.WriteLine($"Records in Demo according to dbcCore : {dbcCore.Demo.Count()}");
                Console.WriteLine($"Records in Demo according to dbc6 : {dbc6.Demo.Count()}");

                Console.WriteLine("Iterate all entities in Core");
                foreach (DemoEntity demoEntity in dbcCore.Demo)
                {
                    Console.WriteLine($"{demoEntity.Id} - {demoEntity.Name}");
                }

                Console.WriteLine("Iterate all entities in EF6");
                foreach (DemoEntity demoEntity in dbc6.Demo)
                {
                    Console.WriteLine($"{demoEntity.Id} - {demoEntity.Name}");
                }

            }
        }

        private static IServiceProvider CreateServices()
        {
            var connString = "Server=.;Database=tempdb;User Id=sa;Password=ChangeMe1";
            return new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<DbContextCore>(opt =>
                {
                    opt.UseSqlServer(connString);
                })
                .AddTransient<DbContext6>(sp => new DbContext6(connString))
                .BuildServiceProvider(false);
        }

    }
}
