using System.Collections.Generic;
using NUnit.Framework;

namespace Invisionware.Collections.Tests
{
	[TestFixture]
	public class DictionaryExtensionsTests
	{
		private readonly IDictionary<string, string> _dictionaryStringString = new Dictionary<string, string>
		{
			{"key1", "value1"},
			{"key2", "value2"},
			{"key3", "value3"},
		};

		private readonly IDictionary<string, int> _dictionaryStringInt = new Dictionary<string, int>()
		{
			{"key1", 0},
			{"key2", 1},
			{"key3", 2},
		};
			
		[SetUp]
		public void Initialize()
		{
			
		}

		[Test]
		public void RenameKeyString()
		{
			Assert.IsTrue(_dictionaryStringString.ContainsKey("key1"));
	
			_dictionaryStringString.RenameKey("key1", "key1a");

			Assert.IsFalse(_dictionaryStringString.ContainsKey("key1"));
			Assert.IsTrue(_dictionaryStringString.ContainsKey("key1a"));
		}

		[Test]
		public void RenameKeyNonString()
		{
			Assert.IsTrue(_dictionaryStringInt.ContainsKey("key1"));

			_dictionaryStringInt.RenameKey("key1", "key1a");

			Assert.IsFalse(_dictionaryStringInt.ContainsKey("key1"));
			Assert.IsTrue(_dictionaryStringInt.ContainsKey("key1a"));
		}
	}
}
