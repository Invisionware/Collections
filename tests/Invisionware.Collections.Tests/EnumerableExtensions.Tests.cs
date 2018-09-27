using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Invisionware.Collections.Tests
{
	[TestFixture]
	public class EnumerableExtensionsTests
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
		[TestCaseSource(nameof(CollectionTestItems))]
		public void AnySafeTest(ICollection<object> collection)
		{
			collection.AnySafe(o => o.ToString() == "99");

			Assert.Pass();
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ForEachActionOfTypeTest(ICollection<object> collection)
		{
			int iCounter = 0;
			var result = collection.ForEach<object>(o => { iCounter++; return true; });

			result.Count().Should().Be(collection?.Count() ?? 0);

			//iCounter.ShouldBe(collection.Count());
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ForEachActionTest(ICollection<object> collection)
		{
			int iCounter = 0;
			collection.ForEach(o => { iCounter++; });

			if (collection != null)
			{
				iCounter.Should().Be(collection.Count());
			}
			else Assert.Pass();
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ForEachOfTypeFuncTest(ICollection<object> collection)
		{
			int iCounter = 0;
			var result = collection.ForEach<object>(o => { iCounter++; return true; });

			result.Should().NotBeNull();

			if (collection != null)
			{
				result.Count().Should().Be(collection.Count());
				iCounter.Should().Be(collection.Count());
			}
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ForEachFuncTest(ICollection<object> collection)
		{
			int iCounter = 0;
			var result = collection.ForEach(o => { iCounter++; return true; });

			result.Should().NotBeNull();

			if (collection != null)
			{
				result.Count().Should().Be(collection.Count());
				iCounter.Should().Be(collection.Count());
			}
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ForEachAsyncTest(ICollection<object> collection)
		{
			Assert.Inconclusive("Not Implemented Yet");
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void AddRangeTest(ICollection<object> collection)
		{
			var count = collection?.Count ?? 0;
			var result = collection.AddRange(new object[] { 7, 8, 9 }.ToList());

			if (collection == null)
				result.Should().BeNull();
			else
				result.Count().Should().Be(count + 3);
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void RemoveDuplicatesTest(ICollection<object> collection)
		{
			var result = collection.RemoveDuplicates(o => o);

			if (collection == null)
				result.Should().BeNull();
			else
				result.Count().Should().BeLessOrEqualTo(collection?.Count() ?? 0);
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void CountSafeTest(ICollection<object> collection)
		{
			collection.CountSafe().Should().NotBe(null);
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void ChunkByTest(ICollection<object> collection)
		{
			if (collection != null)
			{
				var result = collection.ChunkBy(2);

				result.Should().NotBeNull();
			}
		}

		[Test]
		[TestCaseSource(nameof(CollectionTestItems))]
		public void TakeLastTest(ICollection<object> collection)
		{
			if (collection != null && collection.Count > 2)
			{
				collection.TakeLast(2).Should().NotBeNullOrEmpty();
			}
		}

		public static IEnumerable<TestCaseData> CollectionTestItems()
		{
			yield return new TestCaseData(new object[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 }.ToList());
			yield return new TestCaseData(new object[] { "1", "1", "2", "2", "3", "3", "4", "4", "5", "5" }.ToList());
			yield return new TestCaseData(new object[] { }.ToList());
			yield return new TestCaseData(null);
		}
	}
}
