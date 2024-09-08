using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BundleTree.Enums;

namespace BundleTree.Models
{
    public class BundlePart
    {
        public Guid BundlePartId { get; set; } // PK
        public Guid BundleId { get; set; } // FK
        public Bundle Bundle { get; set; }
        public Guid? ProductId { get; set; }  // FK
        public Product? Product { get; set; }
        public Guid? SubBundleId { get; set; }  // FK
        public Bundle? SubBundle { get; set; }
        public int RequiredUnits { get; set; }
        public PartType Type { get; set; }

        public BundlePart(Guid bundleId, Guid? productId, Guid? subBundleId, int requiredUnits, PartType type)
        {
            BundleId = bundleId;
            ProductId = productId;
            SubBundleId = subBundleId;
            RequiredUnits = requiredUnits;
            Type = type;
        }

        public BundlePart() { }
    }
}
