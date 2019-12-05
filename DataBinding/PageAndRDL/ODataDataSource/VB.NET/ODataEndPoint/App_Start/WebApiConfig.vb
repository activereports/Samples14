Imports System.Web.Http
Imports System.Web.OData.Batch
Imports System.Web.OData.Builder
Imports System.Web.OData.Extensions

Public Module WebApiConfig
	Public Sub Register(ByVal config As HttpConfiguration)
		' Web API configuration and services

		' Web API routes
		config.MapHttpAttributeRoutes()

		config.Routes.MapHttpRoute(
			name:="DefaultApi",
			routeTemplate:="api/{controller}/{id}",
			defaults:=New With {.id = RouteParameter.Optional}
		)

		Dim builder As ODataModelBuilder = New ODataConventionModelBuilder()
		builder.EntitySet(Of Customer)("Customers")
		builder.EntitySet(Of Movie)("Movies")
		config.MapODataServiceRoute("odata", Nothing, builder.GetEdmModel(), New DefaultODataBatchHandler(GlobalConfiguration.DefaultServer))

		config.EnsureInitialized()

	End Sub
End Module
