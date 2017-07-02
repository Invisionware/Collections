# Invisionware Framework
Invisionware Frmaework is a collection of utilities classes, extension methods, and new functionality to simplify creatig application in .NET. Amost all of the libraries are support on Dektop and Mobile (including Xamarin) development environments to provide the maxinum value possible.

## Collections
This portion of the Invisionware Framework is focused on enhancements to standard collections within the .NET frmaework

[![NuGet](https://img.shields.io/nuget/v/Invisionware.Collections.svg)](https://www.nuget.org/packages/Invisionware.Collections)

Packages related to Invisionware Collections
```powershell
Install-Package Invisionware.Collections
```

Then just add the following using statement
```c#
using Invisionware.Collections;
```

### Dictionary Extensions
The following outline the extension methods provided for the IDictionary<,> class

#### RenameKey
Provides a simple way to rename an existing key in a dictionary

```c#
var dict = new Dictionary<string, string>();

dict["key"] = "value";

dict.RenameKey("key", "newkey");

Console.WriteLine(dict["newkey"]);
// "value"
```
or
```c#
using Invisionware.Collections;

var dict = new Dictionary<int, string>();

dict[1234] = "value";

dict.RenameKey(1234, 9876);

Console.WriteLine(dict[9876]));
// "value"
```
