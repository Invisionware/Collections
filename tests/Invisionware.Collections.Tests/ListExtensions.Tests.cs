using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Invisionware.Collections.Tests
{
	[TestFixture]
	public class ListExtensionsTests
	{
		[SetUp]
		public void TestInitialize()
		{
		}

		[TearDown]
		public void TestCleanup()
		{
		}

		[Test]
		[TestCaseSource(nameof(ListTestItems))]
		public void RemoveRangeTest(IList<object> collection)
		{
			if (collection.AnySafe())
			{
				var lst = collection.RemoveRange(2, 2);

				lst.Should().HaveCount(collection.Count - 2);
			}
		}

		public static IEnumerable<TestCaseData> ListTestItems()
		{
			yield return new TestCaseData(new object[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }.ToList());
			yield return new TestCaseData(new object[] { "1", "1", "2", "2", "3", "3", "4", "4", "5", "5" }.ToList());
			yield return new TestCaseData(new object[] { }.ToList());
			yield return new TestCaseData(null);
		}
	}
}
