using System.Xml;
using System.Xml.XPath;

namespace GrapeCity.ActiveReports.Samples.XmlDataSource
{
	// Provides the data used in the sample.
	internal sealed class DataLayer
	{
		public XmlReader CreateReader()
		{
			var txtReader = new XmlTextReader(@"..\..\MyXmlDB.xml");
			return txtReader;
		}

		public IXPathNavigable CreateDocument()
		{
			var doc = new XPathDocument(@"..\..\MyXmlDB.xml");
			return doc;
		}
	}
}
