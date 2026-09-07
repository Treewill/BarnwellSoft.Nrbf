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
using TUnit.Assertions.Enums;

namespace Barnwellsoft.Nrbf.Tests;

public partial class ReadBinaryFormatterOutput
{
    [Test]
    public async Task ValueTupleArrayOfBooleanValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Boolean[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<bool[]>([true, false, true]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Boolean[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Boolean"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (bool)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfByteValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<byte[]>([0xA0, 0x05, 0xFF]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Byte[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Byte"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (byte)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfCharValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Char[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<char[]>(['X', 'y', 'Z']);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Char[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Char"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (char)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfDecimalValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Decimal[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<decimal[]>([123.45m, -234.56m, 345.67m]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Decimal[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Decimal"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (decimal)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfDoubleValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Double[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<Double[]>([123.45, -234.56, 345.67]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Double[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Double"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (double)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfInt16Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int16[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<short[]>([0x700E, -0x1234, 0x4321]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Int16[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int16"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (short)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfInt32Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int32[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<int[]>([0x7F1234FE, -0x12345678, 0x13572468]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Int32[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int32"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (int)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfInt64Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int64[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<long[]>([0x1234567812345678, -0x1357246813572468, 0x7FFF12345678FFFE]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Int64[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int64"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (long)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfSByteValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.SByte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<sbyte[]>([0x7E, -0x10, 0x5A]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.SByte[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.SByte"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (sbyte)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfSingleValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Single[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<float[]>([123.45f, -234.56f, 345.67f]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Single[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Single"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (float)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfTimeSpanValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.TimeSpan[], System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]";
        var source = new ValueTuple<TimeSpan[]>([
            TimeSpan.FromSeconds(90),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromHours(90),
        ]);

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.TimeSpan[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.TimeSpan"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (TimeSpan)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfDateTimeValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.DateTime[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<DateTime[]>([
            DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2016-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2006-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
        ]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.DateTime[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.DateTime"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (DateTime)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt16Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt16[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ushort[]>([0x8080, 0x1234, 0xFEDC]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.UInt16[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt16"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (ushort)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt32Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt32[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<uint[]>([0x80706050, 0x12345678, 0xFEDCBA98]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.UInt32[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt32"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (uint)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfUInt64Values()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt64[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ulong[]>([0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.UInt64[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt64"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (ulong)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfStringValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.String[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<string[]>(["abc", "def", "xyz"]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.String[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.String"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)item1;
        await Assert.That(array.Select(x => (string)x))
            .IsEquivalentTo(source.Item1, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueTupleArrayOfObjectValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object[]>([new object(), new object(), new object()]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.Object[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
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
                count => count.IsEqualTo(3));
    }

    [Test]
    public async Task ValueTupleArrayOfValueTupleValues()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.ValueTuple[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ValueTuple[]>(new ValueTuple[3]);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo(sourceTypeName))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.ClassInfo.Members[0].Type.ClassName,
                name => name.IsEqualTo("System.ValueTuple[]"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.ValueTuple"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(3));
    }
}