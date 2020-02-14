using System.Collections.Generic;

namespace Coin.Web.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public long VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
        public ICollection<ProductCategory> Categories { get; set; }
        
        // public ProductType Type { get; set; }
        // public bool VisibleIndividually { get; set; }
        // public bool SubjectToAcl { get; set; }
        // public long AclId { get; set; }
        // public Acl Acl { get; set; }        
        // public bool DisplayStockAvailability { get; set; }
        // public bool DisplayStockQuantity { get; set; }
        // public int MinStockQuantity { get; set; }
        // public bool LowStock { get; set; }
        // public decimal Weight { get; set; }
        // public decimal Length { get; set; }
        // public decimal Width { get; set; }
        // public decimal Height { get; set; }
        // public bool IsDownload { get; set; }
        // public string DownloadId { get; set; }
        // public Download Download { get; set; }
        // public bool UnlimitedDownloads { get; set; }
        // public int MaxNumberOfDownloads { get; set; }
        // public int? NumberOfTimesDownloaded { get; set; }
        // public int? DownloadExpirationDays { get; set; }
        // public bool HasUserAgreement { get; set; }
        // public string UserAgreement { get; set; }
        // public ICollection<ProductAttributeValue> AttributeValues { get; set; }
    }
}