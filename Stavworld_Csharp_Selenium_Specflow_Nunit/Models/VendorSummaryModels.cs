using System.Collections.Generic;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Models
{
    /// <summary>
    /// Vendor Summary API Request Model
    /// </summary>
    public class VendorSummaryRequest
    {
        public bool ActiveFlag { get; set; }
        public string TileFlag { get; set; }
        public List<object> Filters { get; set; } = new List<object>();
    }

    /// <summary>
    /// Vendor Summary API Response Model - API returns array directly
    /// </summary>
    public class VendorSummaryResponse : List<VendorSummaryData>
    {
        // API returns array of VendorSummaryData directly
    }

    /// <summary>
    /// Vendor Summary Data Model
    /// </summary>
    public class VendorSummaryData
    {
        public int TotalVendors { get; set; }
        public int ActiveVendors { get; set; }
        public int InactiveVendors { get; set; }
        public int PendingVendors { get; set; }
        public int ApprovedVendors { get; set; }
        public int RejectedVendors { get; set; }
        public int DraftVendors { get; set; }
        public int OnboardingVendors { get; set; }
        public int OffboardingVendors { get; set; }
        public List<VendorItem> Vendors { get; set; } = new List<VendorItem>();
    }

    /// <summary>
    /// Individual Vendor Item
    /// </summary>
    public class VendorItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
