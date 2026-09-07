# BarnwellSoft.Nrbf

A .NET Library for reading and writing [MS-NRBF](https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-nrbf/)
files with a focus on being compatible with the BinaryFormatter. This library does not load assemblies or associated
types like the `BinaryFormatter`
([MSDN](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter))
does. The data is instead loaded into an intermediate representation with limited recursion. In this representation
arrays are kept sparse. This allows loading of untrusted payloads.

## Security

Converting an `NrbfArray` to another collection type may create a very large collection. Always check the dimensions of
the `NrbfArray` before converting.

Recursive records may exist in NRBF files as such recursion is limited by default.

High rank arrays may exist in NRBF files and at present this library does not contain an efficient way to keep them
sparse. As a result the rank is limited by default.

## Quick Start

```
// Read file
using (var stream = File.OpenRead("YourInputFile.dat"))
{
  var reader = new NrbfMessageReader();
  node = reader.ReadMessage(stream).Root;
}

// Update value
node["Employees"][10]["FirstName"] = "Jane";

// Write file
using (var stream = File.OpenWrite("YourOutputFile.dat"))
{
  var writer = new NrbfMessageWriter();
  writer.WriteMessage(node);
}
```

## License

This project is licensed under the [MIT](LICENSE.md) License.

## Development Notes

The `BinaryFormatter` tests do not build by default as they use the unsafe `BinaryFormatter`
([MSDN](https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide)). This also
means that `dotnet test` reports an error if run directly with no additional arguments.

## Sponsoring

If you find this beneficial and can support me:

[<img src="https://cdn.buymeacoffee.com/buttons/default-green.png" alt="Buy Me A Coffee" height="41" width="174">](https://buymeacoffee.com/trevorbarnwell)

