using System;
using GrapeCity.ActiveReports.Calendar.Validation;
using GrapeCity.ActiveReports.PageReportModel;
using NUnit.Framework;

namespace GrapeCity.ActiveReports.Calendar.Tests
{
	/// <summary>
	/// The fixture to test the <see cref="ValidationUtils"/>
	/// </summary>
	[TestFixture]
	public class ValidationUtilsFixture
	{
		private static readonly Length min = "0pt";
		private static readonly Length max = "160pt";

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void ValidateLengthTestInvalidUnit()
		{
			Length invalidUnitLength = "38parrots";
			Assert.IsFalse(invalidUnitLength.IsValid);
			ValidationUtils.ValidateLength(invalidUnitLength, min, max);
		}

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ValidateLengthTestIvalidLength1()
		{
			Length invalidUnitLength = "-10pt";
			ValidationUtils.ValidateLength(invalidUnitLength, min, max);
		}

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ValidateLengthTestIvalidLength2()
		{
			Length invalidUnitLength = "180pt";
			ValidationUtils.ValidateLength(invalidUnitLength, min, max);
		}

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		public void ValidateLengthTestIvalidLength3()
		{
			Length invalidUnitLength = "160pt";
			// expects that exception won't be thrown
			ValidationUtils.ValidateLength(invalidUnitLength, min, max);
		}

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		public void ValidateLengthTestIvalidLength4()
		{
			Length invalidUnitLength = "0pt";
			// expects that exception won't be thrown
			ValidationUtils.ValidateLength(invalidUnitLength, min, max);
		}

		///<summary>
		/// Tests the ValidateLength method of the <see cref="ValidationUtils"/>
		///</summary>
		[Test]
		public void ValidateLengthTestValidLength()
		{
			Length length = "10pt";
			// expects that exception won't be thrown
			ValidationUtils.ValidateLength(length, min, max);
		}
	}
}
