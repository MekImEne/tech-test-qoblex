namespace BundleTree.Models
{
    public class Bundle
    {
        public Guid BundleId { get; set; } // Primary key
        public string BundleName { get; set; } = string.Empty; // Name of Bundle
        public ICollection<BundlePart> BundleParts { get; set; } = new List<BundlePart>(); // One or many BundlePart
    }
}
