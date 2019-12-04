using GrapeCity.ActiveReports.Extensibility.Rendering.Components;
using System.Diagnostics;

namespace GrapeCity.ActiveReports.Calendar.Components.Calendar
{
	/// <summary>
	/// Representation of the calendar appointment's action.
	/// </summary>
	public sealed class Action : IAction
	{
		/// <summary>
		/// Action link value
		/// </summary>
		private readonly string _link;

		/// <summary>
		/// Action type
		/// </summary>
		private readonly ActionType _actionType;

		/// <summary>
		/// Drillthrouth value
		/// </summary>
		private readonly IDrillthrough _drillthrough;

		private readonly int _actionId;

		private Action(string linkValue, ActionType actionType, int actionId)
		{
			_link = linkValue;
			_actionType = actionType;
			_actionId = actionId;
		}

		private Action(IDrillthrough drillthrough, int actionId)
		{
			_drillthrough = drillthrough;
			_actionType = ActionType.DrillThrough;
			_actionId = actionId;
		}

		/// <summary>
		/// Creates BookMark action
		/// </summary>
		/// <param name="bookmark"></param>
		/// <param name="actionId"></param>
		/// <returns></returns>
		public static Action CreateBookmark(string bookmark, int actionId)
		{
			return new Action(bookmark, ActionType.BookmarkLink, actionId);
		}

		/// <summary>
		/// Creates Hyperlink action
		/// </summary>
		/// <param name="hyperlink"></param>
		/// <param name="actionId"></param>
		/// <returns></returns>
		public static Action CreateHyperlink(string hyperlink, int actionId)
		{
			return new Action(hyperlink, ActionType.HyperLink, actionId);
		}

		/// <summary>
		/// Creates Drillthrough action
		/// </summary>
		/// <param name="drillthrough"></param>
		/// <param name="actionId"></param>
		/// <returns></returns>
		public static Action CreateDrillthrouth(IDrillthrough drillthrough, int actionId)
		{
			return new Action(drillthrough, actionId);
		}

		public int ActionId
		{
			get { return _actionId; }
		}

		#region IAction Members

		public string BookmarkLink
		{
			get
			{
				if (_actionType != ActionType.BookmarkLink)
				{
					Debug.Fail("BookmarkLink is undefined for this Action");
					return null;
				}

				return _link;
			}
		}

		public string HyperLink
		{
			get
			{
				if (_actionType != ActionType.HyperLink)
				{
					Debug.Fail("Hyperlink is undefined for this Action");
					return null;
				}

				return _link;
			}
		}

		public IDrillthrough Drillthrough
		{
			get
			{
				if (_actionType != ActionType.DrillThrough)
				{
					Debug.Fail("Drillthgough is undefined for this Action");
					return null;
				}

				return _drillthrough;
			}
		}

		public ActionType ActionType
		{
			get
			{
				return _actionType;
			}
		}

		#endregion
	}
}
