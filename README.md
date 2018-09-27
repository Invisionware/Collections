# Invisionware.Collections
Core components for the Invisionware framework

# Extensions
The following are the details o nthe extensions provided in this library

## Dictionary
The follow extensions are supported on an IDictionary

```
var dct = new Dictionary<string, string>() { { "a", "a1" }, { "b", "b1" } };
```

### RenameKey
Rename a specific key in the dictionary

```
dct.RenameKey("a", "key");
Console.WriteLine(dct.Keys); // Outputs key, b
```

## IEnumerable
The follow extensions are supported on IEnumerable and IEnumerable<T>

### AnySafe
This extension mimics the Any() method with the addition of being safe to all on null objects.  In the event that the collection is NULL it returns FALSE

```
IList<string> lst = null;

Console.WriteLine(lst.AnySafe()); // Outputs "False"
```

### ForEach
Allows for executing a lambda expression on each object in a collection.  Supports both standard and Async type methods

```
var lst = new[] { 1, 2, 3, 4 }l
var total = 0;

lst.ForEach( x => { total += x });
lst.ForEach(myCustomFunction);

```

### AddRange

### RemoveDuplicates
Allows for removing duplicate values in a collection effeciently

```
var lst = new[] { 1, 2, 1, 5, 2, 4, 6 }
var lstNew = lst.RemoveDuplicates();
```

### ToObservableCollection
Converts a collection to an Observable Collection.  Useful for data binding scenerios

## List

### RemoveRange
Removes a specific number of items from the list starting at the specified location

```
var lst = new[] { 1, 2, 1, 5, 2, 4, 6 }
var result = lst.RemoveRange(2, 2); // Removes "2, 1" from the list
```	