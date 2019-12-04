using System;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Design.DdrDesigner.Services.UI.DesignerActionUI;

namespace GrapeCity.ActiveReports.Calendar.Design.SmartPanels.Calendar
{
	/// <summary>
	/// Defines the smart panel builder for a calendar.
	/// </summary>
	internal static class CalendarSmartPanelBuilder
	{
		/// <summary>
		/// Creates and returns calendar data page.
		/// </summary>
		public static IWizardNavigator CreateDataPage(CalendarDesigner designer)
		{
			IServiceProvider provider = designer.ReportItem.Site;
			IWizardNavigator group = WizardStep.CreateaWizardNavigator(null, null, provider);
			group.Steps.Add(new WizardStep(group, new CalendarDataPage(designer),
				Resources.CalendarSmartPanelDataPageTitle, Resources.CalendarSmartPanelDataTitle, Resources.Data_16));
			return group;
		}

		/// <summary>
		/// Creates a new calendar home page/main wizard.
		/// </summary>
		public static IWizardNavigator CreateHomePageWizard(CalendarDesigner designer)
		{
			IServiceProvider provider = designer.ReportItem.Site;
			IWizardNavigator group = WizardStep.CreateaWizardNavigator(null, null, provider);
			group.Steps.Add(new WizardStep(group, new CalendarGeneralPage(designer),
				Resources.CalendarSmartPanelGeneralPageTitle, Resources.CalendarSmartPanelGeneralTitle,
				Resources.General_16));
			group.Steps.Add(new WizardStep(group, new CalendarDataPage(designer),
				Resources.CalendarSmartPanelDataPageTitle, Resources.CalendarSmartPanelDataTitle,
				Resources.Data_16));
			group.Steps.Add(new WizardStep(group, new CalendarDetailGroupPage(designer),
				Resources.CalendarSmartPanelDetailGroupingPageTitle, Resources.CalendarSmartPanelDetailGroupingTitle,
				Resources.General_16));
			group.Steps.Add(new WizardStep(group, new CalendarFiltersPage(designer),
				Resources.CalendarSmartPanelFiltersPageTitle, Resources.CalendarSmartPanelFiltersTitle,
				Resources.Filters_16));
			group.Steps.Add(new WizardStep(group, new CalendarEventAppearancePage(designer),
				Resources.CalendarSmartPanelEventAppearancePageTitle, Resources.CalendarSmartPanelEventAppearanceTitle,
				Resources.EventAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarAppearancePage(designer),
				Resources.CalendarSmartPanelAppearancePageTitle, Resources.CalendarSmartPanelAppearanceTitle,
				Resources.CalendarAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarNavigationPage(designer),
				Resources.CalendarSmartPanelNavigationPageTitle, Resources.CalendarSmartPanelNavigationTitle,
				Resources.Navigation_16));
			group.Steps.Add(new WizardStep(group, new CalendarDataOutputPage(designer),
				Resources.CalendarSmartPanelDataOutputPageTitle, Resources.CalendarSmartPanelDataOutputTitle,
				Resources.Data_Output_16));
			return group;
		}

		/// <summary>
		/// Creates and returns the child page of calendar appearance page.
		/// </summary>
		public static IWizardNavigator CreateCalendarAppearanceChildPage(CalendarDesigner designer)
		{
			IWizardNavigator group = WizardStep.CreateaWizardNavigator(null, null, designer.ReportItem.Site);
			group.Steps.Add(new WizardStep(group, new CalendarMonthAppearancePage(designer),
				Resources.CalendarSmartPanelMonthTitleAppearancePageTitle, Resources.MonthAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarDayAppearancePage(designer),
				Resources.CalendarSmartPanelDayAppearancePageTitle, Resources.DayAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarDayHeadersAppearancePage(designer),
				Resources.CalendarSmartPanelDayHeadersAppearancePageTitle, Resources.DayHeadersAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarWeekendAppearancePage(designer),
				Resources.CalendarSmartPanelWeekendAppearancePageTitle, Resources.WeekendAppearance_16));
			group.Steps.Add(new WizardStep(group, new CalendarFillerDayAppearancePage(designer),
				Resources.CalendarSmartPanelFillerDayAppearancePageTitle, Resources.FillerDayAppearance_16));
			return group;
		}

		/// <summary>
		/// Creates and returns calendar navigation page
		/// </summary>
		public static IWizardNavigator CreateCalendarNavigationPage(CalendarDesigner designer)
		{
			IServiceProvider provider = designer.ReportItem.Site;
			IWizardNavigator group = WizardStep.CreateaWizardNavigator(null, null, provider);
			group.Steps.Add(new WizardStep(group, new CalendarNavigationPage(designer),
				Resources.CalendarSmartPanelNavigationPageTitle, Resources.CalendarSmartPanelNavigationTitle, Resources.Navigation_16));
			return group;
		}
	}
}
