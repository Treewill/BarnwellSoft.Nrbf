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
using TUnit.Assertions.Enums;

namespace Barnwellsoft.Nrbf.Tests.BinaryFormatterInterop;

public partial class WriteBinaryFormatterInput
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

        var objectClassInfo = new NrbfClassInfo(NrbfMemberType.Object, []);
        var valueTupleType = new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary);
        var valueTupleClassInfo = new NrbfClassInfo(valueTupleType, []);
        var node = new NrbfArray(NrbfMemberType.Object)
        {
            null,
            new NrbfClass(objectClassInfo),
            null,
            null,
            true,
            new NrbfClass(valueTupleClassInfo),
            "XYZ",
            TimeSpan.FromSeconds(250),
            DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
            (ushort)0x8080
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>();
        var actualArray = (object[])actual;
        await Assert.That(actualArray[0]).IsNull();
        await Assert.That(actualArray[1]).IsNotNull();
        await Assert.That(actualArray[2]).IsNull();
        await Assert.That(actualArray[3]).IsNull();
        await Assert.That(actualArray[4]).IsEqualTo(source[4]);
        await Assert.That(actualArray[5]).IsEqualTo(source[5]);
        await Assert.That(actualArray[6]).IsEqualTo(source[6]);
        await Assert.That(actualArray[7]).IsEqualTo(source[7]);
        await Assert.That(actualArray[8]).IsEqualTo(source[8]);
        await Assert.That(actualArray[9]).IsEqualTo(source[9]);
    }

    [Test]
    public async Task ValueTupleObjectVariations()
    {
        var source = new object?[]
        {
            new ValueTuple<object>(DateTime.MinValue),
            new ValueTuple<object>(decimal.MinValue),
            new ValueTuple<object>(42),
        };

        var valueTupleObjectType = new NrbfMemberType(
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);

        var valueTupleObjectClassInfo = new NrbfClassInfo(
            valueTupleObjectType,
            [
                new NrbfClassMember("Item1", NrbfMemberType.Object),
            ]);

        var node = new NrbfArray(NrbfMemberType.Object)
        {
            new NrbfClass(valueTupleObjectClassInfo) { ["Item1"] = DateTime.MinValue },
            new NrbfClass(valueTupleObjectClassInfo) { ["Item1"] = decimal.MinValue },
            new NrbfClass(valueTupleObjectClassInfo) { ["Item1"] = 42 },
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfNull1()
    {
        var node = new NrbfArray(NrbfMemberType.Object, [new NrbfArrayDimension(1)]);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.HasProperty(x => x.Length).IsEqualTo(1);
        await Assert.That((object?[])actual)
            .All(x => x is null);
    }

    [Test]
    public async Task ArrayOfNull100()
    {
        var node = new NrbfArray(NrbfMemberType.Object, [new NrbfArrayDimension(100)]);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.HasProperty(x => x.Length).IsEqualTo(100);
        await Assert.That((object?[])actual)
            .All(x => x is null);
    }

    [Test]
    public async Task ArrayOfNull10000()
    {
        var node = new NrbfArray(NrbfMemberType.Object, [new NrbfArrayDimension(10000)]);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.HasProperty(x => x.Length).IsEqualTo(10000);
        await Assert.That((object?[])actual)
            .All(x => x is null);
    }

    [Test]
    public async Task ArrayCycle()
    {
        var node = new NrbfArray(NrbfMemberType.Object, [new NrbfArrayDimension(1)]);
        node[0] = node;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.HasProperty(x => x.Length).IsEqualTo(1);
        await Assert.That(((object?[])actual)[0]).IsSameReferenceAs(actual);
    }

    [Test]
    public async Task NonSystemLibraries()
    {
        var item1Type = CreateLibraryType("Item1", []);
        var item2Type = CreateLibraryType("Item2", []);
        var containerType = CreateLibraryType("Container", [item1Type, item2Type]);

        AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
        try
        {
            var item1Library = new NrbfLibrary("C163665520A645ACB8B4D04F0954E8C6_Item1, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            var item1MemberType = new NrbfMemberType("C163665520A645ACB8B4D04F0954E8C6_Item1.Item1", item1Library);

            var item2Library = new NrbfLibrary("C163665520A645ACB8B4D04F0954E8C6_Item2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            var item2MemberType = new NrbfMemberType("C163665520A645ACB8B4D04F0954E8C6_Item2.Item2", item2Library);

            var containerLibrary = new NrbfLibrary("C163665520A645ACB8B4D04F0954E8C6_Container, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            var containerMemberType =
                new NrbfMemberType("C163665520A645ACB8B4D04F0954E8C6_Container.Container", containerLibrary);
            var containerClassInfo = new NrbfClassInfo(
                containerMemberType,
                [
                    new NrbfClassMember("Item1", item1MemberType),
                    new NrbfClassMember("Item2", item2MemberType),
                ]);

            var node = new NrbfClass(containerClassInfo);

            var actual = ReadWithBinaryFormatter(node);

            await Assert.That(actual).IsNotNull();

        }
        finally
        {
            AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolve;
        }

        return;

        Assembly? AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            var shortName = args.Name;
            var shortNameEnd = shortName.IndexOf(',');
            if (shortNameEnd != -1)
                shortName = shortName.Substring(0, shortNameEnd);

            if (!shortName.StartsWith("C163665520A645ACB8B4D04F0954E8C6"))
                return null;

            if (shortName == item1Type.Assembly.GetName().Name)
                return item1Type.Assembly;

            if (shortName == item2Type.Assembly.GetName().Name)
                return item2Type.Assembly;

            if (shortName == containerType.Assembly.GetName().Name)
                return containerType.Assembly;

            return null;
        }
    }

    private static Type CreateLibraryType(
        string name,
        Span<Type> fields)
    {

        const string assemblyPrefix = "C163665520A645ACB8B4D04F0954E8C6";
        var assemblyName = new AssemblyName($"{assemblyPrefix}_{name}");
        var moduleName = $"{assemblyName.Name}.dll";
        var typeName = $"{assemblyName.Name}.{name}";

        var typeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.Run)
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