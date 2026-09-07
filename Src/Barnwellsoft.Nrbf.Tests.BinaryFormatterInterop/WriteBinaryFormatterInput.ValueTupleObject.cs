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

public partial class WriteBinaryFormatterInput
{
    [Test]
    public async Task ValueTupleObjectWithBooleanValue()
    {
        var source = new ValueTuple<object>(true);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (bool)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithByteValue()
    {
        var source = new ValueTuple<object>((byte)0x87);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (byte)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithCharValue()
    {
        var source = new ValueTuple<object>('X');
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (char)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithDecimalValue()
    {
        var source = new ValueTuple<object>(1234.5678m);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (decimal)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);

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
        var source = new ValueTuple<object>(1234.5678d);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (double)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithInt16Value()
    {
        var source = new ValueTuple<object>((short)-0x1234);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (short)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithInt32Value()
    {
        var source = new ValueTuple<object>(-0x12345678);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (int)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithInt64Value()
    {
        var source = new ValueTuple<object>(-0x1234567812345678);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (long)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithSByteValue()
    {
        var source = new ValueTuple<object>((sbyte)-0x40);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (sbyte)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithSingleValue()
    {
        var source = new ValueTuple<object>(123.456f);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (float)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithTimeSpanValue()
    {
        var source = new ValueTuple<object>(TimeSpan.FromSeconds(365));
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (TimeSpan)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithDateTimeValue()
    {
        var source = new ValueTuple<object>(
            DateTime.ParseExact(
                "2026-05-15T11:27:23.3012265Z",
                "O",
                CultureInfo.InvariantCulture));
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (DateTime)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt16Value()
    {
        var source = new ValueTuple<object>((ushort)0xFE12);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (ushort)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt32Value()
    {
        var source = new ValueTuple<object>(0xFE12DC34);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (uint)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithUInt64Value()
    {
        var source = new ValueTuple<object>(0xFE12DC34BA564321);
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (ulong)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithStringValue()
    {
        var source = new ValueTuple<object>("ABC");
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = (string)source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectWithValueTupleValue()
    {
        var source = new ValueTuple<object>(new ValueTuple());
        var node = new NrbfClass(ValueTupleObjectClassInfo)
        {
            ["Item1"] = new NrbfClass(new NrbfClassInfo(new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary), [])),
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.IsEqualTo(source);
    }

    private static NrbfClassInfo ValueTupleObjectClassInfo =>
        new NrbfClassInfo(
            new NrbfMemberType(
                "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
                NrbfLibrary.SystemLibrary),
            [
                new NrbfClassMember("Item1", NrbfMemberType.Object),
            ]);
}