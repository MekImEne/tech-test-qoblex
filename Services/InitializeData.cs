using System;
using System.Collections.Generic;
using System.Linq;

using BundleTree.Models;
using BundleTree.Enums;
using BundleTree.Data;


namespace BundleTree.Services
{
    public static class DataInitializer
    {
        public static void InitializeBikeTest(BundleContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Generate GUIDs for products
            var productIds = new List<Guid>
            {
                Guid.NewGuid(), // Seat
                Guid.NewGuid(), // Pedal
                Guid.NewGuid(), // Frame
                Guid.NewGuid()  // Tube
            };

            var products = new List<Product>
            {
                new Product { ProductId = productIds[0], ProductName = "seat", Stock = 50 },
                new Product { ProductId = productIds[1], ProductName = "pedal", Stock = 60 },
                new Product { ProductId = productIds[2], ProductName = "frame", Stock = 60 },
                new Product { ProductId = productIds[3], ProductName = "tube", Stock = 35 },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Generate GUIDs for bundles
            var bundleIds = new List<Guid>
            {
                Guid.NewGuid(), // Bike
                Guid.NewGuid()  // Wheels
            };

            var bundles = new List<Bundle>
            {
                new Bundle { BundleId = bundleIds[0], BundleName = "bike" },   // Parent bundle
                new Bundle { BundleId = bundleIds[1], BundleName = "wheels" }  // Sub-bundle
            };
            context.Bundles.AddRange(bundles);
            context.SaveChanges();

            // Define bundle parts with explicit GUIDs
            var bundleParts = new List<BundlePart>
            {
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], SubBundleId = bundleIds[1], RequiredUnits = 2, Type = PartType.Bundle },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], ProductId = productIds[0], RequiredUnits = 1, Type = PartType.Product },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], ProductId = productIds[1], RequiredUnits = 2, Type = PartType.Product },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[1], ProductId = productIds[3], RequiredUnits = 1, Type = PartType.Product },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[1], ProductId = productIds[2], RequiredUnits = 1, Type = PartType.Product }
            };
            context.BundleParts.AddRange(bundleParts);
            context.SaveChanges();
        }

        public static void InitializeComplexTest(BundleContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Generate GUIDs for products
            var productIds = new List<Guid>
            {
                Guid.NewGuid(), // P0
                Guid.NewGuid(), // P1
                Guid.NewGuid(), // P2
                Guid.NewGuid(), // P1a
                Guid.NewGuid(), // P1b
                Guid.NewGuid(), // P1a1
                Guid.NewGuid()  // P1a2
            };

            var products = new List<Product>
            {
                new Product { ProductId = productIds[0], ProductName = "P0" },
                new Product { ProductId = productIds[1], ProductName = "P1" },
                new Product { ProductId = productIds[2], ProductName = "P2" },
                new Product { ProductId = productIds[3], ProductName = "P1a" },
                new Product { ProductId = productIds[4], ProductName = "P1b" },
                new Product { ProductId = productIds[5], ProductName = "P1a1" },
                new Product { ProductId = productIds[6], ProductName = "P1a2" },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Generate GUIDs for bundles
            var bundleIds = new List<Guid>
            {
                Guid.NewGuid(), // P0
                Guid.NewGuid(), // P1
                Guid.NewGuid()  // P1a
            };

            var bundles = new List<Bundle>
            {
                new Bundle { BundleId = bundleIds[0], BundleName = "P0" },
                new Bundle { BundleId = bundleIds[1], BundleName = "P1" },
                new Bundle { BundleId = bundleIds[2], BundleName = "P1a" }
            };
            context.Bundles.AddRange(bundles);
            context.SaveChanges();

            // Define bundle parts with explicit GUIDs
            var bundleParts = new List<BundlePart>
            {
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], SubBundleId = bundleIds[1], RequiredUnits = 2, Type = PartType.Bundle },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], SubBundleId = bundleIds[2], RequiredUnits = 1, Type = PartType.Bundle },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[0], SubBundleId = bundleIds[2], RequiredUnits = 1, Type = PartType.Bundle },

                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[1], SubBundleId = bundleIds[2], RequiredUnits = 2, Type = PartType.Bundle },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[1], ProductId = productIds[4], RequiredUnits = 3, Type = PartType.Product },

                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[2], ProductId = productIds[5], RequiredUnits = 1, Type = PartType.Product },
                new BundlePart { BundlePartId = Guid.NewGuid(), BundleId = bundleIds[2], ProductId = productIds[6], RequiredUnits = 1, Type = PartType.Product }
            };
            context.BundleParts.AddRange(bundleParts);
            context.SaveChanges();
        }

        public static void RunScenario(BundleContext context, string bundleName, Dictionary<string, int> stock)
        {
            var bundleService = new BundleService(context);

            var bundle = context.Bundles.First(b => b.BundleName == bundleName);
            var numberOfBundles = bundleService.ConstructBundles(bundle, stock);
        }
    }
}
