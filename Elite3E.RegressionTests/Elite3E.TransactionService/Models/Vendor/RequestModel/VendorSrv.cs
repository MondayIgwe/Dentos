using System.Xml.Serialization;

namespace Elite3E.SoapServices.Models.Vendor.RequestModel
{
	[XmlRoot(ElementName = "Vendor_Srv", Namespace = "http://elite.com/schemas/transaction/process/write/Vendor_Srv")]
	public class VendorSrv
	{

		[XmlElement(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public Initialize Initialize { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Initialize", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
	public class Initialize
	{

		[XmlElement(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public Add Add { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Add", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
	public class Add
	{

		[XmlElement(ElementName = "Vendor", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public Vendor Vendor { get; set; }
	}

	[XmlRoot(ElementName = "Vendor", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
	public class Vendor
	{

		[XmlElement(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public Attributes Attributes { get; set; }
	}
	[XmlRoot(ElementName = "Attributes", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
	public class Attributes
	{

		[XmlElement(ElementName = "VendorNum", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string VendorNum { get; set; }

		[XmlElement(ElementName = "Entity", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string Entity { get; set; }

		[XmlElement(ElementName = "Name", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string Name { get; set; }

		[XmlElement(ElementName = "RelatedVendor", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string RelatedVendor { get; set; }

		[XmlElement(ElementName = "SortName", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string SortName { get; set; }

		[XmlElement(ElementName = "Site", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string Site { get; set; }

		[XmlElement(ElementName = "VendorStatus", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string VendorStatus { get; set; }

		[XmlElement(ElementName = "IsConfidential", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public int IsConfidential { get; set; }

		[XmlElement(ElementName = "IsOneTime", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public int IsOneTime { get; set; }

		[XmlElement(ElementName = "Site1099", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string Site1099 { get; set; }

		[XmlElement(ElementName = "TaxNum", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string TaxNum { get; set; }

		[XmlElement(ElementName = "IsAutoNumbering", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public int IsAutoNumbering { get; set; }

		[XmlElement(ElementName = "WhichSite", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string WhichSite { get; set; }

		[XmlElement(ElementName = "VendorType", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string VendorType { get; set; }

		[XmlElement(ElementName = "VendorCategory", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string VendorCategory { get; set; }

		[XmlElement(ElementName = "AltNum", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string AltNum { get; set; }

		[XmlElement(ElementName = "IsAutoNumAfterSave", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public int IsAutoNumAfterSave { get; set; }

		[XmlElement(ElementName = "GlobalVendor_ccc", Namespace = "http://elite.com/schemas/transaction/object/write/Vendor")]
		public string GlobalVendor { get; set; }
    }



}
