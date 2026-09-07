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
    public async Task ValueTupleBooleanValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<bool>(true);
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
                name => name.IsEqualTo("System.Boolean"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Boolean));

        await Assert.That((bool)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleByteValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Byte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<byte>(0x87);
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
                name => name.IsEqualTo("System.Byte"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Byte));

        await Assert.That((byte)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleCharValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<char>('X');
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
                name => name.IsEqualTo("System.Char"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Char));

        await Assert.That((char)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleDecimalValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<decimal>(1234.5678m);
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
                name => name.IsEqualTo("System.Decimal"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Decimal));

        await Assert.That((decimal)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleDoubleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<double>(1234.5678d);
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
                name => name.IsEqualTo("System.Double"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Double));

        await Assert.That((double)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleInt16Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<short>(-0x1234);
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
                name => name.IsEqualTo("System.Int16"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int16));

        await Assert.That((short)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleInt32Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<int>(-0x12345678);
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
                name => name.IsEqualTo("System.Int32"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int32));

        await Assert.That((int)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleInt64Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<long>(-0x1234567812345678);
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
                name => name.IsEqualTo("System.Int64"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int64));

        await Assert.That((long)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleSByteValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.SByte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<sbyte>(-0x40);
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
                name => name.IsEqualTo("System.SByte"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.SByte));

        await Assert.That((sbyte)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleSingleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<float>(123.456f);
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
                name => name.IsEqualTo("System.Single"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Single));

        await Assert.That((float)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleTimeSpanValue()
    {
        // Note: The sourceTypeName is the actual behavior of the BinaryFormatter.
        //       The result is not what I would expect.

        // Expected: mscorlib
        var sourceTypeName =
            "System.ValueTuple`1[[System.TimeSpan, System.Private.CoreLib, Version=10.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]";
        var source = new ValueTuple<TimeSpan>(TimeSpan.FromSeconds(365));
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
                name => name.IsEqualTo("System.TimeSpan"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.TimeSpan));

        await Assert.That((TimeSpan)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleDateTimeValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<DateTime>(
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
                name => name.IsEqualTo("System.DateTime"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.DateTime));

        await Assert.That((DateTime)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleUInt16Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ushort>(0xFE12);
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
                name => name.IsEqualTo("System.UInt16"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt16));

        await Assert.That((ushort)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleUInt32Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<uint>(0xFE12DC34);
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
                name => name.IsEqualTo("System.UInt32"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt32));

        await Assert.That((uint)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleUInt64Value()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.UInt64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ulong>(0xFE12DC34BA564321);
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
                name => name.IsEqualTo("System.UInt64"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt64));

        await Assert.That((ulong)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleStringValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<string>("ABC");
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
                name => name.IsEqualTo("System.String"))
            .And.Member(
                x => x.ClassInfo.Members[0].Type.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var item1 = node["Item1"];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.String));

        await Assert.That((string)item1).IsEqualTo(source.Item1);
    }

    [Test]
    public async Task ValueTupleObjectValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<object>(new object());
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
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ClassInfo.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());
    }

    [Test]
    public async Task ValueTupleValueTupleValue()
    {
        var sourceTypeName =
            "System.ValueTuple`1[[System.ValueTuple, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";
        var source = new ValueTuple<ValueTuple>(new ValueTuple());
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
                name => name.IsEqualTo("System.ValueTuple"))
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