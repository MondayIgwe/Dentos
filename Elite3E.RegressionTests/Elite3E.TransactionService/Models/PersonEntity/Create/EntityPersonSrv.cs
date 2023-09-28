using System.Xml.Serialization;

namespace Elite3E.SoapServices.Models.PersonEntity.Create
{

	[XmlRoot(ElementName = "EntityPerson_Srv", Namespace = "http://elite.com/schemas/transaction/process/write/EntityPerson_Srv")]
	public class EntityPersonSrv
	{

		[XmlElement(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Initialize Initialize { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Add
	{

		[XmlElement(ElementName = "EntityUse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public EntityUse EntityUse { get; set; }

		[XmlElement(ElementName = "Site_Phone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SitePhone SitePhone { get; set; }

		[XmlElement(ElementName = "Site_URL", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SiteURL SiteURL { get; set; }

		[XmlElement(ElementName = "Site_EMail", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SiteEMail SiteEMail { get; set; }

		[XmlElement(ElementName = "Site", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Site Site { get; set; }

		[XmlElement(ElementName = "RelatedEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public RelatedEntity RelatedEntity { get; set; }

		[XmlElement(ElementName = "Relate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Relate Relate { get; set; }

		[XmlElement(ElementName = "RelateInverse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public RelateInverse RelateInverse { get; set; }

		[XmlElement(ElementName = "EntAltNamePerson", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public EntAltNamePerson EntAltNamePerson { get; set; }

		[XmlElement(ElementName = "EntityPerson", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public EntityPerson EntityPerson { get; set; }
	}

	[XmlRoot(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Children
	{

		[XmlElement(ElementName = "Site_Phone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SitePhone SitePhone { get; set; }

		[XmlElement(ElementName = "Site_URL", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SiteURL SiteURL { get; set; }

		[XmlElement(ElementName = "Site_EMail", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public SiteEMail SiteEMail { get; set; }

		[XmlElement(ElementName = "Site", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Site Site { get; set; }

		[XmlElement(ElementName = "RelatedEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public RelatedEntity RelatedEntity { get; set; }

		[XmlElement(ElementName = "EntityUse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public EntityUse EntityUse { get; set; }

		[XmlElement(ElementName = "Relate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Relate Relate { get; set; }

		[XmlElement(ElementName = "RelateInverse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public RelateInverse RelateInverse { get; set; }

		[XmlElement(ElementName = "EntAltNamePerson", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public EntAltNamePerson EntAltNamePerson { get; set; }
	}


	[XmlRoot(ElementName = "EntAltNamePerson", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class EntAltNamePerson
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "EntityPerson", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class EntityPerson
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Children Children { get; set; }
	}

	[XmlRoot(ElementName = "EntityUse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class EntityUse
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Initialize
	{

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Relate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Relate
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Children Children { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "RelatedEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class RelatedEntity
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "RelateInverse", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class RelateInverse
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Site", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Site
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Children", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Children Children { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Site_EMail", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class SiteEMail
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Site_Phone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class SitePhone
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Site_URL", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class SiteURL
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Attributes Attributes { get; set; }

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public Add Add { get; set; }
	}

	[XmlRoot(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
	public class Attributes
	{

		[XmlElement(ElementName = "DisplayName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string DisplayName { get; set; }

		[XmlElement(ElementName = "EntityType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string EntityType { get; set; }

		[XmlElement(ElementName = "Comment", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Comment { get; set; }

		[XmlElement(ElementName = "TaxID", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string TaxID { get; set; }

		[XmlElement(ElementName = "SyncID", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string SyncID { get; set; }

		[XmlElement(ElementName = "EntitySanction", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string EntitySanction { get; set; }

		[XmlElement(ElementName = "AltNum", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AltNum { get; set; }

		[XmlElement(ElementName = "LoadSource", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string LoadSource { get; set; }

		[XmlElement(ElementName = "LoadGroup", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string LoadGroup { get; set; }

		[XmlElement(ElementName = "LoadNumber", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string LoadNumber { get; set; }

		[XmlElement(ElementName = "IsChangeAll", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsChangeAll { get; set; }

		[XmlElement(ElementName = "CalledFrom", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string CalledFrom { get; set; }

		[XmlElement(ElementName = "KeyID", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string KeyID { get; set; }

		[XmlElement(ElementName = "IsMerged", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsMerged { get; set; }

		[XmlElement(ElementName = "NewEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string NewEntity { get; set; }

		[XmlElement(ElementName = "FirstName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string FirstName { get; set; }

		[XmlElement(ElementName = "MiddleName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string MiddleName { get; set; }

		[XmlElement(ElementName = "LastName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string LastName { get; set; }

		[XmlElement(ElementName = "GoesBy", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string GoesBy { get; set; }

		[XmlElement(ElementName = "NameFormat", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string NameFormat { get; set; }

		[XmlElement(ElementName = "BirthDate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string BirthDate { get; set; }

		[XmlElement(ElementName = "Gender", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Gender { get; set; }

		[XmlElement(ElementName = "SSNumber", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string SSNumber { get; set; }

		[XmlElement(ElementName = "PersonType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string PersonType { get; set; }

		[XmlElement(ElementName = "FormattedName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string FormattedName { get; set; }

		[XmlElement(ElementName = "Prefix", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Prefix { get; set; }

		[XmlElement(ElementName = "Suffix", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Suffix { get; set; }

		[XmlElement(ElementName = "EntityUseID", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string EntityUseID { get; set; }

		[XmlElement(ElementName = "ArchetypeCode", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string ArchetypeCode { get; set; }

		[XmlElement(ElementName = "Number", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Number { get; set; }

		[XmlElement(ElementName = "ParentIdx", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string ParentIdx { get; set; }

		[XmlElement(ElementName = "RelType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string RelType { get; set; }

		[XmlElement(ElementName = "ObjEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string ObjEntity { get; set; }

		[XmlElement(ElementName = "RelPercent", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string RelPercent { get; set; }

		[XmlElement(ElementName = "Description", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Description { get; set; }

		[XmlElement(ElementName = "RelTitle", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string RelTitle { get; set; }

		[XmlElement(ElementName = "RelDepartment", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string RelDepartment { get; set; }

		[XmlElement(ElementName = "StartDate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string StartDate { get; set; }

		[XmlElement(ElementName = "FinishDate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string FinishDate { get; set; }

		[XmlElement(ElementName = "IsValidate", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsValidate { get; set; }

		[XmlElement(ElementName = "IsOrganization", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsOrganization { get; set; }

		[XmlElement(ElementName = "IsDefault", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsDefault { get; set; }

		[XmlElement(ElementName = "Address", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Address { get; set; }

		[XmlElement(ElementName = "SiteType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string SiteType { get; set; }

		[XmlElement(ElementName = "Attention", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Attention { get; set; }

		[XmlElement(ElementName = "MailStop", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string MailStop { get; set; }

		[XmlElement(ElementName = "SortString", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string SortString { get; set; }

		[XmlElement(ElementName = "Language", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Language { get; set; }

		[XmlElement(ElementName = "FormatCode", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string FormatCode { get; set; }

		[XmlElement(ElementName = "OrgName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string OrgName { get; set; }

		[XmlElement(ElementName = "Street", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Street { get; set; }

		[XmlElement(ElementName = "City", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string City { get; set; }

		[XmlElement(ElementName = "ZipCode", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string ZipCode { get; set; }

		[XmlElement(ElementName = "County", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string County { get; set; }

		[XmlElement(ElementName = "IsUpdateAllSites", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsUpdateAllSites { get; set; }

		[XmlElement(ElementName = "FormattedString", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string FormattedString { get; set; }

		[XmlElement(ElementName = "AddrDescription", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AddrDescription { get; set; }

		[XmlElement(ElementName = "Country", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Country { get; set; }

		[XmlElement(ElementName = "TZone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string TZone { get; set; }

		[XmlElement(ElementName = "AddrSiteIdx", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AddrSiteIdx { get; set; }

		[XmlElement(ElementName = "Additional1", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Additional1 { get; set; }

		[XmlElement(ElementName = "Additional2", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Additional2 { get; set; }

		[XmlElement(ElementName = "Additional3", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Additional3 { get; set; }

		[XmlElement(ElementName = "Additional4", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Additional4 { get; set; }

		[XmlElement(ElementName = "State", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string State { get; set; }

		[XmlElement(ElementName = "ZipCodeLookup", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string ZipCodeLookup { get; set; }

		[XmlElement(ElementName = "Phone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Phone { get; set; }

		[XmlElement(ElementName = "Extension", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Extension { get; set; }

		[XmlElement(ElementName = "IsPrimary", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsPrimary { get; set; }

		[XmlElement(ElementName = "PhoneType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string PhoneType { get; set; }

		[XmlElement(ElementName = "PhoneFormat", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string PhoneFormat { get; set; }

		[XmlElement(ElementName = "CountryCode", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string CountryCode { get; set; }

		[XmlElement(ElementName = "AreaCode", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AreaCode { get; set; }

		[XmlElement(ElementName = "IsNewPhone", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsNewPhone { get; set; }

		[XmlElement(ElementName = "URL", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string URL { get; set; }

		[XmlElement(ElementName = "URLType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string URLType { get; set; }

		[XmlElement(ElementName = "EmailAddr", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string EmailAddr { get; set; }

		[XmlElement(ElementName = "EmailType", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string EmailType { get; set; }

		[XmlElement(ElementName = "Entity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string Entity { get; set; }

		[XmlElement(ElementName = "RelTypeDescription", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string RelTypeDescription { get; set; }

		[XmlElement(ElementName = "InvEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string InvEntity { get; set; }

		[XmlElement(ElementName = "IsSubject", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string IsSubject { get; set; }

		[XmlElement(ElementName = "SbjEntity", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string SbjEntity { get; set; }

		[XmlElement(ElementName = "AltName", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AltName { get; set; }

		[XmlElement(ElementName = "AltFirst", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AltFirst { get; set; }

		[XmlElement(ElementName = "AltMiddle", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AltMiddle { get; set; }

		[XmlElement(ElementName = "AltLast", Namespace = "http://elite.com/schemas/transaction/object/write/EntityPerson")]
		public string AltLast { get; set; }
	}
}
