using System;
using System.Collections.Generic;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Models
{
    /// <summary>
    /// Vendor Detail Response Model - matches actual API response structure
    /// </summary>
    public class VendorDetailResponse
    {
        public Company Company { get; set; }
        public Vendor1099 Vendor1099 { get; set; }
        public TaxClassification TaxClassification { get; set; }
        public VendorContact VendorContact { get; set; }
        public VendorBalance VendorBalance { get; set; }
        public Compliance Compliance { get; set; }
        public List<object> Accounts { get; set; }
        public List<object> VendorContracts { get; set; }
        public List<object> VendorExpenseTypeMapping { get; set; }
        public List<object> VendorAllocationSchemeMapping { get; set; }
        public UDFValue UDFValue { get; set; }
        public object UseTax { get; set; }
        public RefPayment RefPayment { get; set; }
        public object InvoiceCcy { get; set; }
        public object PaymentCcy { get; set; }
        public List<object> Documents { get; set; }
        public List<object> VendorBusinessOwnerMapping { get; set; }
        public object LegalVendor { get; set; }
        public object PaymentTerm { get; set; }
        public List<object> AccessGroups { get; set; }
        public object VendorEPayment { get; set; }
        public List<object> AccountApprovals { get; set; }
        
        // Main vendor properties
        public int VendorId { get; set; }
        public int CompanyId { get; set; }
        public string VendorCode { get; set; }
        public string VendorShortName { get; set; }
        public string VendorLongName { get; set; }
        public string VendorNotes { get; set; }
        public string BudgetCode { get; set; }
        public object VendorUpdateRevisionNumber { get; set; }
        public bool IsActive { get; set; }
        public object VendorCategoryId { get; set; }
        public object UseTaxId { get; set; }
        public object UseTaxComment { get; set; }
        public int VendorTypeId { get; set; }
        public object WorkflowEntityMapping { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int Vendor1099Id { get; set; }
        public int TaxClassificationId { get; set; }
        public bool IsForeignEntity { get; set; }
        public int DefaultRefPaymentId { get; set; }
        public object InvoiceCcyId { get; set; }
        public object VendorAllocationNotes { get; set; }
        public object VendorWorkflowNotes { get; set; }
        public object PaymentCcyId { get; set; }
        public object ParentId { get; set; }
        public object FeedRunId { get; set; }
        public int NoteCount { get; set; }
        public object LegalVendorId { get; set; }
        public object PaymentTermId { get; set; }
        public bool IsShowOCRDataLoader { get; set; }
        public object WorkflowSchemeCode { get; set; }
        public object VendorEPaymentId { get; set; }
        public object PaymentNotificationEmail { get; set; }
    }

    /// <summary>
    /// Company information
    /// </summary>
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string CompanyWebsite { get; set; }
        public object FiscalYearStartsWithMonth { get; set; }
        public object IncomeTaxYearStartsWithMonth { get; set; }
        public object EIN { get; set; }
        public object SSN { get; set; }
        public int BusinessNumber { get; set; }
        public int TaxForm { get; set; }
        public object CompanyNotes { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    /// <summary>
    /// Vendor 1099 information
    /// </summary>
    public class Vendor1099
    {
        public Address Address { get; set; }
        public Vendor1099Type Vendor1099Type { get; set; }
        public object Vendor1099FormType { get; set; }
        public int Vendor1099Id { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
        public int AddressId { get; set; }
        public object CompanyName { get; set; }
        public object DBA { get; set; }
        public bool Track1099 { get; set; }
        public object SameAsContactAddress { get; set; }
        public object EINSSN { get; set; }
        public object Tax1099Notes { get; set; }
        public bool IsElectronic1099 { get; set; }
        public object Electronic1099Emails { get; set; }
        public object Electronic1099CCEmails { get; set; }
        public int Vendor1099TypeId { get; set; }
        public object Vendor1099FormTypeId { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrimary1099Recipient { get; set; }
    }

    /// <summary>
    /// Tax Classification information
    /// </summary>
    public class TaxClassification
    {
        public object ParentLookupValue { get; set; }
        public int LookupValueId { get; set; }
        public int CompanyId { get; set; }
        public int LookupTypeId { get; set; }
        public object LookupTypeEnum { get; set; }
        public string LookupValueName { get; set; }
        public object LookupLongName { get; set; }
        public string LookupName { get; set; }
        public object ExternalId { get; set; }
        public bool IsActive { get; set; }
        public object IsForwardAction { get; set; }
        public int Sequence { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public object ParentLookupValueId { get; set; }
    }

    /// <summary>
    /// Vendor Contact information
    /// </summary>
    public class VendorContact
    {
        public Address Address { get; set; }
        public int VendorContactId { get; set; }
        public object CompanyId { get; set; }
        public int VendorId { get; set; }
        public object ContactSalutation { get; set; }
        public object ContactName { get; set; }
        public object ContactFullName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMobile { get; set; }
        public object ContactFax { get; set; }
        public object ContactNote { get; set; }
        public bool IsActive { get; set; }
        public object ContactWebsite { get; set; }
        public string ContactEmail { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int AddressId { get; set; }
    }

    /// <summary>
    /// Vendor Balance information
    /// </summary>
    public class VendorBalance
    {
        public int VendorBalanceId { get; set; }
        public object CompanyId { get; set; }
        public int VendorId { get; set; }
        public double BalanceAsOfDate { get; set; }
        public double DayOpenBalance { get; set; }
        public double DayCloseBalance { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    /// <summary>
    /// Compliance information
    /// </summary>
    public class Compliance
    {
        public List<object> DueDilligenceNotes { get; set; }
        public List<object> VendorComplianceDepartmentMapping { get; set; }
        public List<object> VendorComplianceUserMapping { get; set; }
        public List<object> Documents { get; set; }
        public int ComplianceId { get; set; }
        public int VendorId { get; set; }
        public object CategorySubCategoryId { get; set; }
        public int CompanyId { get; set; }
        public string RiskCategorySelection { get; set; }
        public object ComplianceNotes { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    /// <summary>
    /// UDF Value information
    /// </summary>
    public class UDFValue
    {
        public int Id { get; set; }
        public int UDFValuePrincipalId { get; set; }
        public bool IsActive { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool UDF_123 { get; set; }
        public object UDF_32271 { get; set; }
        public bool UDF_54487yesno { get; set; }
        public object UDF_Blacklisted { get; set; }
        public object UDF_Change { get; set; }
        public object UDF_Changetype { get; set; }
        public string UDF_Classv360 { get; set; }
        public bool UDF_CsaVendor { get; set; }
        public object UDF_Danslocation { get; set; }
        public object UDF_Devclasstest { get; set; }
        public double UDF_Devdecimaltest { get; set; }
        public object UDF_Fundowner { get; set; }
        public object UDF_GregsField { get; set; }
        public object UDF_InheritanceUdf { get; set; }
        public object UDF_InheritanceUdf1 { get; set; }
        public object UDF_MaheshTest { get; set; }
        public object UDF_Manager { get; set; }
        public object UDF_NeetaApproves { get; set; }
        public object UDF_Number { get; set; }
        public string UDF_PaidBy { get; set; }
        public object UDF_Period { get; set; }
        public object UDF_Received1099 { get; set; }
        public string UDF_SoftDollar { get; set; }
        public object UDF_Subsidiary { get; set; }
        public object UDF_Test { get; set; }
        public object UDF_Test876 { get; set; }
        public object UDF_TestOneUdfInsert { get; set; }
        public object UDF_Testdemo { get; set; }
        public object UDF_Testdemo2 { get; set; }
        public object UDF_Testdemo24 { get; set; }
        public object UDF_Testeff { get; set; }
        public object UDF_Testinternal { get; set; }
        public object UDF_Testqa123 { get; set; }
        public object UDF_Testudfvalue { get; set; }
        public object UDF_Text { get; set; }
        public string UDF_Udf { get; set; }
        public object UDF_Udf1 { get; set; }
        public string UDF_UdfListText { get; set; }
        public int UDF_UdfNumber { get; set; }
        public string UDF_UdfPopUpTest { get; set; }
        public string UDF_Udfclass { get; set; }
        public object UDF_Udffromvendor { get; set; }
        public object UDF_Udfnum { get; set; }
        public int UDF_V349 { get; set; }
        public object UDF_V364DateUdf { get; set; }
        public object UDF_V364NumListUdf { get; set; }
        public string UDF_V364paymentmodewire { get; set; }
        public object UDF_Vat { get; set; }
        public object UDF_VendorClassification { get; set; }
        public string UDF_W9Date { get; set; }
        public object UDF_W9OnFile { get; set; }
        public object UDF_YesOrNo { get; set; }
        public object UDF_Yesno { get; set; }
    }

    /// <summary>
    /// Reference Payment information
    /// </summary>
    public class RefPayment
    {
        public int PaymentId { get; set; }
        public string PaymentMode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// Address information
    /// </summary>
    public class Address
    {
        public object AddressState { get; set; }
        public object AddressCountry { get; set; }
        public List<object> AddressModuleMapping { get; set; }
        public int AddressId { get; set; }
        public object AddressLine1 { get; set; }
        public object AddressLine2 { get; set; }
        public object AddressLine3 { get; set; }
        public object AddressLine4 { get; set; }
        public object AddressLine5 { get; set; }
        public object AddressCity { get; set; }
        public object AddressStateId { get; set; }
        public object AddressProvince { get; set; }
        public object AddressZipCode { get; set; }
        public object AddressCountryId { get; set; }
        public string AddressVerificationStatus { get; set; }
        public object AddressNote { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int LastModifiedById { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }

    /// <summary>
    /// Vendor 1099 Type information
    /// </summary>
    public class Vendor1099Type
    {
        public object ParentLookupValue { get; set; }
        public int LookupValueId { get; set; }
        public int CompanyId { get; set; }
        public int LookupTypeId { get; set; }
        public object LookupTypeEnum { get; set; }
        public string LookupValueName { get; set; }
        public object LookupLongName { get; set; }
        public string LookupName { get; set; }
        public object ExternalId { get; set; }
        public bool IsActive { get; set; }
        public object IsForwardAction { get; set; }
        public object Sequence { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public object ParentLookupValueId { get; set; }
    }

}
