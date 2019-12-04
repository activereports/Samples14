using System;
using System.ComponentModel;

namespace GrapeCity.ActiveReports.Calendar
{
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ResourceDescriptionAttribute : DescriptionAttribute
	{
		private readonly string _resourceKey;

		public ResourceDescriptionAttribute(string resourceKey)
		{
			_resourceKey = resourceKey;
		}

		public override string Description
		{
			get { return Resources.ResourceManager.GetString(_resourceKey); }
		}
	}
	/// <summary>
	/// The implementation of the resource-based display name.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class ResourceDisplayNameAttribute : DisplayNameAttribute
	{
		private readonly string _resourceKey;
		public ResourceDisplayNameAttribute(string resourceKey)
		{
			_resourceKey = resourceKey;
		}
		public override string DisplayName
		{
			get
			{
				return Resources.ResourceManager.GetString(_resourceKey);
			}
		}
	}
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ResourceCategoryAttribute : CategoryAttribute
	{
		private readonly string _resourceKey;
		public ResourceCategoryAttribute(string resourceKey)
		{
			_resourceKey = resourceKey;
		}
		protected override string GetLocalizedString(string value)
		{
			return Resources.ResourceManager.GetString(_resourceKey)
				   ?? base.GetLocalizedString(value);
		}
	}
}
