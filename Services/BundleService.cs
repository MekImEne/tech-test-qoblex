using System.Collections.Generic;
using System.Linq;

using BundleTree.Data;
using BundleTree.Enums;
using BundleTree.Models;

namespace BundleTree.Services
{
    public class BundleService
    {
        private readonly BundleContext _context;

        public BundleService(BundleContext context)
        {
            _context = context;
        }

        public int ConstructBundles(Bundle bundle, Dictionary<string, int> stock)
        {
            Console.WriteLine($"Constructing bundle: {bundle.BundleName}");
            return Construct(bundle, stock);
        }

        private int Construct(Bundle bundle, Dictionary<string, int> stock)
        {
            // Get all the parts for this bundle
            var bundleParts = _context.BundleParts
                                      .Where(bp => bp.BundleId == bundle.BundleId)
                                      .ToList();

            if (!bundleParts.Any())
            {
                Console.WriteLine($"Bundle {bundle.BundleName} has no parts.");
                return 0;
            }

            int maxBundles = int.MaxValue;

            foreach (var part in bundleParts)
            {
                int availableUnits = 0;

                if (part.Type == PartType.Product)
                {
                    var product = _context.Products.FirstOrDefault(p => p.ProductId == part.ProductId);
                    if (product != null && stock.ContainsKey(product.ProductName))
                    {
                        availableUnits = stock[product.ProductName] / part.RequiredUnits;
                        Console.WriteLine($"Product {product.ProductName}: Stock = {stock[product.ProductName]}, Required = {part.RequiredUnits}, Can make = {availableUnits}");
                    }
                    else
                    {
                        Console.WriteLine($"Product {product?.ProductName ?? "Unknown"} is not in stock.");
                    }
                }
                else if (part.Type == PartType.Bundle)
                {
                    var subBundle = _context.Bundles.FirstOrDefault(b => b.BundleId == part.SubBundleId);
                    if (subBundle != null)
                    {
                        availableUnits = Construct(subBundle, stock) / part.RequiredUnits;
                        Console.WriteLine($"Sub-bundle {subBundle.BundleName}: Can make = {availableUnits}");
                    }
                }

                maxBundles = System.Math.Min(maxBundles, availableUnits);
            }

            Console.WriteLine($"Maximum number of {bundle.BundleName} that can be constructed: {maxBundles}");
            return maxBundles;
        }
    }
}
