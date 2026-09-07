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

namespace Barnwellsoft.Nrbf.Tests.BinaryFormatterInterop;

public partial class ReadBinaryFormatterOutput
{
    [Test]
    public async Task ValueTupleObjectWithBooleanValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(true);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Boolean));

        await Assert.That((bool)item1).IsEqualTo((bool)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithByteValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>((byte)0x87);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Byte));

        await Assert.That((byte)item1).IsEqualTo((byte)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithCharValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>('X');
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Char));

        await Assert.That((char)item1).IsEqualTo((char)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithDecimalValue()
    {
        // Note: The assertion handles the actual behavior of the BinaryFormatter.
        //       The result is not what I would expect.

        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(1234.5678m);
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
                name => name.IsEqualTo("System.Decimal")) // Expected: typeof(ValueTuple<object>.Item1)
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Decimal));

        await Assert.That((decimal)item1).IsEqualTo((decimal)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithDoubleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(1234.5678d);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Double));

        await Assert.That((double)item1).IsEqualTo((double)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithInt16Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>((short)-0x1234);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int16));

        await Assert.That((short)item1).IsEqualTo((short)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithInt32Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(-0x12345678);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int32));

        await Assert.That((int)item1).IsEqualTo((int)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithInt64Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(-0x1234567812345678);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int64));

        await Assert.That((long)item1).IsEqualTo((long)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithSByteValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>((sbyte)-0x40);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.SByte));

        await Assert.That((sbyte)item1).IsEqualTo((sbyte)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithSingleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(123.456f);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Single));

        await Assert.That((float)item1).IsEqualTo((float)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithTimeSpanValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";

        var source = new ValueTuple<object>(TimeSpan.FromSeconds(365));
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.TimeSpan));

        await Assert.That((TimeSpan)item1).IsEqualTo((TimeSpan)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithDateTimeValue()
    {
        // Note: The assertion handles the actual behavior of the BinaryFormatter.
        //       The result is not what I would expect.

        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(
            DateTime.ParseExact(
                "2026-05-15T11:27:23.3012265Z",
                "O",
                CultureInfo.InvariantCulture));
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
                name => name.IsEqualTo("System.DateTime")) // Expected: typeof(ValueTuple<object>.Item1) 
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.DateTime));

        await Assert.That((DateTime)item1).IsEqualTo((DateTime)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt16Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>((ushort)0xFE12);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt16));

        await Assert.That((ushort)item1).IsEqualTo((ushort)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt32Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(0xFE12DC34);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt32));

        await Assert.That((uint)item1).IsEqualTo((uint)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt64Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(0xFE12DC34BA564321);
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt64));

        await Assert.That((ulong)item1).IsEqualTo((ulong)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithStringValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>("ABC");
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.String));

        await Assert.That((string)item1).IsEqualTo((string)source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectWithValueTupleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(new ValueTuple());
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Class))
            .And.IsTypeOf<NrbfClass>()
            .And.Member(
                x => x.ClassInfo.ClassName,
                name => name.IsEqualTo("System.ValueTuple"))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());
    }
}