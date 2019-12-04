Imports System.Web.Services
Imports System.IO
Imports System.Web.Script.Services

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ComponentModel.ToolboxItem(False)> _
<Script.Services.ScriptService()> _
Public Class WebService
	Inherits System.Web.Services.WebService

	<WebMethod()>
	<ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
	Public Function GetJson() As String
		Return My.Resources.Resource.customers
	End Function

End Class
