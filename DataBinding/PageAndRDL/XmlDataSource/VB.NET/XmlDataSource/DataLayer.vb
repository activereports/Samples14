Imports System.Xml
Imports System.Xml.XPath

' Provides the data used in the sample.
Friend NotInheritable Class DataLayer

	Public Function CreateReader() As XmlReader
		Dim txtReader As New XmlTextReader("..\..\MyXmlDB.xml")
		Return txtReader
	End Function

	Public Function CreateDocument() As IXPathNavigable
		Dim doc As New XPathDocument("..\..\MyXmlDB.xml")
		Return doc
	End Function

End Class
