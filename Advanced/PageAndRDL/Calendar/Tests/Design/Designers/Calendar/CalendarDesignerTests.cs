using System.Reflection;
using GrapeCity.ActiveReports.Calendar.Design.Designers;
using GrapeCity.ActiveReports.Design;
using NUnit.Framework;

namespace GrapeCity.ActiveReports.Calendar.Tests.Design.Designers.Calendar
{
	[TestFixture]
	public class CalendarDesignerTests
	{
		[Test]
		public void StylesArrayTest()
		{
			var stylesField = typeof(CalendarDesigner).GetField("StylesArray", BindingFlags.NonPublic | BindingFlags.Static);
			Assert.NotNull(stylesField);

			var styles = stylesField.GetValue(null) as Styles[];
			Assert.NotNull(styles);
		}
	}
}
