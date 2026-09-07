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
    public async Task ValueTupleBooleanValue()
    {
        var source = new ValueTuple<bool>(true);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Boolean),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<bool>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleByteValue()
    {
        var source = new ValueTuple<byte>(0x87);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Byte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Byte),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<byte>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleCharValue()
    {
        var source = new ValueTuple<char>('X');
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Char, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Char),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<char>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleDecimalValue()
    {
        var source = new ValueTuple<decimal>(1234.5678m);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Decimal),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<decimal>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleDoubleValue()
    {
        var source = new ValueTuple<double>(1234.5678d);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Double),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<double>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleInt16Value()
    {
        var source = new ValueTuple<short>(-0x1234);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Int16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Int16),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<short>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleInt32Value()
    {
        var source = new ValueTuple<int>(-0x12345678);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Int32),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<int>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleInt64Value()
    {
        var source = new ValueTuple<long>(-0x1234567812345678);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Int64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Int64),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<long>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleSByteValue()
    {
        var source = new ValueTuple<sbyte>(-0x40);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.SByte, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.SByte),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<sbyte>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleSingleValue()
    {
        var source = new ValueTuple<float>(123.456f);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Single, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Single),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<float>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleTimeSpanValue()
    {
        var source = new ValueTuple<TimeSpan>(TimeSpan.FromSeconds(365));
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.TimeSpan, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.TimeSpan),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<TimeSpan>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleDateTimeValue()
    {
        var source = new ValueTuple<DateTime>(
            DateTime.ParseExact(
                "2026-05-15T11:27:23.3012265Z",
                "O",
                CultureInfo.InvariantCulture));
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.DateTime),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<DateTime>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleUInt16Value()
    {
        var source = new ValueTuple<ushort>(0xFE12);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.UInt16, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.UInt16),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<ushort>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleUInt32Value()
    {
        var source = new ValueTuple<uint>(0xFE12DC34);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.UInt32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.UInt32),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<uint>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleUInt64Value()
    {
        var source = new ValueTuple<ulong>(0xFE12DC34BA564321);
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.UInt64, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.UInt64),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<ulong>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleStringValue()
    {
        var source = new ValueTuple<string>("ABC");
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.String),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = source.Item1,
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<string>>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ValueTupleObjectValue()
    {
        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", NrbfMemberType.Object),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = new NrbfClass(new NrbfClassInfo(NrbfMemberType.Object, [])),
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<object>>()
            .And.Member(
                x => x.Item1,
                item1 => item1.IsNotNull());
    }

    [Test]
    public async Task ValueTupleValueTupleValue()
    {
        var source = new ValueTuple<ValueTuple>(new ValueTuple());

        var valueTupleType = new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary);

        var type = new NrbfMemberType(
            "System.ValueTuple`1[[System.ValueTuple, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            NrbfLibrary.SystemLibrary);
        var members = new[]
        {
            new NrbfClassMember("Item1", valueTupleType),
        };
        var info = new NrbfClassInfo(type, members);
        var node = new NrbfClass(info)
        {
            ["Item1"] = new NrbfClass(new NrbfClassInfo(valueTupleType, [])),
        };

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple<ValueTuple>>()
            .And.IsEqualTo(source);
    }
}