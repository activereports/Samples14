using GrapeCity.ActiveReports.Design.Advanced;

namespace GrapeCity.ActiveReports.Samples.FlatUserDesigner
{
	internal class SessionSettingsStorage : ISessionSettingsStorage
	{
		public void Load(SessionSettings settings)
		{
			settings.RecentFiles = Settings.Default.RecentFiles;
			settings.WindowSize = Settings.Default.WindowSize;
			settings.WindowPosition = Settings.Default.WindowPosition;
			settings.IsWindowMaximized = Settings.Default.IsWindowMaximized;
			settings.RightPanelWidth = Settings.Default.RightPanelWidth;
			settings.ActiveRightTab = Settings.Default.ActiveRightTab;
			settings.LeftPanelWidth = Settings.Default.LeftPanelWidth;
			settings.IsGroupEditorActive = Settings.Default.IsGroupEditorActive;
			settings.IsReportExplorerActive = Settings.Default.IsReportExplorerActive;
			settings.IsLayersListActive = Settings.Default.IsLayersListActive;
		}

		public void Save(SessionSettings settings)
		{
			Settings.Default.RecentFiles = settings.RecentFiles;
			Settings.Default.WindowSize = settings.WindowSize;
			Settings.Default.WindowPosition = settings.WindowPosition;
			Settings.Default.IsWindowMaximized = settings.IsWindowMaximized;
			Settings.Default.RightPanelWidth = settings.RightPanelWidth;
			Settings.Default.ActiveRightTab = settings.ActiveRightTab;
			Settings.Default.LeftPanelWidth = settings.LeftPanelWidth;
			Settings.Default.IsGroupEditorActive = settings.IsGroupEditorActive;
			Settings.Default.IsReportExplorerActive = settings.IsReportExplorerActive;
			Settings.Default.IsLayersListActive = settings.IsLayersListActive;
			Settings.Default.Save();
		}
	}
}
