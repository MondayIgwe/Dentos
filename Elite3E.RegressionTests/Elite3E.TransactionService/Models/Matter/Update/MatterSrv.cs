using System.Xml.Serialization;

namespace Elite3E.SoapServices.Models.Matter.Update
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(MatterSrv));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (MatterSrv)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class Attributes
	{

		[XmlElement(ElementName = "Number", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Number { get; set; }

		[XmlElement(ElementName = "AltNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AltNumber { get; set; }

		[XmlElement(ElementName = "DisplayName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DisplayName { get; set; }

		[XmlElement(ElementName = "Description", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Description { get; set; }

		[XmlElement(ElementName = "Client", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Client { get; set; }

		[XmlElement(ElementName = "MattInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattInfo { get; set; }

		[XmlElement(ElementName = "RelMattIndex", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RelMattIndex { get; set; }

		[XmlElement(ElementName = "MattStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattStatus { get; set; }

		[XmlElement(ElementName = "MattStatusDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattStatusDate { get; set; }

		[XmlElement(ElementName = "MattType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattType { get; set; }

		[XmlElement(ElementName = "OpenDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string OpenDate { get; set; }

		[XmlElement(ElementName = "ConflictStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ConflictStatus { get; set; }

		[XmlElement(ElementName = "Narrative", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Narrative { get; set; }

		[XmlElement(ElementName = "BillingInstruc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingInstruc { get; set; }

		[XmlElement(ElementName = "Language", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Language { get; set; }

		[XmlElement(ElementName = "ContactInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ContactInfo { get; set; }

		[XmlElement(ElementName = "ReferralInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ReferralInfo { get; set; }

		[XmlElement(ElementName = "MattCloseType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattCloseType { get; set; }

		[XmlElement(ElementName = "CloseDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CloseDate { get; set; }

		[XmlElement(ElementName = "IsMaster", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsMaster { get; set; }

		[XmlElement(ElementName = "NonBillType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NonBillType { get; set; }

		[XmlElement(ElementName = "IsProBono", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsProBono { get; set; }

		[XmlElement(ElementName = "ProBonoEntity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProBonoEntity { get; set; }

		[XmlElement(ElementName = "ProBonoInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProBonoInfo { get; set; }

		[XmlElement(ElementName = "IsAdmin", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsAdmin { get; set; }

		[XmlElement(ElementName = "AdminAccount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AdminAccount { get; set; }

		[XmlElement(ElementName = "AdminInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AdminInfo { get; set; }

		[XmlElement(ElementName = "ElecBillingType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecBillingType { get; set; }

		[XmlElement(ElementName = "ElecNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecNumber { get; set; }

		[XmlElement(ElementName = "ElecInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecInfo { get; set; }

		[XmlElement(ElementName = "IsNonBillable", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsNonBillable { get; set; }

		[XmlElement(ElementName = "BillAsMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillAsMatter { get; set; }

		[XmlElement(ElementName = "BillSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillSite { get; set; }

		[XmlElement(ElementName = "StatementSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StatementSite { get; set; }

		[XmlElement(ElementName = "IsValidate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsValidate { get; set; }

		[XmlElement(ElementName = "OpenTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string OpenTkpr { get; set; }

		[XmlElement(ElementName = "CloseTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CloseTkpr { get; set; }

		[XmlElement(ElementName = "Comments", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Comments { get; set; }

		[XmlElement(ElementName = "IsDefault", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsDefault { get; set; }

		[XmlElement(ElementName = "BillingFrequency", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingFrequency { get; set; }

		[XmlElement(ElementName = "IsNoProforma", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsNoProforma { get; set; }

		[XmlElement(ElementName = "IsNoBill", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsNoBill { get; set; }

		[XmlElement(ElementName = "Markup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Markup { get; set; }

		[XmlElement(ElementName = "WithholdingTax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WithholdingTax { get; set; }

		[XmlElement(ElementName = "TimeTaxCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeTaxCode { get; set; }

		[XmlElement(ElementName = "CostTaxCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CostTaxCode { get; set; }

		[XmlElement(ElementName = "ChrgTaxCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ChrgTaxCode { get; set; }

		[XmlElement(ElementName = "DueDays", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DueDays { get; set; }

		[XmlElement(ElementName = "Currency", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Currency { get; set; }

		[XmlElement(ElementName = "CurrencyDateList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CurrencyDateList { get; set; }

		[XmlElement(ElementName = "ElecCostTypeMap", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecCostTypeMap { get; set; }

		[XmlElement(ElementName = "TimeIncrement", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeIncrement { get; set; }

		[XmlElement(ElementName = "IsAutoNumbering", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsAutoNumbering { get; set; }

		[XmlElement(ElementName = "IsEngageLetterReq", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsEngageLetterReq { get; set; }

		[XmlElement(ElementName = "EngageLetterSubDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EngageLetterSubDate { get; set; }

		[XmlElement(ElementName = "EngageLetterRecDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EngageLetterRecDate { get; set; }

		[XmlElement(ElementName = "EngageLetterComment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EngageLetterComment { get; set; }

		[XmlElement(ElementName = "IsWaiverLetterReq", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsWaiverLetterReq { get; set; }

		[XmlElement(ElementName = "WaiverLetterSubDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WaiverLetterSubDate { get; set; }

		[XmlElement(ElementName = "WaiverLetterRecDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WaiverLetterRecDate { get; set; }

		[XmlElement(ElementName = "WaiverLetterComment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WaiverLetterComment { get; set; }

		[XmlElement(ElementName = "IsConflictsConfidential", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsConflictsConfidential { get; set; }

		[XmlElement(ElementName = "BillDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillDCSTemplate { get; set; }

		[XmlElement(ElementName = "ProfDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProfDCSTemplate { get; set; }

		[XmlElement(ElementName = "StmtDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StmtDCSTemplate { get; set; }

		[XmlElement(ElementName = "IsAutoNumAfterSave", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsAutoNumAfterSave { get; set; }

		[XmlElement(ElementName = "ApproveTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ApproveTkpr { get; set; }

		[XmlElement(ElementName = "MattAttribute", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattAttribute { get; set; }

		[XmlElement(ElementName = "MattCategory", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattCategory { get; set; }

		[XmlElement(ElementName = "EntryDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EntryDate { get; set; }

		[XmlElement(ElementName = "VATRegistration", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string VATRegistration { get; set; }

		[XmlElement(ElementName = "MattMinType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattMinType { get; set; }

		[XmlElement(ElementName = "GLProject", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLProject { get; set; }

		[XmlElement(ElementName = "IsForeign", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsForeign { get; set; }

		[XmlElement(ElementName = "VolumeDiscountGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string VolumeDiscountGroup { get; set; }

		[XmlElement(ElementName = "MattInterest", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattInterest { get; set; }

		[XmlElement(ElementName = "IsEBill", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsEBill { get; set; }

		[XmlElement(ElementName = "ElecTitleMap", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecTitleMap { get; set; }

		[XmlElement(ElementName = "ElecDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecDCSTemplate { get; set; }

		[XmlElement(ElementName = "PaymentTermsInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PaymentTermsInfo { get; set; }

		[XmlElement(ElementName = "IsFeeEstimate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsFeeEstimate { get; set; }

		[XmlElement(ElementName = "FeeEstimateAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FeeEstimateAmount { get; set; }

		[XmlElement(ElementName = "EstimatedCompletionDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EstimatedCompletionDate { get; set; }

		[XmlElement(ElementName = "ApproveDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ApproveDate { get; set; }

		[XmlElement(ElementName = "InvoiceOverride", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string InvoiceOverride { get; set; }

		[XmlElement(ElementName = "ProformaEmail", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProformaEmail { get; set; }

		[XmlElement(ElementName = "BillEmail", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillEmail { get; set; }

		[XmlElement(ElementName = "IsNumberEnabled", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsNumberEnabled { get; set; }

		[XmlElement(ElementName = "GLRespTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLRespTkpr { get; set; }

		[XmlElement(ElementName = "IsICBAcctRec", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsICBAcctRec { get; set; }

		[XmlElement(ElementName = "IsICBPayable", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsICBPayable { get; set; }

		[XmlElement(ElementName = "ICBUnitDueTo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ICBUnitDueTo { get; set; }

		[XmlElement(ElementName = "ICBUnitDueFrom", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ICBUnitDueFrom { get; set; }

		[XmlElement(ElementName = "HasTimekeeperChanged", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string HasTimekeeperChanged { get; set; }

		[XmlElement(ElementName = "IsAllowTrustOverdraw", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsAllowTrustOverdraw { get; set; }

		[XmlElement(ElementName = "BillingOffice", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingOffice { get; set; }

		[XmlElement(ElementName = "BankAcctAp", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BankAcctAp { get; set; }

		[XmlElement(ElementName = "TaxReportID1", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TaxReportID1 { get; set; }

		[XmlElement(ElementName = "TaxReportID2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TaxReportID2 { get; set; }

		[XmlElement(ElementName = "ToTaxArea", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ToTaxArea { get; set; }

		[XmlElement(ElementName = "IsLeadVolumeDiscountMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsLeadVolumeDiscountMatter { get; set; }

		[XmlElement(ElementName = "IsBillStatementIncludeDoubtful", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsBillStatementIncludeDoubtful { get; set; }

		[XmlElement(ElementName = "LoadGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LoadGroup { get; set; }

		[XmlElement(ElementName = "LoadNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LoadNumber { get; set; }

		[XmlElement(ElementName = "LoadSource", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LoadSource { get; set; }

		[XmlElement(ElementName = "WPType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WPType { get; set; }

		[XmlElement(ElementName = "BillTkprDispName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillTkprDispName { get; set; }

		[XmlElement(ElementName = "GLActivity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLActivity { get; set; }

		[XmlElement(ElementName = "CreditNoteDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CreditNoteDCSTemplate { get; set; }

		[XmlElement(ElementName = "ElecTaxCodeMap", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ElecTaxCodeMap { get; set; }

		[XmlElement(ElementName = "BillGroupDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillGroupDCSTemplate { get; set; }

		[XmlElement(ElementName = "CreditNoteGroupDCSTemplate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CreditNoteGroupDCSTemplate { get; set; }

		[XmlElement(ElementName = "IsExportRestricted", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsExportRestricted { get; set; }

		[XmlElement(ElementName = "IsOnlyABATimeTypes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsOnlyABATimeTypes { get; set; }

		[XmlElement(ElementName = "EffStart", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EffStart { get; set; }

		[XmlElement(ElementName = "PracticeGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PracticeGroup { get; set; }

		[XmlElement(ElementName = "Department", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Department { get; set; }

		[XmlElement(ElementName = "Section", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Section { get; set; }

		[XmlElement(ElementName = "Arrangement", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Arrangement { get; set; }

		[XmlElement(ElementName = "TkprTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TkprTeam { get; set; }

		[XmlElement(ElementName = "ReservesGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ReservesGroup { get; set; }

		[XmlElement(ElementName = "PTAGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroup { get; set; }

		[XmlElement(ElementName = "Office", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Office { get; set; }

		[XmlElement(ElementName = "MattSplitType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattSplitType { get; set; }

		[XmlElement(ElementName = "BillTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillTkpr { get; set; }

		[XmlElement(ElementName = "RspTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RspTkpr { get; set; }

		[XmlElement(ElementName = "SpvTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SpvTkpr { get; set; }

		[XmlElement(ElementName = "PTAGroupCost", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroupCost { get; set; }

		[XmlElement(ElementName = "PTAGroupChrg", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroupChrg { get; set; }

		[XmlElement(ElementName = "IsFireValidation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsFireValidation { get; set; }

		[XmlElement(ElementName = "PTAGroup2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroup2 { get; set; }

		[XmlElement(ElementName = "PTAGroupChrg2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroupChrg2 { get; set; }

		[XmlElement(ElementName = "PTAGroupCost2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PTAGroupCost2 { get; set; }

		[XmlElement(ElementName = "NxStartDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NxStartDate { get; set; }

		[XmlElement(ElementName = "Timekeeper", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Timekeeper { get; set; }

		[XmlElement(ElementName = "Percentage", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Percentage { get; set; }

		[XmlElement(ElementName = "IsPrimary", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsPrimary { get; set; }

		[XmlElement(ElementName = "Site", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Site { get; set; }

		[XmlElement(ElementName = "MattSiteType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattSiteType { get; set; }

		[XmlElement(ElementName = "StartDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StartDate { get; set; }

		[XmlElement(ElementName = "FinishDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FinishDate { get; set; }

		[XmlElement(ElementName = "BillingContactType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingContactType { get; set; }

		[XmlElement(ElementName = "EntityPerson", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EntityPerson { get; set; }

		[XmlElement(ElementName = "EntityPosition", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EntityPosition { get; set; }

		[XmlElement(ElementName = "IsDefaultBillingContact", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsDefaultBillingContact { get; set; }

		[XmlElement(ElementName = "IsDefaultCollectionContact", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsDefaultCollectionContact { get; set; }

		[XmlElement(ElementName = "Rate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Rate { get; set; }

		[XmlElement(ElementName = "MaxHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxHours { get; set; }

		[XmlElement(ElementName = "MaxBillAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxBillAmt { get; set; }

		[XmlElement(ElementName = "CurrDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CurrDate { get; set; }

		[XmlElement(ElementName = "IsActive", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsActive { get; set; }

		[XmlElement(ElementName = "RefRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RefRate { get; set; }

		[XmlElement(ElementName = "RateGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateGroup { get; set; }

		[XmlElement(ElementName = "MaxFees", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxFees { get; set; }

		[XmlElement(ElementName = "MaxHCo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxHCo { get; set; }

		[XmlElement(ElementName = "MaxSCo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxSCo { get; set; }

		[XmlElement(ElementName = "MaxInt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxInt { get; set; }

		[XmlElement(ElementName = "MaxBOA", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxBOA { get; set; }

		[XmlElement(ElementName = "MaxTax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxTax { get; set; }

		[XmlElement(ElementName = "MaxOth", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxOth { get; set; }

		[XmlElement(ElementName = "IsEnforceMaximums", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsEnforceMaximums { get; set; }

		[XmlElement(ElementName = "RateExcList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateExcList { get; set; }

		[XmlElement(ElementName = "IsMaximum", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsMaximum { get; set; }

		[XmlElement(ElementName = "IsMatchCurr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsMatchCurr { get; set; }

		[XmlElement(ElementName = "OverrideDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string OverrideDate { get; set; }

		[XmlElement(ElementName = "IsSkipClientExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsSkipClientExc { get; set; }

		[XmlElement(ElementName = "CostType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CostType { get; set; }

		[XmlElement(ElementName = "MultiDimensionOrdinal", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MultiDimensionOrdinal { get; set; }

		[XmlElement(ElementName = "IsStdRateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsStdRateExc { get; set; }

		[XmlElement(ElementName = "RateOverride", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateOverride { get; set; }

		[XmlElement(ElementName = "MaxRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaxRate { get; set; }

		[XmlElement(ElementName = "CurrencyDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CurrencyDate { get; set; }

		[XmlElement(ElementName = "Title", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Title { get; set; }

		[XmlElement(ElementName = "RateClass", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateClass { get; set; }

		[XmlElement(ElementName = "Startdate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Startdate { get; set; }

		[XmlElement(ElementName = "OverridePercent", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string OverridePercent { get; set; }

		[XmlElement(ElementName = "RoundingMethod", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RoundingMethod { get; set; }

		[XmlElement(ElementName = "RateYearRangeMin", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateYearRangeMin { get; set; }

		[XmlElement(ElementName = "RateYearRangeMax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateYearRangeMax { get; set; }

		[XmlElement(ElementName = "MatterRateMax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MatterRateMax { get; set; }

		[XmlElement(ElementName = "MatterRateMin", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MatterRateMin { get; set; }

		[XmlElement(ElementName = "DeviationAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DeviationAmount { get; set; }

		[XmlElement(ElementName = "EndDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EndDate { get; set; }

		[XmlElement(ElementName = "Matter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Matter { get; set; }

		[XmlElement(ElementName = "WestTaxomony", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WestTaxomony { get; set; }

		[XmlElement(ElementName = "RateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RateExc { get; set; }

		[XmlElement(ElementName = "BillTemplateOption", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillTemplateOption { get; set; }

		[XmlElement(ElementName = "BillTemplateOptionValue", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillTemplateOptionValue { get; set; }

		[XmlElement(ElementName = "BillingGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingGroup { get; set; }

		[XmlElement(ElementName = "IsLead", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsLead { get; set; }

		[XmlElement(ElementName = "IsBillingMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsBillingMatter { get; set; }

		[XmlElement(ElementName = "Amount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Amount { get; set; }

		[XmlElement(ElementName = "ProfAdjustMethodList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProfAdjustMethodList { get; set; }

		[XmlElement(ElementName = "ProfAdjustType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProfAdjustType { get; set; }

		[XmlElement(ElementName = "IsIncludeOtherAdjustments", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsIncludeOtherAdjustments { get; set; }

		[XmlElement(ElementName = "BillAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillAmount { get; set; }

		[XmlElement(ElementName = "ProfEnd", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProfEnd { get; set; }

		[XmlElement(ElementName = "ProfStart", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProfStart { get; set; }

		[XmlElement(ElementName = "ChrgCardFilter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ChrgCardFilter { get; set; }

		[XmlElement(ElementName = "CostCardFilter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CostCardFilter { get; set; }

		[XmlElement(ElementName = "TimeCardFilter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeCardFilter { get; set; }

		[XmlElement(ElementName = "IsAdjustWithFilters", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsAdjustWithFilters { get; set; }

		[XmlElement(ElementName = "ChrgType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ChrgType { get; set; }

		[XmlElement(ElementName = "TimeType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeType { get; set; }

		[XmlElement(ElementName = "GLNatural", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLNatural { get; set; }

		[XmlElement(ElementName = "GLUnit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLUnit { get; set; }

		[XmlElement(ElementName = "GLOffice", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLOffice { get; set; }

		[XmlElement(ElementName = "GLTimekeeper", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLTimekeeper { get; set; }

		[XmlElement(ElementName = "GLDepartment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLDepartment { get; set; }

		[XmlElement(ElementName = "GLSection", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLSection { get; set; }

		[XmlElement(ElementName = "GLArrangement", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLArrangement { get; set; }

		[XmlElement(ElementName = "GLTitle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLTitle { get; set; }

		[XmlElement(ElementName = "GLPracticeGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLPracticeGroup { get; set; }

		[XmlElement(ElementName = "GLWorkType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string GLWorkType { get; set; }

		[XmlElement(ElementName = "MaskOverrideType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MaskOverrideType { get; set; }

		[XmlElement(ElementName = "ChrgTypePredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ChrgTypePredicate { get; set; }

		[XmlElement(ElementName = "ClientPredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientPredicate { get; set; }

		[XmlElement(ElementName = "CostTypePredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CostTypePredicate { get; set; }

		[XmlElement(ElementName = "MatterPredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MatterPredicate { get; set; }

		[XmlElement(ElementName = "TimekeeperPredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimekeeperPredicate { get; set; }

		[XmlElement(ElementName = "TimeTypePredicate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeTypePredicate { get; set; }

		[XmlElement(ElementName = "UsingChrgType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingChrgType { get; set; }

		[XmlElement(ElementName = "UsingClient", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingClient { get; set; }

		[XmlElement(ElementName = "UsingCostType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingCostType { get; set; }

		[XmlElement(ElementName = "UsingMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingMatter { get; set; }

		[XmlElement(ElementName = "UsingTimekeeper", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingTimekeeper { get; set; }

		[XmlElement(ElementName = "UsingTimeType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string UsingTimeType { get; set; }

		[XmlElement(ElementName = "SortString", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SortString { get; set; }

		[XmlElement(ElementName = "IsFee", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsFee { get; set; }

		[XmlElement(ElementName = "IsCost", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsCost { get; set; }

		[XmlElement(ElementName = "BudHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BudHours { get; set; }

		[XmlElement(ElementName = "BudAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BudAmount { get; set; }

		[XmlElement(ElementName = "Phase", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Phase { get; set; }

		[XmlElement(ElementName = "Task", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Task { get; set; }

		[XmlElement(ElementName = "Activity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Activity { get; set; }

		[XmlElement(ElementName = "BillHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillHours { get; set; }

		[XmlElement(ElementName = "CollAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CollAmount { get; set; }

		[XmlElement(ElementName = "CollHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CollHours { get; set; }

		[XmlElement(ElementName = "NBAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NBAmount { get; set; }

		[XmlElement(ElementName = "NBHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NBHours { get; set; }

		[XmlElement(ElementName = "WOffAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WOffAmount { get; set; }

		[XmlElement(ElementName = "WOffHours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string WOffHours { get; set; }

		[XmlElement(ElementName = "Activity2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Activity2 { get; set; }

		[XmlElement(ElementName = "Phase2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Phase2 { get; set; }

		[XmlElement(ElementName = "Task2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Task2 { get; set; }

		[XmlElement(ElementName = "FlatFeeAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FlatFeeAmount { get; set; }

		[XmlElement(ElementName = "MilestoneDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MilestoneDate { get; set; }

		[XmlElement(ElementName = "IsTimeEntry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsTimeEntry { get; set; }

		[XmlElement(ElementName = "IsCostEntry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsCostEntry { get; set; }

		[XmlElement(ElementName = "IsChargeEntry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsChargeEntry { get; set; }

		[XmlElement(ElementName = "IndustryGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IndustryGroup { get; set; }

		[XmlElement(ElementName = "PracticeTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PracticeTeam { get; set; }

		[XmlElement(ElementName = "SummarizeTo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SummarizeTo { get; set; }

		[XmlElement(ElementName = "IsDoNotSummarize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsDoNotSummarize { get; set; }

		[XmlElement(ElementName = "CmCaseNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmCaseNumber { get; set; }

		[XmlElement(ElementName = "DocketID", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DocketID { get; set; }

		[XmlElement(ElementName = "CmTimeZone", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmTimeZone { get; set; }

		[XmlElement(ElementName = "CmRuleset", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmRuleset { get; set; }

		[XmlElement(ElementName = "CmJurisdiction", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmJurisdiction { get; set; }

		[XmlElement(ElementName = "CmCaseCategory", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmCaseCategory { get; set; }

		[XmlElement(ElementName = "Alert", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Alert { get; set; }

		[XmlElement(ElementName = "RMSubmatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RMSubmatter { get; set; }

		[XmlElement(ElementName = "IsViewcase", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsViewcase { get; set; }

		[XmlElement(ElementName = "TkprsAffectClosedEvents", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TkprsAffectClosedEvents { get; set; }

		[XmlElement(ElementName = "TkprsAffectPastEvents", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TkprsAffectPastEvents { get; set; }

		[XmlElement(ElementName = "CmCaseStatusList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmCaseStatusList { get; set; }

		[XmlElement(ElementName = "StatusStartDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StatusStartDate { get; set; }

		[XmlElement(ElementName = "Comment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Comment { get; set; }

		[XmlElement(ElementName = "CmCaseTkprRoleList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmCaseTkprRoleList { get; set; }

		[XmlElement(ElementName = "IsCustom", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsCustom { get; set; }

		[XmlElement(ElementName = "Status", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Status { get; set; }

		[XmlElement(ElementName = "IsStatusDirty", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsStatusDirty { get; set; }

		[XmlElement(ElementName = "NxEndDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NxEndDate { get; set; }

		[XmlElement(ElementName = "CmTkprCaseStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmTkprCaseStatus { get; set; }

		[XmlElement(ElementName = "CmSubEventStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmSubEventStatus { get; set; }

		[XmlElement(ElementName = "AmtContested", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AmtContested { get; set; }

		[XmlElement(ElementName = "SettlementAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SettlementAmt { get; set; }

		[XmlElement(ElementName = "DueDiligence", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DueDiligence { get; set; }

		[XmlElement(ElementName = "ConvDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ConvDate { get; set; }

		[XmlElement(ElementName = "CmBkChList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmBkChList { get; set; }

		[XmlElement(ElementName = "AssetDescription", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AssetDescription { get; set; }

		[XmlElement(ElementName = "LossAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LossAmt { get; set; }

		[XmlElement(ElementName = "IsSubrogationAllowed", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsSubrogationAllowed { get; set; }

		[XmlElement(ElementName = "CmSettleRelList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmSettleRelList { get; set; }

		[XmlElement(ElementName = "PolicyNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PolicyNumber { get; set; }

		[XmlElement(ElementName = "AccidentLocation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AccidentLocation { get; set; }

		[XmlElement(ElementName = "AdjusterName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AdjusterName { get; set; }

		[XmlElement(ElementName = "InsuredEntity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string InsuredEntity { get; set; }

		[XmlElement(ElementName = "AdjusterEntity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AdjusterEntity { get; set; }

		[XmlElement(ElementName = "JudgeEntity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string JudgeEntity { get; set; }

		[XmlElement(ElementName = "SettledAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SettledAmt { get; set; }

		[XmlElement(ElementName = "AuthorizedAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AuthorizedAmt { get; set; }

		[XmlElement(ElementName = "FinalAmt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FinalAmt { get; set; }

		[XmlElement(ElementName = "IsJuryTrialRequired", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsJuryTrialRequired { get; set; }

		[XmlElement(ElementName = "CmDisposition", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmDisposition { get; set; }

		[XmlElement(ElementName = "ClosingNotes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClosingNotes { get; set; }

		[XmlElement(ElementName = "ClaimNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClaimNumber { get; set; }

		[XmlElement(ElementName = "LossDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LossDate { get; set; }

		[XmlElement(ElementName = "Entity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Entity { get; set; }

		[XmlElement(ElementName = "PropertyAddress", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PropertyAddress { get; set; }

		[XmlElement(ElementName = "ParcelID", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ParcelID { get; set; }

		[XmlElement(ElementName = "ZoningRestr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ZoningRestr { get; set; }

		[XmlElement(ElementName = "EnvConcerns", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EnvConcerns { get; set; }

		[XmlElement(ElementName = "Mark", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Mark { get; set; }

		[XmlElement(ElementName = "DesignType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DesignType { get; set; }

		[XmlElement(ElementName = "SerialNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string SerialNumber { get; set; }

		[XmlElement(ElementName = "RegNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RegNumber { get; set; }

		[XmlElement(ElementName = "FormNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FormNumber { get; set; }

		[XmlElement(ElementName = "Class", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Class { get; set; }

		[XmlElement(ElementName = "RegDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RegDate { get; set; }

		[XmlElement(ElementName = "NumberShares", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string NumberShares { get; set; }

		[XmlElement(ElementName = "PerShareValue", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PerShareValue { get; set; }

		[XmlElement(ElementName = "InventionTitle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string InventionTitle { get; set; }

		[XmlElement(ElementName = "PatentDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PatentDate { get; set; }

		[XmlElement(ElementName = "FilingDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FilingDate { get; set; }

		[XmlElement(ElementName = "ApplicationNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ApplicationNumber { get; set; }

		[XmlElement(ElementName = "Country", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Country { get; set; }

		[XmlElement(ElementName = "Medicare", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Medicare { get; set; }

		[XmlElement(ElementName = "Medicaid", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Medicaid { get; set; }

		[XmlElement(ElementName = "Pharmaceuticals", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Pharmaceuticals { get; set; }

		[XmlElement(ElementName = "LongTermCare", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LongTermCare { get; set; }

		[XmlElement(ElementName = "IsDiscrimination", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsDiscrimination { get; set; }

		[XmlElement(ElementName = "IsHarrassment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsHarrassment { get; set; }

		[XmlElement(ElementName = "IsBreachOfContract", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsBreachOfContract { get; set; }

		[XmlElement(ElementName = "DamagesCap", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DamagesCap { get; set; }

		[XmlElement(ElementName = "StatuteLimit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StatuteLimit { get; set; }

		[XmlElement(ElementName = "DamagesClaimed", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DamagesClaimed { get; set; }

		[XmlElement(ElementName = "CmStrictLiabNegList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CmStrictLiabNegList { get; set; }

		[XmlElement(ElementName = "ProdNameCat", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ProdNameCat { get; set; }

		[XmlElement(ElementName = "StateLocalIssue", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StateLocalIssue { get; set; }

		[XmlElement(ElementName = "FedIssue", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FedIssue { get; set; }

		[XmlElement(ElementName = "PersonalIssue", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PersonalIssue { get; set; }

		[XmlElement(ElementName = "BusIssues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BusIssues { get; set; }

		[XmlElement(ElementName = "Statute", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Statute { get; set; }

		[XmlElement(ElementName = "DateTime", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DateTime { get; set; }

		[XmlElement(ElementName = "Location", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Location { get; set; }

		[XmlElement(ElementName = "Assets", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Assets { get; set; }

		[XmlElement(ElementName = "PrenupDtls", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PrenupDtls { get; set; }

		[XmlElement(ElementName = "PlaceOfEmploy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PlaceOfEmploy { get; set; }

		[XmlElement(ElementName = "TaxReturnsPayStubs", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TaxReturnsPayStubs { get; set; }

		[XmlElement(ElementName = "ChildMentPhysIssues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ChildMentPhysIssues { get; set; }

		[XmlElement(ElementName = "ParentMentPhysIssues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ParentMentPhysIssues { get; set; }

		[XmlElement(ElementName = "IsLayer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsLayer { get; set; }

		[XmlElement(ElementName = "IsLayerAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsLayerAmount { get; set; }

		[XmlElement(ElementName = "IsTaxbyMultipayor", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsTaxbyMultipayor { get; set; }

		[XmlElement(ElementName = "IsMultipayorByRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsMultipayorByRate { get; set; }

		[XmlElement(ElementName = "EffDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EffDate { get; set; }

		[XmlElement(ElementName = "Payor", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Payor { get; set; }

		[XmlElement(ElementName = "PctFee", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctFee { get; set; }

		[XmlElement(ElementName = "PctHCo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctHCo { get; set; }

		[XmlElement(ElementName = "PctSCo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctSCo { get; set; }

		[XmlElement(ElementName = "PctTax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctTax { get; set; }

		[XmlElement(ElementName = "PctInt", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctInt { get; set; }

		[XmlElement(ElementName = "PctBOA", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctBOA { get; set; }

		[XmlElement(ElementName = "PctOth", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PctOth { get; set; }

		[XmlElement(ElementName = "StmtSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string StmtSite { get; set; }

		[XmlElement(ElementName = "ForAttentionOf", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ForAttentionOf { get; set; }

		[XmlElement(ElementName = "RefNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RefNumber { get; set; }

		[XmlElement(ElementName = "FeeRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FeeRate { get; set; }

		[XmlElement(ElementName = "Ordinal", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Ordinal { get; set; }

		[XmlElement(ElementName = "PayorAmountFrequency", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PayorAmountFrequency { get; set; }

		[XmlElement(ElementName = "Layer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Layer { get; set; }

		[XmlElement(ElementName = "LayerAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LayerAmount { get; set; }

		[XmlElement(ElementName = "LayerPercent", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LayerPercent { get; set; }

		[XmlElement(ElementName = "BankAcctTrust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BankAcctTrust { get; set; }

		[XmlElement(ElementName = "TaxArticle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TaxArticle { get; set; }

		[XmlElement(ElementName = "MattNoteType", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MattNoteType { get; set; }

		[XmlElement(ElementName = "DateEntered", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DateEntered { get; set; }

		[XmlElement(ElementName = "EntryUser", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EntryUser { get; set; }

		[XmlElement(ElementName = "Note", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Note { get; set; }

		[XmlElement(ElementName = "EBillValList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EBillValList { get; set; }

		[XmlElement(ElementName = "InvoiceSequence", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string InvoiceSequence { get; set; }

		[XmlElement(ElementName = "ClientFirmId", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientFirmId { get; set; }

		[XmlElement(ElementName = "ClientIDOverride", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientIDOverride { get; set; }

		[XmlElement(ElementName = "ConfigurationTag", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ConfigurationTag { get; set; }

		[XmlElement(ElementName = "LawFirmLocationId", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LawFirmLocationId { get; set; }

		[XmlElement(ElementName = "PurchaseOrderNumber", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PurchaseOrderNumber { get; set; }

		[XmlElement(ElementName = "ClientMatterName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientMatterName { get; set; }

		[XmlElement(ElementName = "DivisionName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DivisionName { get; set; }

		[XmlElement(ElementName = "DivisionOffice", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string DivisionOffice { get; set; }

		[XmlElement(ElementName = "LawFirmName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LawFirmName { get; set; }

		[XmlElement(ElementName = "MatterReferenceId", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string MatterReferenceId { get; set; }

		[XmlElement(ElementName = "ClaimRepName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClaimRepName { get; set; }

		[XmlElement(ElementName = "AccountTitle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AccountTitle { get; set; }

		[XmlElement(ElementName = "AFAClass", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AFAClass { get; set; }

		[XmlElement(ElementName = "BillingCycle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingCycle { get; set; }

		[XmlElement(ElementName = "BillingCycleIteration", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string BillingCycleIteration { get; set; }

		[XmlElement(ElementName = "ClientCompanyCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientCompanyCode { get; set; }

		[XmlElement(ElementName = "ClientContactFirstName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientContactFirstName { get; set; }

		[XmlElement(ElementName = "ClientContactLastName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ClientContactLastName { get; set; }

		[XmlElement(ElementName = "CompanyCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CompanyCode { get; set; }

		[XmlElement(ElementName = "EntityBilledCountry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string EntityBilledCountry { get; set; }

		[XmlElement(ElementName = "LawFirmTaxId", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string LawFirmTaxId { get; set; }

		[XmlElement(ElementName = "PaymentMethod", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PaymentMethod { get; set; }

		[XmlElement(ElementName = "RemitToAddressCity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemitToAddressCity { get; set; }

		[XmlElement(ElementName = "RemitToAddressCountry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemitToAddressCountry { get; set; }

		[XmlElement(ElementName = "RemitToAddressPostalCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemitToAddressPostalCode { get; set; }

		[XmlElement(ElementName = "RemitToAddressState", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemitToAddressState { get; set; }

		[XmlElement(ElementName = "RemitToAddressStreet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemitToAddressStreet { get; set; }

		[XmlElement(ElementName = "TimeTypeGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TimeTypeGroup { get; set; }

		[XmlElement(ElementName = "Is_Default", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Is_Default { get; set; }

		[XmlElement(ElementName = "InternalComment", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string InternalComment { get; set; }

		[XmlElement(ElementName = "AutoCreate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AutoCreate { get; set; }

		[XmlElement(ElementName = "FeeArrangeGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FeeArrangeGroup { get; set; }

		[XmlElement(ElementName = "AutoCreateStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string AutoCreateStatus { get; set; }

		[XmlElement(ElementName = "ReviewDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ReviewDate { get; set; }

		[XmlElement(ElementName = "TaxJurisdiction", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TaxJurisdiction { get; set; }

		[XmlElement(ElementName = "CountryCode", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string CountryCode { get; set; }

		[XmlElement(ElementName = "TemplateName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TemplateName { get; set; }

		[XmlElement(ElementName = "Printer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Printer { get; set; }

		[XmlElement(ElementName = "TemplateFormat", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string TemplateFormat { get; set; }

		[XmlElement(ElementName = "Copies", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Copies { get; set; }

		[XmlElement(ElementName = "Duplex", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Duplex { get; set; }

		[XmlElement(ElementName = "Orientation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Orientation { get; set; }

		[XmlElement(ElementName = "PrinterLocation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PrinterLocation { get; set; }

		[XmlElement(ElementName = "IsUseAlternatingTrays", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsUseAlternatingTrays { get; set; }

		[XmlElement(ElementName = "PrintToFileName", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PrintToFileName { get; set; }

		[XmlElement(ElementName = "PrinterXML", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string PrinterXML { get; set; }

		[XmlElement(ElementName = "IsBillNoPrint", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string IsBillNoPrint { get; set; }

		[XmlElement(ElementName = "ArrangementAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string ArrangementAmount { get; set; }

		[XmlElement(ElementName = "RemainingAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string RemainingAmount { get; set; }

		[XmlElement(ElementName = "FlatAmount", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string FlatAmount { get; set; }

		[XmlElement(ElementName = "Hours", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Hours { get; set; }

		[XmlElement(ElementName = "Offset", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Offset { get; set; }

		[XmlElement(ElementName = "Quantity", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public string Quantity { get; set; }
	}

	[XmlRoot(ElementName = "MattOrgTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattOrgTkpr
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class Edit
	{

		[XmlElement(ElementName = "MattOrgTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattOrgTkpr MattOrgTkpr { get; set; }

		[XmlElement(ElementName = "MattPrlfTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPrlfTkpr MattPrlfTkpr { get; set; }

		[XmlElement(ElementName = "MattSpvTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattSpvTkpr MattSpvTkpr { get; set; }

		[XmlElement(ElementName = "MattDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattDate MattDate { get; set; }

		[XmlElement(ElementName = "MattSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattSite MattSite { get; set; }

		[XmlElement(ElementName = "MattBillingContact", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattBillingContact MattBillingContact { get; set; }

		[XmlElement(ElementName = "MattRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattRate MattRate { get; set; }

		[XmlElement(ElementName = "RateExcDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcDet RateExcDet { get; set; }

		[XmlElement(ElementName = "RateExcClientList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcClientList RateExcClientList { get; set; }

		[XmlElement(ElementName = "RateExcMatterList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcMatterList RateExcMatterList { get; set; }

		[XmlElement(ElementName = "RateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExc RateExc { get; set; }

		[XmlElement(ElementName = "MattTaxonomy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTaxonomy MattTaxonomy { get; set; }

		[XmlElement(ElementName = "MatterRateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MatterRateExc MatterRateExc { get; set; }

		[XmlElement(ElementName = "MattTemplateOption", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTemplateOption MattTemplateOption { get; set; }

		[XmlElement(ElementName = "BillingGroupMatter1", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public BillingGroupMatter1 BillingGroupMatter1 { get; set; }

		[XmlElement(ElementName = "MattProfAdjust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattProfAdjust MattProfAdjust { get; set; }

		[XmlElement(ElementName = "MaskOverrideValues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MaskOverrideValues MaskOverrideValues { get; set; }

		[XmlElement(ElementName = "MattBudget", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattBudget MattBudget { get; set; }

		[XmlElement(ElementName = "MattFlatFee", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattFlatFee MattFlatFee { get; set; }

		[XmlElement(ElementName = "MattPhaseException", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPhaseException MattPhaseException { get; set; }

		[XmlElement(ElementName = "MattIndustryGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattIndustryGroup MattIndustryGroup { get; set; }

		[XmlElement(ElementName = "MattPracticeTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPracticeTeam MattPracticeTeam { get; set; }

		[XmlElement(ElementName = "MattCostTypeSummarize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattCostTypeSummarize MattCostTypeSummarize { get; set; }

		[XmlElement(ElementName = "CMCaseStatusHist", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CMCaseStatusHist CMCaseStatusHist { get; set; }

		[XmlElement(ElementName = "CmCaseTeamStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCaseTeamStatus CmCaseTeamStatus { get; set; }

		[XmlElement(ElementName = "CmCaseTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCaseTeam CmCaseTeam { get; set; }

		[XmlElement(ElementName = "CmLitigation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmLitigation CmLitigation { get; set; }

		[XmlElement(ElementName = "CmBankruptcy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmBankruptcy CmBankruptcy { get; set; }

		[XmlElement(ElementName = "CmClaimNos", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmClaimNos CmClaimNos { get; set; }

		[XmlElement(ElementName = "CmLossDates", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmLossDates CmLossDates { get; set; }

		[XmlElement(ElementName = "CmInsExperts", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmInsExperts CmInsExperts { get; set; }

		[XmlElement(ElementName = "CmInsurance", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmInsurance CmInsurance { get; set; }

		[XmlElement(ElementName = "CmRealEstate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmRealEstate CmRealEstate { get; set; }

		[XmlElement(ElementName = "CmTrademark", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmTrademark CmTrademark { get; set; }

		[XmlElement(ElementName = "CmSecurities", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmSecurities CmSecurities { get; set; }

		[XmlElement(ElementName = "CmPatentAppNum", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmPatentAppNum CmPatentAppNum { get; set; }

		[XmlElement(ElementName = "CmPatentCountry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmPatentCountry CmPatentCountry { get; set; }

		[XmlElement(ElementName = "CmPatent", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmPatent CmPatent { get; set; }

		[XmlElement(ElementName = "CmHealthLaw", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmHealthLaw CmHealthLaw { get; set; }

		[XmlElement(ElementName = "CmLaborEmploy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmLaborEmploy CmLaborEmploy { get; set; }

		[XmlElement(ElementName = "CmMedMal", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmMedMal CmMedMal { get; set; }

		[XmlElement(ElementName = "CmProductsLiability", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmProductsLiability CmProductsLiability { get; set; }

		[XmlElement(ElementName = "CmTax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmTax CmTax { get; set; }

		[XmlElement(ElementName = "CmCriminalLaw", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCriminalLaw CmCriminalLaw { get; set; }

		[XmlElement(ElementName = "CmDivorce", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmDivorce CmDivorce { get; set; }

		[XmlElement(ElementName = "CmChildSupport", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmChildSupport CmChildSupport { get; set; }

		[XmlElement(ElementName = "CmChildCustody", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmChildCustody CmChildCustody { get; set; }

		[XmlElement(ElementName = "CmCase", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCase CmCase { get; set; }

		[XmlElement(ElementName = "MattPayorDetail", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorDetail MattPayorDetail { get; set; }

		[XmlElement(ElementName = "MattPayorLayerDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorLayerDet MattPayorLayerDet { get; set; }

		[XmlElement(ElementName = "MattPayorLayer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorLayer MattPayorLayer { get; set; }

		[XmlElement(ElementName = "MattPayor", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayor MattPayor { get; set; }

		[XmlElement(ElementName = "MattTrust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTrust MattTrust { get; set; }

		[XmlElement(ElementName = "MattTaxArticle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTaxArticle MattTaxArticle { get; set; }

		[XmlElement(ElementName = "MattNote", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattNote MattNote { get; set; }

		[XmlElement(ElementName = "MattEBillValList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillValList MattEBillValList { get; set; }

		[XmlElement(ElementName = "MattEBillLSS", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillLSS MattEBillLSS { get; set; }

		[XmlElement(ElementName = "MattEBillLEDES", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillLEDES MattEBillLEDES { get; set; }

		[XmlElement(ElementName = "MatterAdditionalInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MatterAdditionalInfo MatterAdditionalInfo { get; set; }

		[XmlElement(ElementName = "TimeTypeGroupMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public TimeTypeGroupMatter TimeTypeGroupMatter { get; set; }

		[XmlElement(ElementName = "MattAltBillArrangeDateDet2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrangeDateDet2 MattAltBillArrangeDateDet2 { get; set; }

		[XmlElement(ElementName = "MattAltBillArrangeDate2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrangeDate2 MattAltBillArrangeDate2 { get; set; }

		[XmlElement(ElementName = "MattAltBillArrange2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrange2 MattAltBillArrange2 { get; set; }

		[XmlElement(ElementName = "Matter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Matter Matter { get; set; }
	}

	[XmlRoot(ElementName = "MattPrlfTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPrlfTkpr
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattSpvTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattSpvTkpr
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class Children
	{

		[XmlElement(ElementName = "MattOrgTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattOrgTkpr MattOrgTkpr { get; set; }

		[XmlElement(ElementName = "MattPrlfTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPrlfTkpr MattPrlfTkpr { get; set; }

		[XmlElement(ElementName = "MattSpvTkpr", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattSpvTkpr MattSpvTkpr { get; set; }

		[XmlElement(ElementName = "RateExcDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcDet RateExcDet { get; set; }

		[XmlElement(ElementName = "RateExcClientList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcClientList RateExcClientList { get; set; }

		[XmlElement(ElementName = "RateExcMatterList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExcMatterList RateExcMatterList { get; set; }

		[XmlElement(ElementName = "CmCaseTeamStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCaseTeamStatus CmCaseTeamStatus { get; set; }

		[XmlElement(ElementName = "CmClaimNos", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmClaimNos CmClaimNos { get; set; }

		[XmlElement(ElementName = "CmLossDates", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmLossDates CmLossDates { get; set; }

		[XmlElement(ElementName = "CmInsExperts", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmInsExperts CmInsExperts { get; set; }

		[XmlElement(ElementName = "CmPatentAppNum", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmPatentAppNum CmPatentAppNum { get; set; }

		[XmlElement(ElementName = "CmPatentCountry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmPatentCountry CmPatentCountry { get; set; }

		[XmlElement(ElementName = "CMCaseStatusHist", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CMCaseStatusHist CMCaseStatusHist { get; set; }

		[XmlElement(ElementName = "CmCaseTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCaseTeam CmCaseTeam { get; set; }

		[XmlElement(ElementName = "CmCaseDtl", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCaseDtl CmCaseDtl { get; set; }

		[XmlElement(ElementName = "MattPayorLayerDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorLayerDet MattPayorLayerDet { get; set; }

		[XmlElement(ElementName = "MattPayorDetail", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorDetail MattPayorDetail { get; set; }

		[XmlElement(ElementName = "MattPayorLayer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayorLayer MattPayorLayer { get; set; }

		[XmlElement(ElementName = "MattEBillLSS", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillLSS MattEBillLSS { get; set; }

		[XmlElement(ElementName = "MattEBillLEDES", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillLEDES MattEBillLEDES { get; set; }

		[XmlElement(ElementName = "MattAltBillArrangeDateDet2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrangeDateDet2 MattAltBillArrangeDateDet2 { get; set; }

		[XmlElement(ElementName = "MattAltBillArrangeDate2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrangeDate2 MattAltBillArrangeDate2 { get; set; }

		[XmlElement(ElementName = "MattDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattDate MattDate { get; set; }

		[XmlElement(ElementName = "MattSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattSite MattSite { get; set; }

		[XmlElement(ElementName = "MattBillingContact", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattBillingContact MattBillingContact { get; set; }

		[XmlElement(ElementName = "MattRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattRate MattRate { get; set; }

		[XmlElement(ElementName = "RateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public RateExc RateExc { get; set; }

		[XmlElement(ElementName = "MattTaxonomy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTaxonomy MattTaxonomy { get; set; }

		[XmlElement(ElementName = "MatterRateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MatterRateExc MatterRateExc { get; set; }

		[XmlElement(ElementName = "MattTemplateOption", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTemplateOption MattTemplateOption { get; set; }

		[XmlElement(ElementName = "BillingGroupMatter1", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public BillingGroupMatter1 BillingGroupMatter1 { get; set; }

		[XmlElement(ElementName = "MattProfAdjust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattProfAdjust MattProfAdjust { get; set; }

		[XmlElement(ElementName = "MaskOverrideValues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MaskOverrideValues MaskOverrideValues { get; set; }

		[XmlElement(ElementName = "MattBudget", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattBudget MattBudget { get; set; }

		[XmlElement(ElementName = "MattFlatFee", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattFlatFee MattFlatFee { get; set; }

		[XmlElement(ElementName = "MattPhaseException", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPhaseException MattPhaseException { get; set; }

		[XmlElement(ElementName = "MattIndustryGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattIndustryGroup MattIndustryGroup { get; set; }

		[XmlElement(ElementName = "MattPracticeTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPracticeTeam MattPracticeTeam { get; set; }

		[XmlElement(ElementName = "MattCostTypeSummarize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattCostTypeSummarize MattCostTypeSummarize { get; set; }

		[XmlElement(ElementName = "CmCase", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public CmCase CmCase { get; set; }

		[XmlElement(ElementName = "MattPayor", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattPayor MattPayor { get; set; }

		[XmlElement(ElementName = "MattTrust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTrust MattTrust { get; set; }

		[XmlElement(ElementName = "MattTaxArticle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattTaxArticle MattTaxArticle { get; set; }

		[XmlElement(ElementName = "MattNote", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattNote MattNote { get; set; }

		[XmlElement(ElementName = "MattEBillValList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattEBillValList MattEBillValList { get; set; }

		[XmlElement(ElementName = "MatterAdditionalInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MatterAdditionalInfo MatterAdditionalInfo { get; set; }

		[XmlElement(ElementName = "TimeTypeGroupMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public TimeTypeGroupMatter TimeTypeGroupMatter { get; set; }

		[XmlElement(ElementName = "MattAltBillArrange2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public MattAltBillArrange2 MattAltBillArrange2 { get; set; }
	}

	[XmlRoot(ElementName = "MattDate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattDate
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattSite", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattSite
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattBillingContact", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattBillingContact
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattRate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattRate
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "RateExcDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class RateExcDet
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "RateExcClientList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class RateExcClientList
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "RateExcMatterList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class RateExcMatterList
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "RateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class RateExc
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattTaxonomy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattTaxonomy
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MatterRateExc", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MatterRateExc
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattTemplateOption", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattTemplateOption
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "BillingGroupMatter1", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class BillingGroupMatter1
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattProfAdjust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattProfAdjust
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MaskOverrideValues", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MaskOverrideValues
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattBudget", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattBudget
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattFlatFee", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattFlatFee
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPhaseException", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPhaseException
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattIndustryGroup", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattIndustryGroup
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPracticeTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPracticeTeam
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattCostTypeSummarize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattCostTypeSummarize
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CMCaseStatusHist", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CMCaseStatusHist
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmCaseTeamStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmCaseTeamStatus
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmCaseTeam", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmCaseTeam
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmLitigation", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmLitigation
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmBankruptcy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmBankruptcy
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmClaimNos", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmClaimNos
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmLossDates", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmLossDates
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmInsExperts", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmInsExperts
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmInsurance", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmInsurance
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmRealEstate", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmRealEstate
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmTrademark", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmTrademark
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmSecurities", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmSecurities
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmPatentAppNum", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmPatentAppNum
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmPatentCountry", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmPatentCountry
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmPatent", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmPatent
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmHealthLaw", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmHealthLaw
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmLaborEmploy", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmLaborEmploy
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmMedMal", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmMedMal
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmProductsLiability", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmProductsLiability
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmTax", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmTax
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmCriminalLaw", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmCriminalLaw
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmDivorce", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmDivorce
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmChildSupport", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmChildSupport
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmChildCustody", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmChildCustody
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "CmCaseDtl", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmCaseDtl
	{

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public List<Edit> Edit { get; set; }
	}

	[XmlRoot(ElementName = "CmCase", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class CmCase
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPayorDetail", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPayorDetail
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPayorLayerDet", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPayorLayerDet
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPayorLayer", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPayorLayer
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattPayor", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattPayor
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattTrust", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattTrust
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattTaxArticle", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattTaxArticle
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattNote", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattNote
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattEBillValList", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattEBillValList
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattEBillLSS", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattEBillLSS
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattEBillLEDES", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattEBillLEDES
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MatterAdditionalInfo", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MatterAdditionalInfo
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "TimeTypeGroupMatter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class TimeTypeGroupMatter
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattAltBillArrangeDateDet2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattAltBillArrangeDateDet2
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattAltBillArrangeDate2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattAltBillArrangeDate2
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "MattAltBillArrange2", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class MattAltBillArrange2
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }
	}

	[XmlRoot(ElementName = "Matter", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class Matter
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Children Children { get; set; }

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public string KeyValue { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
	public class Initialize
	{

		[XmlElement(ElementName = "Edit", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Edit Edit { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Matter_Srv", Namespace = "http://elite.com/schemas/transaction/process/write/Matter_Srv")]
	public class MatterSrv
	{

		[XmlElement(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/Matter")]
		public Initialize Initialize { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
