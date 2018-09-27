using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Invisionware.Collections
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// A null reference safe version of Any.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns><c>true</c> if the collection is not null and contains any items, <c>false</c> otherwise.</returns>
		public static bool AnySafe<T>(this IEnumerable<T> collection, Func<T, bool> predicate = null)
		{
			return collection != null && (predicate != null ? collection.Any(predicate) : collection.Any());
		}


		///// <summary>
		///// A Null Safe implementation of Any
		///// </summary>
		///// <param name="collection">The collection.</param>
		///// <returns><c>true</c> if collection contains any items, <c>false</c> otherwise.</returns>
		//public static bool AnySafe(this IList collection)
		//{
		//	return collection != null && collection.Count != 0;
		//}

		/// <summary>
		/// Iterates through the entire collection and executes the Action on each item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="func">The function.</param>
		/// <param name="throwException">if set to <c>true</c> [throw exception].</param>
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> func, bool throwException = true)
		{
			if (collection == null) return;
			if (func == null) return;

			foreach (var item in collection)
			{
				try
				{
					func(item);
				}
				catch
				{
					if (throwException) throw;
				}
			}
		}

		/// <summary>
		/// Iterates through the entire collection and executes the Action on each item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="func">The function.</param>
		public static void ForEach(this IEnumerable collection, Action<object> func, bool throwException = true)
		{
			if (collection == null) return;
			if (func == null) return;

			foreach (var item in collection)
			{
				try
				{
					func(item);
				}
				catch
				{
					if (throwException) throw;
				}
			}
		}

		/// <summary>
		/// Iterates through the entire collection and executes the Func on each item
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="func">The function.</param>
		public static IEnumerable<T> ForEach<T>(this IEnumerable collection, Func<object, T> func)
		{
			if (collection != null && func != null)
			{
				 foreach (var item in collection)
				{
					yield return func(item);
				}
			}
		}

		/// <summary>
		/// for each as an asynchronous operation.
		/// </summary>
		/// <typeparam name="TSource">The type of the t source.</typeparam>
		/// <typeparam name="TResult">The type of the t result.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="selector">The selector.</param>
		/// <param name="maxDegreesOfParallelism">The maximum degrees of parallelism.</param>
		/// <returns>Task&lt;IList&lt;TResult&gt;&gt;.</returns>
		public static async Task<IList<TResult>> ForEachAsync<TSource, TResult>(this IEnumerable<TSource> source,
			Func<TSource, Task<TResult>> selector, int maxDegreesOfParallelism = 4)
		{
			var results = new List<TResult>();

			var activeTasks = new HashSet<Task<TResult>>();
			foreach (var item in source)
			{
				activeTasks.Add(selector(item));
				if (activeTasks.Count >= maxDegreesOfParallelism)
				{
					var completed = await Task.WhenAny(activeTasks);
					activeTasks.Remove(completed);
					results.Add(completed.Result);
				}
			}

			results.AddRange(await Task.WhenAll(activeTasks));
			return results;
		}

		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="range">The range.</param>
		/// <param name="includeFunc">The comparison function used to exclude items if needed. Return true to include, False to exclude (default is true)</param>
		/// <returns>ICollection&lt;T&gt;.</returns>
		public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> range, Func<T, bool> includeFunc = null)
		{
			if (collection == null) return null;

			foreach (var o in range)
			{
				if (includeFunc == null || includeFunc(o))
				{
					collection.Add(o);
				}
			}

			return collection;
		}

		/// <summary>
		/// Removes the duplicates.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TKey">The type of the t key.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="groupByFunc">The group by function.</param>
		/// <returns>ICollection&lt;T&gt;.</returns>
		public static ICollection<T> RemoveDuplicates<T, TKey>(this ICollection<T> collection, Func<T, TKey> groupByFunc)
		{
			if (collection == null) return null;

			var result = collection.Select(x => x)
				.GroupBy(groupByFunc)
				.Select(grp => grp.FirstOrDefault());

			return result.ToObservableCollection();
		}

		/// <summary>
		/// To the observable collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <returns>ObservableCollection&lt;T&gt;.</returns>
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			if (source == null) return null;

			return new ObservableCollection<T>(source);
		}

		/// <summary>
		/// Breaks the array up into Chunks of a specific size.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="chunkSize">Size of the chunk.</param>
		/// <returns>A collection of collections that is split into slices</returns>
		public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / chunkSize)
				.Select(x => x.Select(v => v.Value));
		}

		/// <summary>
		/// A null reference safe version of Count.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns>the number of items in the collection or 0 if its null.</returns>
		public static int CountSafe<T>(this IEnumerable<T> collection, Func<T, bool> predicate = null)
		{
			return collection != null ? (predicate != null ? collection.Count(predicate) : collection.Count()) : 0;
		}

		/// <summary>
		/// Takes the last n number of items in the array.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns>a new collection that contains only the last count number of items</returns>
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
		{
			return source?.Skip(Math.Max(0, source.Count() - count));
		}

	}
}
