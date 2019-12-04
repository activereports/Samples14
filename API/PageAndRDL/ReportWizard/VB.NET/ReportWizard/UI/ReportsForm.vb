Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports GrapeCity.ActiveReports.Samples.ReportWizard.MetaData
Imports GrapeCity.ActiveReports.Samples.ReportWizard.UI.WizardSteps
Namespace UI
    Partial Public Class ReportsForm
        Inherits Form
        Private Shared availableReports As List(Of ReportMetaData)
        Public Shared ReadOnly Property AvailableReportTemplates() As IEnumerable(Of ReportMetaData)
            Get
                Return availableReports

            End Get
        End Property
        Private Shared state As ReportWizardState
        Private reportDefinition As PageReport
        Public Sub New()
            InitializeComponent()
        End Sub
        'Add DropDownItems to ToolStripDropDownItem.


        Private Shared Function SaveReportToStream(ByVal def As PageReport) As Stream
            Dim stream As New MemoryStream()
            Using writer As XmlWriter = XmlWriter.Create(stream)
                If def IsNot Nothing Then
                    def.Save(writer)
                End If
            End Using
            stream.Position = 0
            Return stream
        End Function

        Private Shared Sub SetupTemplates()
            availableReports = New List(Of ReportMetaData)()
            Dim doc As New XmlDocument()
            doc.Load("Reports.xml")
            For Each reportNode As XmlNode In doc.SelectNodes("//reports/report")
                availableReports.Add(LoadReportMetaData(reportNode))
            Next
        End Sub
        Private Shared Function LoadReportMetaData(ByVal node As XmlNode) As ReportMetaData
            Dim data As New ReportMetaData()
            data.Title = node.Attributes("title").Value
            data.MasterReportFile = node.Attributes("filename").Value
            Dim fields As XmlNodeList = node.SelectNodes("fields/field")
            For Each fieldNode As XmlNode In fields
                Dim field As FieldMetaData = LoadFieldMetaData(fieldNode)
                data.Fields.Add(field.Name, field)
            Next
            Return data
        End Function
        Private Shared Function LoadFieldMetaData(ByVal node As XmlNode) As FieldMetaData
            Dim data As New FieldMetaData()
            data.Name = node.Attributes("name").Value
            data.Title = node.Attributes("title").Value
            data.PreferredWidth = node.Attributes("width").Value
            data.IsNumeric = node.Attributes("datatype").Value = "number"
            Dim attr As XmlAttribute = node.Attributes("format")
            If Not (attr) Is Nothing Then
                data.FormatString = attr.Value
            End If
            attr = node.Attributes("summarizable")
            If Not (attr) Is Nothing Then
                data.AllowTotaling = Convert.ToBoolean(attr.Value)
            End If
            attr = node.Attributes("summaryFunction")
            If Not (attr) Is Nothing Then
                data.SummaryFunction = attr.Value
            End If
            Return data
        End Function
        Private Shared Function CreateWizard() As WizardDialog
            state = New ReportWizardState()
            state.AvailableMasterReports.AddRange(AvailableReportTemplates)
            Dim dlg As New WizardDialog(state)
            dlg.Steps.Add(New SelectMasterReport())
            dlg.Steps.Add(New SelectGroupingFields())
            dlg.Steps.Add(New SelectOutputFields())
            dlg.Steps.Add(New SelectSummaryOptions())
            Return dlg
        End Function

        Private Sub ReportsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            SetupTemplates()
            Dim wizard As WizardDialog = CreateWizard()
            Dim res As DialogResult = wizard.ShowDialog()
            If res.Equals(DialogResult.OK) Then
                Me.reportDefinition = LayoutBuilder.BuildReport(state)
                Using stream As Stream = SaveReportToStream(reportDefinition)
                    Viewer1.LoadDocument(stream, GrapeCity.Viewer.Common.DocumentFormat.Rdlx) '
                End Using
            Else
                BeginInvoke(New MethodInvoker(AddressOf Me.Close))
            End If

        End Sub
    End Class
End Namespace
