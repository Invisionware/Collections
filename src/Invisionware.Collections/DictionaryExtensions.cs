using System.Collections.Generic;

namespace Invisionware.Collections
{
	/// <summary>
	/// Class Extensions.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Renames the key.
		/// </summary>
		/// <typeparam name="TKey">The type of the t key.</typeparam>
		/// <typeparam name="TValue">The type of the t value.</typeparam>
		/// <param name="source">The dictionary.</param>
		/// <param name="oldKey">The old key.</param>
		/// <param name="newKey">The new key.</param>
		/// <returns><c>true</c> if cannot get the value, <c>false</c> otherwise true.</returns>
		public static bool RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> source,
			TKey oldKey, TKey newKey)
		{
			if (!source.TryGetValue(oldKey, out TValue value))
				return false;

			source.Remove(oldKey);  // do not change order
			source[newKey] = value;  // or source.Add(newKey, value) depending on ur comfort
			return true;
		}

		/// <summary>
		/// Renames the key.
		/// </summary>
		/// <param name="source">The dictionary.</param>
		/// <param name="oldKey">The old key.</param>
		/// <param name="newKey">The new key.</param>
		/// <returns><c>true</c> if cannot get value, <c>false</c> otherwise true.</returns>
		public static bool RenameKey(this IDictionary<string, string> source, string oldKey, string newKey)
		{
			return source.RenameKey<string, string>(oldKey, newKey);
		}
	}
}