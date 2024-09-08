using BundleTree.Data;
using BundleTree.Enums;
using BundleTree.Models;
using BundleTree.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BundleContext>();
        optionsBuilder.UseSqlite("Data Source=bundles.db");

        using (var context = new BundleContext(optionsBuilder.Options))
        {

            // Ensure the database is created and up to date
            context.Database.EnsureCreated();


            // Scenario 1: Initialize bike scenario
            DataInitializer.InitializeBikeTest(context);

            // Stock for bike scenario
            var stockForBike = new Dictionary<string, int>
            {
                { "seat", 50 },
                { "pedal", 60 },
                { "frame", 60 },
                { "tube", 35 },
            };
            DataInitializer.RunScenario(context, "bike", stockForBike);


            Console.WriteLine($"***************************************************************");


            // Scenario 2: Initialize a different scenario
            DataInitializer.InitializeComplexTest(context);

            // Stock for different scenario
            var stockForComplexScenario = new Dictionary<string, int>
            {
                { "P0", 20 },
                { "P1", 50 },
                { "P2", 48 },
                { "P3", 20 },
                { "P1a", 60 },
                { "P1b", 30 },
                { "P1a1", 20 },
                { "P1a2", 20 },
            };
            DataInitializer.RunScenario(context, "P0", stockForComplexScenario);

        }
    }
}
