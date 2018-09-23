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

