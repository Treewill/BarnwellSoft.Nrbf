#region license
// Copyright (c) 2026 Trevor Barnwell
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the “Software”), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the
// Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Barnwellsoft.Nrbf.Tests.BinaryFormatterInterop;

public partial class ReadBinaryFormatterOutput
{
    [Test]
    public async Task HeterogeneousArray()
    {
        object?[] source =
        [
            null,
            new object(),
            null,
            null,
            true,
            new ValueTuple(),
            "XYZ",
            TimeSpan.FromSeconds(250),
            DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
            (ushort)0x8080,
        ];

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(source.Length));

        await Assert.That(node[0]).IsNull();
        await Assert.That(node[1]).IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo("System.Object"));
        await Assert.That(node[2]).IsNull();
        await Assert.That(node[3]).IsNull();
        await Assert.That((bool)node[4]).IsTrue();
        await Assert.That(node[5]).IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo("System.ValueTuple"));
        await Assert.That((string?)node[6]).IsEqualTo((string?)source[6]);
        await Assert.That((TimeSpan?)node[7]).IsEqualTo((TimeSpan?)source[7]);
        await Assert.That((DateTime?)node[8]).IsEqualTo((DateTime?)source[8]);
        await Assert.That((ushort?)node[9]).IsEqualTo((ushort?)source[9]);
    }

    [Test]
    public async Task ValueTupleObjectVariations()
    {
        // Note: The assertion handles the actual behavior of the BinaryFormatter.
        //       The result is not what I would expect.
        var source = new[]
        {
            new ValueTuple<object>(DateTime.MinValue),
            new ValueTuple<object>(decimal.MinValue),
            new ValueTuple<object>(42),
        };

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        var one = (NrbfClass)node[0];
        var two = (NrbfClass)node[1];
        var three = (NrbfClass)node[2];

        using var unused = Assert.Multiple();

        await Assert.That(two.ClassInfo)
            .IsSameReferenceAs(one.ClassInfo);

        await Assert.That(three.ClassInfo)
            .IsSameReferenceAs(one.ClassInfo);

        await Assert.That(one.ClassInfo.Members[0].Type)
            .IsEqualTo(NrbfMemberType.DateTime);  // Expected: typeof(ValueTuple<object>.Item1)

        await Assert.That((DateTime)one["Item1"])
            .IsEqualTo(DateTime.MinValue);

        await Assert.That((decimal)two["Item1"])
            .IsEqualTo(decimal.MinValue);

        await Assert.That((int)three["Item1"])
            .IsEqualTo(42);
    }

    [Test]
    public async Task ArrayOfNull1()
    {
        var source = new object[1];
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node).IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(source.Length));
    }

    [Test]
    public async Task ArrayOfNull100()
    {
        var source = new object[100];
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node).IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(source.Length));
    }

    [Test]
    public async Task ArrayOfNull10000()
    {
        var source = new object[10000];
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node).IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(source.Length));
    }

    [Test]
    public async Task ArrayCycle()
    {
        var source = new object[1];
        source[0] = source;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node[0]).IsSameReferenceAs(node);
    }

    [Test]
    public async Task RecursiveLimit()
    {
        var source = new[]
        {
            new ValueTuple<object>(DateTime.MinValue),
            new ValueTuple<object>(decimal.MinValue),
            new ValueTuple<object>(42),
        };

        var reader = new NrbfMessageReader(
            new NrbfMessageReaderOptions() { MaximumRecursionDepth = 0 });

        var stream = GetSerializedStream(source);
        await Assert.That(() => reader.ReadMessage(stream).Root)
            .Throws<NrbfMessageReaderException>();
    }

    [Test]
    public async Task NonSystemLibraries()
    {
        var item1Type = CreateLibraryType("Item1", []);
        var item2Type = CreateLibraryType("Item2", []);
        var containerType = CreateLibraryType("Container", [item1Type, item2Type]);
        var source = Activator.CreateInstance(containerType)!;

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node).IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(containerType.FullName))
            .And.Member(
                x => x.ClassInfo.Library.Name,
                name => name.IsEqualTo(containerType.Assembly.FullName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsFalse())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo(item1Type.FullName))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.Name,
                name => name.IsEqualTo(item1Type.Assembly.FullName))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsFalse())
            .And.Member(
                x => x.ClassInfo.Members[1].Type.ClassName,
                name => name.IsEqualTo(item2Type.FullName))
            .And.Member(
                x => x.ClassInfo.Members[1].Type.Library.Name,
                name => name.IsEqualTo(item2Type.Assembly.FullName))
            .And.Member(
                x => x.ClassInfo.Members[1].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsFalse());
    }

    private static Type CreateLibraryType(
        string name,
        Span<Type> fields)
    {
        const string assemblyPrefix = "BEF1BA4E91124451B8B2230BF35A6B4D";
        var assemblyName = new AssemblyName($"{assemblyPrefix}_{name}");
        var moduleName = $"{assemblyName.Name}.dll";
        var typeName = $"{assemblyName.Name}.{name}";

        var typeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.RunAndCollect)
            .DefineDynamicModule(moduleName)
            .DefineType(typeName, TypeAttributes.Public);


        typeBuilder.SetCustomAttribute(
            new CustomAttributeBuilder(
                typeof(SerializableAttribute).GetConstructor([])!,
                []));

        var i = 1;
        foreach (var field in fields)
        {
            typeBuilder.DefineField($"Item{i}", field, FieldAttributes.Public);
            ++i;
        }

        return typeBuilder.CreateType();
    }
}