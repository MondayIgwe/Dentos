using System.Xml.Serialization;

namespace Elite3E.SoapServices.Models.Vendor.ResponseModel
{
	[XmlRoot(ElementName = "ProcessExecutionResults", Namespace = "")]
	public class ProcessExecutionResults
	{

		[XmlElement(ElementName = "Keys", Namespace = "")]
		public Keys Keys { get; set; }

		[XmlElement(ElementName = "MESSAGE", Namespace = "")]
		public string MESSAGE { get; set; }

		[XmlAttribute(AttributeName = "Process", Namespace = "")]
		public string Process { get; set; }

		[XmlAttribute(AttributeName = "Result", Namespace = "")]
		public string Result { get; set; }

		[XmlAttribute(AttributeName = "OutputId", Namespace = "")]
		public string OutputId { get; set; }

		[XmlAttribute(AttributeName = "Records", Namespace = "")]
		public int Records { get; set; }

		[XmlAttribute(AttributeName = "User", Namespace = "")]
		public string User { get; set; }

		[XmlAttribute(AttributeName = "Name", Namespace = "")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "ProcessItemId", Namespace = "")]
		public string ProcessItemId { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Vendor", Namespace = "")]
	public class Vendor
	{

		[XmlAttribute(AttributeName = "KeyValue", Namespace = "")]
		public int KeyValue { get; set; }
	}

	[XmlRoot(ElementName = "Keys", Namespace = "")]
	public class Keys
	{

		[XmlElement(ElementName = "Vendor", Namespace = "")]
		public Vendor Vendor { get; set; }
	}

}
