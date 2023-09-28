using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Matter
{
    public  class AddProcessResponseModel
    {
        [JsonProperty("changes")]
        public List<Change> Changes { get; set; }

        [JsonProperty("dataStateChanges")]
        public List<DataStateChange> DataStateChanges { get; set; }
    }

    public partial class Change
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public ChangeValue? Value { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("op")]
        public string Op { get; set; }
    }

    public partial class PurpleValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("actions")]
        public ValueActions Actions { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("childObjects")]
        public ChildObjects ChildObjects { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("rowState")]
        public long RowState { get; set; }

        [JsonProperty("subclassId")]
        public string SubclassId { get; set; }
    }

    public partial class ValueActions
    {
        [JsonProperty("EditEntity")]
        public EditEntity EditEntity { get; set; }

        [JsonProperty("MattSplit")]
        public MattSplit MattSplit { get; set; }

        [JsonProperty("EditNBType")]
        public EditEntity EditNbType { get; set; }

        [JsonProperty("NewBillSite")]
        public EditEntity NewBillSite { get; set; }

        [JsonProperty("NewStmtSite")]
        public EditEntity NewStmtSite { get; set; }

        [JsonProperty("MatterBalances")]
        public EditEntity MatterBalances { get; set; }
    }

    public partial class EditEntity
    {
    }

    public partial class MattSplit
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("MattIndex")]
        public ElecTitleMap MattIndex { get; set; }

        [JsonProperty("Number")]
        public CloseDate Number { get; set; }

        [JsonProperty("AltNumber")]
        public AdminAccount AltNumber { get; set; }

        [JsonProperty("DisplayName")]
        public BillSite1Address1BillAddress DisplayName { get; set; }

        [JsonProperty("Description")]
        public AdminAccount Description { get; set; }

        [JsonProperty("Client")]
        public Client Client { get; set; }

        [JsonProperty("MattInfo")]
        public AdminAccount MattInfo { get; set; }

        [JsonProperty("RelMattIndex")]
        public ApproveTkpr RelMattIndex { get; set; }

        [JsonProperty("MattStatus")]
        public ElecTitleMap MattStatus { get; set; }

        [JsonProperty("MattStatusDate")]
        public MattStatusDate MattStatusDate { get; set; }

        [JsonProperty("MattType")]
        public ApproveTkpr MattType { get; set; }

        [JsonProperty("OpenDate")]
        public CloseDate OpenDate { get; set; }

        [JsonProperty("Narrative")]
        public Narrative Narrative { get; set; }

        [JsonProperty("BillingInstruc")]
        public AdminAccount BillingInstruc { get; set; }

        [JsonProperty("Language")]
        public ElecTitleMap Language { get; set; }

        [JsonProperty("ContactInfo")]
        public AdminAccount ContactInfo { get; set; }

        [JsonProperty("ReferralInfo")]
        public AdminAccount ReferralInfo { get; set; }

        [JsonProperty("MattCloseType")]
        public Client MattCloseType { get; set; }

        [JsonProperty("CloseDate")]
        public CloseDate CloseDate { get; set; }

        [JsonProperty("IsMaster")]
        public CloseDate IsMaster { get; set; }

        [JsonProperty("NonBillType")]
        public ApproveTkpr NonBillType { get; set; }

        [JsonProperty("IsProBono")]
        public CloseDate IsProBono { get; set; }

        [JsonProperty("ProBonoEntity")]
        public ApproveTkpr ProBonoEntity { get; set; }

        [JsonProperty("ProBonoInfo")]
        public AdminAccount ProBonoInfo { get; set; }

        [JsonProperty("IsAdmin")]
        public CloseDate IsAdmin { get; set; }

        [JsonProperty("AdminAccount")]
        public AdminAccount AdminAccount { get; set; }

        [JsonProperty("AdminInfo")]
        public AdminAccount AdminInfo { get; set; }

        [JsonProperty("ElecBillingType")]
        public ApproveTkpr ElecBillingType { get; set; }

        [JsonProperty("ElecNumber")]
        public AdminAccount ElecNumber { get; set; }

        [JsonProperty("ElecInfo")]
        public AdminAccount ElecInfo { get; set; }

        [JsonProperty("IsNonBillable")]
        public ApproveDate IsNonBillable { get; set; }

        [JsonProperty("BillSite")]
        public ApproveTkpr BillSite { get; set; }

        [JsonProperty("StatementSite")]
        public ApproveTkpr StatementSite { get; set; }

        [JsonProperty("OpenTkpr")]
        public Client OpenTkpr { get; set; }

        [JsonProperty("CloseTkpr")]
        public ApproveTkpr CloseTkpr { get; set; }

        [JsonProperty("Comments")]
        public AdminAccount Comments { get; set; }

        [JsonProperty("IsDefault")]
        public ApproveDate IsDefault { get; set; }

        [JsonProperty("BillingFrequency")]
        public ApproveTkpr BillingFrequency { get; set; }

        [JsonProperty("IsNoProforma")]
        public CloseDate IsNoProforma { get; set; }

        [JsonProperty("IsNoBill")]
        public CloseDate IsNoBill { get; set; }

        [JsonProperty("Markup")]
        public ApproveTkpr Markup { get; set; }

        [JsonProperty("WithholdingTax")]
        public ApproveTkpr WithholdingTax { get; set; }

        [JsonProperty("TimeTaxCode")]
        public ApproveTkpr TimeTaxCode { get; set; }

        [JsonProperty("CostTaxCode")]
        public ApproveTkpr CostTaxCode { get; set; }

        [JsonProperty("ChrgTaxCode")]
        public ApproveTkpr ChrgTaxCode { get; set; }

        [JsonProperty("DueDays")]
        public AdminAccount DueDays { get; set; }

        [JsonProperty("Currency")]
        public CloseDate Currency { get; set; }

        [JsonProperty("CurrencyDateList")]
        public Client CurrencyDateList { get; set; }

        [JsonProperty("ElecCostTypeMap")]
        public ApproveDate ElecCostTypeMap { get; set; }

        [JsonProperty("TimeIncrement")]
        public ApproveTkpr TimeIncrement { get; set; }

        [JsonProperty("IsAutoNumbering")]
        public BillSite1Address1BillAddress IsAutoNumbering { get; set; }

        [JsonProperty("IsEngageLetterReq")]
        public CloseDate IsEngageLetterReq { get; set; }

        [JsonProperty("EngageLetterSubDate")]
        public ApproveDate EngageLetterSubDate { get; set; }

        [JsonProperty("EngageLetterRecDate")]
        public ApproveDate EngageLetterRecDate { get; set; }

        [JsonProperty("EngageLetterComment")]
        public AdminAccount EngageLetterComment { get; set; }

        [JsonProperty("IsWaiverLetterReq")]
        public CloseDate IsWaiverLetterReq { get; set; }

        [JsonProperty("WaiverLetterSubDate")]
        public ApproveDate WaiverLetterSubDate { get; set; }

        [JsonProperty("WaiverLetterRecDate")]
        public ApproveDate WaiverLetterRecDate { get; set; }

        [JsonProperty("WaiverLetterComment")]
        public AdminAccount WaiverLetterComment { get; set; }

        [JsonProperty("IsConflictsConfidential")]
        public ApproveDate IsConflictsConfidential { get; set; }

        [JsonProperty("BillDCSTemplate")]
        public ApproveTkpr BillDcsTemplate { get; set; }

        [JsonProperty("ProfDCSTemplate")]
        public ApproveTkpr ProfDcsTemplate { get; set; }

        [JsonProperty("StmtDCSTemplate")]
        public ApproveTkpr StmtDcsTemplate { get; set; }

        [JsonProperty("ApproveTkpr")]
        public ApproveTkpr ApproveTkpr { get; set; }

        [JsonProperty("MattAttribute")]
        public ApproveTkpr MattAttribute { get; set; }

        [JsonProperty("MattCategory")]
        public ApproveTkpr MattCategory { get; set; }

        [JsonProperty("EntryDate")]
        public ApproveDate EntryDate { get; set; }

        [JsonProperty("VATRegistration")]
        public AdminAccount VatRegistration { get; set; }

        [JsonProperty("MattMinType")]
        public ApproveTkpr MattMinType { get; set; }

        [JsonProperty("GLProject")]
        public ApproveTkpr GlProject { get; set; }

        [JsonProperty("IsForeign")]
        public ApproveDate IsForeign { get; set; }

        [JsonProperty("VolumeDiscountGroup")]
        public ApproveTkpr VolumeDiscountGroup { get; set; }

        [JsonProperty("MattInterest")]
        public ApproveTkpr MattInterest { get; set; }

        [JsonProperty("IsEBill")]
        public CloseDate IsEBill { get; set; }

        [JsonProperty("ElecTitleMap")]
        public ElecTitleMap ElecTitleMap { get; set; }

        [JsonProperty("ElecDCSTemplate")]
        public ApproveTkpr ElecDcsTemplate { get; set; }

        [JsonProperty("PaymentTermsInfo")]
        public AdminAccount PaymentTermsInfo { get; set; }

        [JsonProperty("IsFeeEstimate")]
        public CloseDate IsFeeEstimate { get; set; }

        [JsonProperty("FeeEstimateAmount")]
        public AdminAccount FeeEstimateAmount { get; set; }

        [JsonProperty("EstimatedCompletionDate")]
        public ApproveDate EstimatedCompletionDate { get; set; }

        [JsonProperty("ApproveDate")]
        public ApproveDate ApproveDate { get; set; }

        [JsonProperty("InvoiceOverride")]
        public ApproveTkpr InvoiceOverride { get; set; }

        [JsonProperty("ProformaEmail")]
        public AdminAccount ProformaEmail { get; set; }

        [JsonProperty("BillEmail")]
        public AdminAccount BillEmail { get; set; }

        [JsonProperty("IsNumberEnabled")]
        public BillSite1Address1BillAddress IsNumberEnabled { get; set; }

        [JsonProperty("GLRespTkpr")]
        public ApproveTkpr GlRespTkpr { get; set; }

        [JsonProperty("IsICBAcctRec")]
        public CloseDate IsIcbAcctRec { get; set; }

        [JsonProperty("IsICBPayable")]
        public CloseDate IsIcbPayable { get; set; }

        [JsonProperty("ICBUnitDueTo")]
        public Client IcbUnitDueTo { get; set; }

        [JsonProperty("ICBUnitDueFrom")]
        public Client IcbUnitDueFrom { get; set; }

        [JsonProperty("IsAllowTrustOverdraw")]
        public CloseDate IsAllowTrustOverdraw { get; set; }

        [JsonProperty("BillingOffice")]
        public ApproveTkpr BillingOffice { get; set; }

        [JsonProperty("TaxReportID1")]
        public AdminAccount TaxReportId1 { get; set; }

        [JsonProperty("TaxReportID2")]
        public AdminAccount TaxReportId2 { get; set; }

        [JsonProperty("ToTaxArea")]
        public ApproveTkpr ToTaxArea { get; set; }

        [JsonProperty("IsLeadVolumeDiscountMatter")]
        public CloseDate IsLeadVolumeDiscountMatter { get; set; }

        [JsonProperty("IsBillStatementIncludeDoubtful")]
        public CloseDate IsBillStatementIncludeDoubtful { get; set; }

        [JsonProperty("WPType")]
        public ApproveTkpr WpType { get; set; }

        [JsonProperty("GLActivity")]
        public ApproveTkpr GlActivity { get; set; }

        [JsonProperty("CreditNoteDCSTemplate")]
        public ApproveTkpr CreditNoteDcsTemplate { get; set; }

        [JsonProperty("ElecTaxCodeMap")]
        public AdminAccount ElecTaxCodeMap { get; set; }

        [JsonProperty("BillGroupDCSTemplate")]
        public ApproveTkpr BillGroupDcsTemplate { get; set; }

        [JsonProperty("CreditNoteGroupDCSTemplate")]
        public ApproveTkpr CreditNoteGroupDcsTemplate { get; set; }

        [JsonProperty("IsExportRestricted")]
        public CloseDate IsExportRestricted { get; set; }

        [JsonProperty("IsOnlyABATimeTypes")]
        public ApproveDate IsOnlyAbaTimeTypes { get; set; }

        [JsonProperty("eBHClientList")]
        public ApproveTkpr EBhClientList { get; set; }

        [JsonProperty("EBHArrangement")]
        public ApproveTkpr EbhArrangement { get; set; }

        [JsonProperty("Client1.ClientDispName")]
        public BillSite1Address1BillAddress Client1ClientDispName { get; set; }

        [JsonProperty("Client1.IsClientExportRestricted")]
        public BillSite1Address1BillAddress Client1IsClientExportRestricted { get; set; }

        [JsonProperty("Client1.Entity1.EntIndex")]
        public BillSite1Address1BillAddress Client1Entity1EntIndex { get; set; }

        [JsonProperty("Client1.VolumeDiscountRel.ClientVolumeDiscountDescription")]
        public BillSite1Address1BillAddress Client1VolumeDiscountRelClientVolumeDiscountDescription { get; set; }

        [JsonProperty("Client1.RelatedClient1.RelClientDiscountGroup")]
        public Client Client1RelatedClient1RelClientDiscountGroup { get; set; }

        [JsonProperty("RelMattIndex1.DisplayName")]
        public BillSite1Address1BillAddress RelMattIndex1DisplayName { get; set; }

        [JsonProperty("Entity1.ProBonoEntityName")]
        public BillSite1Address1BillAddress Entity1ProBonoEntityName { get; set; }

        [JsonProperty("BillSite1.Address1.BillAddress")]
        public BillSite1Address1BillAddress BillSite1Address1BillAddress { get; set; }

        [JsonProperty("StatementSite1.Address1.StmtAddress")]
        public BillSite1Address1BillAddress StatementSite1Address1StmtAddress { get; set; }

        [JsonProperty("OpenTkprRel.DisplayName")]
        public BillSite1Address1BillAddress OpenTkprRelDisplayName { get; set; }

        [JsonProperty("CloseTkprRel.DisplayName")]
        public BillSite1Address1BillAddress CloseTkprRelDisplayName { get; set; }

        [JsonProperty("MatterTaxOverride_ccc")]
        public ApproveTkpr MatterTaxOverrideCcc { get; set; }

        [JsonProperty("BankAcct_ccc")]
        public ApproveTkpr BankAcctCcc { get; set; }

        [JsonProperty("UDFList_ccc")]
        public ApproveTkpr UdfListCcc { get; set; }

        [JsonProperty("BillTkprDispName")]
        public BillSite1Address1BillAddress BillTkprDispName { get; set; }
    }

    public partial class AdminAccount
    {
        [JsonProperty("dataType")]
        public long DataType { get; set; }
    }

    public partial class ApproveDate
    {
        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class ApproveTkpr
    {
        [JsonProperty("aliasValue")]
        public string AliasValue { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }
    }

    public partial class BillSite1Address1BillAddress
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }
    }

    public partial class Client
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }

        [JsonProperty("aliasValue")]
        public string AliasValue { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }
    }

    public partial class CloseDate
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("displayValue", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayValue { get; set; }
    }

    public partial class ElecTitleMap
    {
        [JsonProperty("aliasValue", NullValueHandling = NullValueHandling.Ignore)]
        public string AliasValue { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("accessType", NullValueHandling = NullValueHandling.Ignore)]
        public long? AccessType { get; set; }
    }

    public partial class MattStatusDate
    {
        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

        [JsonProperty("value")]
        public DateTimeOffset Value { get; set; }
    }

    public partial class Narrative
    {
        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }
    }

    public partial class ChildObjects
    {
        [JsonProperty("MattDate")]
        public ChildObjectsMattDate MattDate { get; set; }

        [JsonProperty("MattSite")]
        public ChildObjectsBillingGroupMatter1 MattSite { get; set; }

        [JsonProperty("MattBillingContact")]
        public ChildObjectsBillingGroupMatter1 MattBillingContact { get; set; }

        [JsonProperty("MattRate")]
        public MattRateClass MattRate { get; set; }

        [JsonProperty("RateExc")]
        public RateExc RateExc { get; set; }

        [JsonProperty("MattTaxonomy")]
        public ChildObjectsBillingGroupMatter1 MattTaxonomy { get; set; }

        [JsonProperty("MatterRateExc")]
        public ChildObjectsBillingGroupMatter1 MatterRateExc { get; set; }

        [JsonProperty("MattTemplateOption")]
        public ChildObjectsBillingGroupMatter1 MattTemplateOption { get; set; }

        [JsonProperty("BillingGroupMatter1")]
        public ChildObjectsBillingGroupMatter1 BillingGroupMatter1 { get; set; }

        [JsonProperty("MattProfAdjust")]
        public ChildObjectsBillingGroupMatter1 MattProfAdjust { get; set; }

        [JsonProperty("MaskOverrideValues")]
        public ChildObjectsBillingGroupMatter1 MaskOverrideValues { get; set; }

        [JsonProperty("MattBudget")]
        public ChildObjectsBillingGroupMatter1 MattBudget { get; set; }

        [JsonProperty("MattFlatFee")]
        public ChildObjectsBillingGroupMatter1 MattFlatFee { get; set; }

        [JsonProperty("MattPhaseException")]
        public ChildObjectsBillingGroupMatter1 MattPhaseException { get; set; }

        [JsonProperty("MattIndustryGroup")]
        public ChildObjectsBillingGroupMatter1 MattIndustryGroup { get; set; }

        [JsonProperty("MattPracticeTeam")]
        public ChildObjectsBillingGroupMatter1 MattPracticeTeam { get; set; }

        [JsonProperty("MattCostTypeSummarize")]
        public ChildObjectsBillingGroupMatter1 MattCostTypeSummarize { get; set; }

        [JsonProperty("CmCase")]
        public CmCase CmCase { get; set; }

        [JsonProperty("MattPayor")]
        public MattAltBillArrange2Class MattPayor { get; set; }

        [JsonProperty("MattTrust")]
        public MattAltBillArrange2Class MattTrust { get; set; }

        [JsonProperty("MattTaxArticle")]
        public MattAltBillArrange2Class MattTaxArticle { get; set; }

        [JsonProperty("MattNote")]
        public MattAltBillArrange2Class MattNote { get; set; }

        [JsonProperty("MattEBillValList")]
        public ChrgTypeGroupMatterCcc MattEBillValList { get; set; }

        [JsonProperty("MatterAdditionalInfo")]
        public MatterAdditionalInfo MatterAdditionalInfo { get; set; }

        [JsonProperty("TimeTypeGroupMatter")]
        public TimeTypeGroupMatter TimeTypeGroupMatter { get; set; }

        [JsonProperty("MattAltBillArrange2")]
        public MattAltBillArrange2Class MattAltBillArrange2 { get; set; }

        [JsonProperty("ChrgTypeGroupMatter_ccc")]
        public ChrgTypeGroupMatterCcc ChrgTypeGroupMatterCcc { get; set; }

        [JsonProperty("CostTypeGroupMatter_ccc")]
        public ChrgTypeGroupMatterCcc CostTypeGroupMatterCcc { get; set; }

        [JsonProperty("MattUDF_ccc")]
        public MattRateClass MattUdfCcc { get; set; }
    }

    public partial class ChildObjectsBillingGroupMatter1
    {
        [JsonProperty("actions")]
        public BillingGroupMatter1Actions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class BillingGroupMatter1Actions
    {
        [JsonProperty("Add")]
        public EditEntity Add { get; set; }

        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("Audit")]
        public MattSplit Audit { get; set; }

        [JsonProperty("Attachments")]
        public MattSplit Attachments { get; set; }

        [JsonProperty("ACL")]
        public MattSplit Acl { get; set; }

        [JsonProperty("BillingGroup", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity BillingGroup { get; set; }

        [JsonProperty("Clone", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity Clone { get; set; }

        [JsonProperty("AddRateExc", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity AddRateExc { get; set; }
    }

    public partial class ChrgTypeGroupMatterCcc
    {
        [JsonProperty("actions")]
        public ChrgTypeGroupMatterCccActions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class ChrgTypeGroupMatterCccActions
    {
        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("NewChrgTypeGroup", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity NewChrgTypeGroup { get; set; }

        [JsonProperty("AddByQuery")]
        public EditEntity AddByQuery { get; set; }

        [JsonProperty("NewCostTypeGroup", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity NewCostTypeGroup { get; set; }
    }

    public partial class CmCase
    {
        [JsonProperty("actions")]
        public CmCaseActions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class CmCaseActions
    {
        [JsonProperty("Add")]
        public EditEntity Add { get; set; }

        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("Audit")]
        public EditEntity Audit { get; set; }

        [JsonProperty("Attachments")]
        public EditEntity Attachments { get; set; }

        [JsonProperty("ACL")]
        public MattSplit Acl { get; set; }
    }

    public partial class MattAltBillArrange2Class
    {
        [JsonProperty("actions")]
        public MattAltBillArrange2Actions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class MattAltBillArrange2Actions
    {
        [JsonProperty("Add")]
        public EditEntity Add { get; set; }

        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("Clone", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity Clone { get; set; }
    }

    public partial class ChildObjectsMattDate
    {
        [JsonProperty("actions")]
        public MattDateActions Actions { get; set; }

        [JsonProperty("actualRowCount")]
        public long ActualRowCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class MattDateActions
    {
        [JsonProperty("Add")]
        public EditEntity Add { get; set; }

        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("Audit")]
        public EditEntity Audit { get; set; }

        [JsonProperty("Attachments")]
        public MattSplit Attachments { get; set; }

        [JsonProperty("ACL")]
        public MattSplit Acl { get; set; }

        [JsonProperty("Clone", NullValueHandling = NullValueHandling.Ignore)]
        public EditEntity Clone { get; set; }
    }

    public partial class MattRateClass
    {
        [JsonProperty("actions")]
        public BillingGroupMatter1Actions Actions { get; set; }

        [JsonProperty("actualRowCount")]
        public long ActualRowCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class MatterAdditionalInfo
    {
        [JsonProperty("actions")]
        public MatterAdditionalInfoActions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class MatterAdditionalInfoActions
    {
        [JsonProperty("Add")]
        public EditEntity Add { get; set; }

        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }
    }

    public partial class RateExc
    {
        [JsonProperty("actions")]
        public MattDateActions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class TimeTypeGroupMatter
    {
        [JsonProperty("actions")]
        public TimeTypeGroupMatterActions Actions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rows")]
        public EditEntity Rows { get; set; }
    }

    public partial class TimeTypeGroupMatterActions
    {
        [JsonProperty("Delete")]
        public EditEntity Delete { get; set; }

        [JsonProperty("Remove")]
        public EditEntity Remove { get; set; }

        [JsonProperty("Audit")]
        public MattSplit Audit { get; set; }

        [JsonProperty("Attachments")]
        public MattSplit Attachments { get; set; }

        [JsonProperty("ACL")]
        public MattSplit Acl { get; set; }

        [JsonProperty("AddByQuery")]
        public EditEntity AddByQuery { get; set; }

        [JsonProperty("NewTimeTypeGroup")]
        public EditEntity NewTimeTypeGroup { get; set; }
    }

    public partial class DataStateChange
    {
        [JsonProperty("value")]
        public DataStateChangeValue Value { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("op")]
        public string Op { get; set; }
    }

    public partial class FluffyValue
    {
        [JsonProperty("childStates")]
        public ValueChildStates ChildStates { get; set; }
    }

    public partial class ValueChildStates
    {
        [JsonProperty("NxAttachment")]
        public ChrgTypeGroupMatterCccClass NxAttachment { get; set; }

        [JsonProperty("MattDate")]
        public ChildStatesMattDate MattDate { get; set; }

        [JsonProperty("MattSite")]
        public ChrgTypeGroupMatterCccClass MattSite { get; set; }

        [JsonProperty("MattBillingContact")]
        public ChrgTypeGroupMatterCccClass MattBillingContact { get; set; }

        [JsonProperty("MattRate")]
        public MattRate MattRate { get; set; }

        [JsonProperty("RateExc")]
        public ChrgTypeGroupMatterCccClass RateExc { get; set; }

        [JsonProperty("MattTaxonomy")]
        public ChrgTypeGroupMatterCccClass MattTaxonomy { get; set; }

        [JsonProperty("MatterRateExc")]
        public ChrgTypeGroupMatterCccClass MatterRateExc { get; set; }

        [JsonProperty("MattTemplateOption")]
        public ChrgTypeGroupMatterCccClass MattTemplateOption { get; set; }

        [JsonProperty("BillingGroupMatter1")]
        public ChrgTypeGroupMatterCccClass BillingGroupMatter1 { get; set; }

        [JsonProperty("MattProfAdjust")]
        public ChrgTypeGroupMatterCccClass MattProfAdjust { get; set; }

        [JsonProperty("MaskOverrideValues")]
        public ChrgTypeGroupMatterCccClass MaskOverrideValues { get; set; }

        [JsonProperty("MattBudget")]
        public ChrgTypeGroupMatterCccClass MattBudget { get; set; }

        [JsonProperty("MattFlatFee")]
        public ChrgTypeGroupMatterCccClass MattFlatFee { get; set; }

        [JsonProperty("MattPhaseException")]
        public ChrgTypeGroupMatterCccClass MattPhaseException { get; set; }

        [JsonProperty("MattIndustryGroup")]
        public ChrgTypeGroupMatterCccClass MattIndustryGroup { get; set; }

        [JsonProperty("MattPracticeTeam")]
        public ChrgTypeGroupMatterCccClass MattPracticeTeam { get; set; }

        [JsonProperty("MattCostTypeSummarize")]
        public ChrgTypeGroupMatterCccClass MattCostTypeSummarize { get; set; }

        [JsonProperty("CmCase")]
        public ChrgTypeGroupMatterCccClass CmCase { get; set; }

        [JsonProperty("MattPayor")]
        public ChrgTypeGroupMatterCccClass MattPayor { get; set; }

        [JsonProperty("MattTrust")]
        public ChrgTypeGroupMatterCccClass MattTrust { get; set; }

        [JsonProperty("MattTaxArticle")]
        public ChrgTypeGroupMatterCccClass MattTaxArticle { get; set; }

        [JsonProperty("MattNote")]
        public ChrgTypeGroupMatterCccClass MattNote { get; set; }

        [JsonProperty("MattEBillValList")]
        public ChrgTypeGroupMatterCccClass MattEBillValList { get; set; }

        [JsonProperty("MatterAdditionalInfo")]
        public ChrgTypeGroupMatterCccClass MatterAdditionalInfo { get; set; }

        [JsonProperty("TimeTypeGroupMatter")]
        public ChrgTypeGroupMatterCccClass TimeTypeGroupMatter { get; set; }

        [JsonProperty("MattAltBillArrange2")]
        public ChrgTypeGroupMatterCccClass MattAltBillArrange2 { get; set; }

        [JsonProperty("LxWallMatter")]
        public ChrgTypeGroupMatterCccClass LxWallMatter { get; set; }

        [JsonProperty("ChrgTypeGroupMatter_ccc")]
        public ChrgTypeGroupMatterCccClass ChrgTypeGroupMatterCcc { get; set; }

        [JsonProperty("CostTypeGroupMatter_ccc")]
        public ChrgTypeGroupMatterCccClass CostTypeGroupMatterCcc { get; set; }

        [JsonProperty("MattUDF_ccc")]
        public MattUdfCcc MattUdfCcc { get; set; }
    }

    public partial class ChrgTypeGroupMatterCccClass
    {
        [JsonProperty("rowStates")]
        public EditEntity RowStates { get; set; }
    }

    public partial class ChildStatesMattDate
    {
        [JsonProperty("currentRowId")]
        public string CurrentRowId { get; set; }

        [JsonProperty("rowStates")]
        public MattDateRowStates RowStates { get; set; }
    }

    public partial class MattDateRowStates
    {
        [JsonProperty("e1119ac54cf24578952d732828cac197")]
        public E1119Ac54Cf24578952D732828Cac197 E1119Ac54Cf24578952D732828Cac197 { get; set; }
    }

    public partial class E1119Ac54Cf24578952D732828Cac197
    {
        [JsonProperty("childStates")]
        public E1119Ac54Cf24578952D732828Cac197ChildStates ChildStates { get; set; }
    }

    public partial class E1119Ac54Cf24578952D732828Cac197ChildStates
    {
        [JsonProperty("MattOrgTkpr")]
        public ChrgTypeGroupMatterCccClass MattOrgTkpr { get; set; }

        [JsonProperty("MattPrlfTkpr")]
        public ChrgTypeGroupMatterCccClass MattPrlfTkpr { get; set; }

        [JsonProperty("MattSpvTkpr")]
        public ChrgTypeGroupMatterCccClass MattSpvTkpr { get; set; }
    }

    public partial class MattRate
    {
        [JsonProperty("currentRowId")]
        public string CurrentRowId { get; set; }

        [JsonProperty("rowStates")]
        public MattRateRowStates RowStates { get; set; }
    }

    public partial class MattRateRowStates
    {
        [JsonProperty("95e2ff5887d14f8eb04ce5a254a891fd")]
        public The95_E2Ff5887D14F8Eb04Ce5A254A891Fd The95E2Ff5887D14F8Eb04Ce5A254A891Fd { get; set; }
    }

    public partial class The95_E2Ff5887D14F8Eb04Ce5A254A891Fd
    {
        [JsonProperty("childStates")]
        public EditEntity ChildStates { get; set; }
    }

    public partial class MattUdfCcc
    {
        [JsonProperty("currentRowId")]
        public string CurrentRowId { get; set; }

        [JsonProperty("rowStates")]
        public Dictionary<string, The95_E2Ff5887D14F8Eb04Ce5A254A891Fd> RowStates { get; set; }
    }

    public partial struct ChangeValue
    {
        public long? Integer;
        public PurpleValue PurpleValue;

        public static implicit operator ChangeValue(long Integer) => new() { Integer = Integer };
        public static implicit operator ChangeValue(PurpleValue PurpleValue) => new() { PurpleValue = PurpleValue };
    }

    public partial struct DataStateChangeValue
    {
        public FluffyValue FluffyValue;
        public long? Integer;
        public string String;

        public static implicit operator DataStateChangeValue(FluffyValue FluffyValue) => new() { FluffyValue = FluffyValue };
        public static implicit operator DataStateChangeValue(long Integer) => new() { Integer = Integer };
        public static implicit operator DataStateChangeValue(string String) => new() { String = String };
    }

}
