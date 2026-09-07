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

namespace Barnwellsoft.Nrbf.Tests;

public class NrbfValueTests
{
    [Test]
    public async Task BooleanConversion()
    {
        NrbfNode node = true;
        await Assert.That((bool)node).EqualTo(true);
    }

    [Test]
    public async Task ByteConversion()
    {
        NrbfNode node = byte.MaxValue;
        await Assert.That((byte)node).EqualTo(byte.MaxValue);
    }

    [Test]
    public async Task CharConversion()
    {
        NrbfNode node = 'Z';
        await Assert.That((char)node).EqualTo('Z');
    }

    [Test]
    public async Task DecimalConversion()
    {
        NrbfNode node = decimal.MaxValue;
        await Assert.That((decimal)node).EqualTo(decimal.MaxValue);
    }

    [Test]
    public async Task DoubleConversion()
    {
        NrbfNode node = double.MaxValue;
        await Assert.That((double)node).EqualTo(double.MaxValue);
    }

    [Test]
    public async Task Int16Conversion()
    {
        NrbfNode node = short.MinValue;
        await Assert.That((short)node).EqualTo(short.MinValue);
    }

    [Test]
    public async Task Int32Conversion()
    {
        NrbfNode node = int.MinValue;
        await Assert.That((int)node).EqualTo(int.MinValue);
    }

    [Test]
    public async Task Int64Conversion()
    {
        NrbfNode node = long.MinValue;
        await Assert.That((long)node).EqualTo(long.MinValue);
    }

    [Test]
    public async Task SByteConversion()
    {
        NrbfNode node = sbyte.MinValue;
        await Assert.That((sbyte)node).EqualTo(sbyte.MinValue);
    }

    [Test]
    public async Task SingleConversion()
    {
        NrbfNode node = float.MaxValue;
        await Assert.That((float)node).EqualTo(float.MaxValue);
    }

    [Test]
    public async Task TimeSpanConversion()
    {
        NrbfNode node = TimeSpan.MaxValue;
        await Assert.That((TimeSpan)node).EqualTo(TimeSpan.MaxValue);
    }

    [Test]
    public async Task DateTimeConversion()
    {
        NrbfNode node = DateTime.MaxValue;
        await Assert.That((DateTime)node).EqualTo(DateTime.MaxValue);
    }

    [Test]
    public async Task UInt16Conversion()
    {
        NrbfNode node = ushort.MaxValue;
        await Assert.That((ushort)node).EqualTo(ushort.MaxValue);
    }

    [Test]
    public async Task UInt32Conversion()
    {
        NrbfNode node = uint.MaxValue;
        await Assert.That((uint)node).EqualTo(uint.MaxValue);
    }

    [Test]
    public async Task UInt64Conversion()
    {
        NrbfNode node = ulong.MaxValue;
        await Assert.That((ulong)node).EqualTo(ulong.MaxValue);
    }

    [Test]
    public async Task StringConversion()
    {
        NrbfNode node = "abc";
        await Assert.That((string)node).EqualTo("abc");
    }

    [Test]
    public async Task NullStringConversion()
    {
        string? nil = null;
        NrbfNode node = nil;
        await Assert.That(node)
            .IsNull();
    }

    [Test]
    public async Task ConstructNullStringValue()
    {
        string? nil = null;
        await Assert.That(() => new NrbfValue(nil))
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task BooleanConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (bool)node)
            .Throws<InvalidCastException>();
    }


    [Test]
    public async Task ByteConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (byte)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task CharConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (char)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task DecimalConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (decimal)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task DoubleConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (double)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task Int16ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (short)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task Int32ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (int)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task Int64ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (long)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task SByteConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (sbyte)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task SingleConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (float)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task TimeSpanConversionInvalid()
    {
        NrbfNode node = false;
        await Assert.That(() => _ = (TimeSpan)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task DateTimeConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (DateTime)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task UInt16ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (ushort)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task UInt32ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (uint)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task UInt64ConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (ulong)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task StringConversionInvalid()
    {
        NrbfNode node = TimeSpan.FromMinutes(42);
        await Assert.That(() => _ = (string)node)
            .Throws<InvalidCastException>();
    }

    [Test]
    public async Task EqualsWhenReferenceEquals()
    {
        NrbfNode node = "42";
        var same = node;
        await Assert.That(node.Equals(same)).IsTrue();
    }

    [Test]
    public async Task GetItem() =>
        await Assert.That(() => new NrbfValue("42")["Value"])
            .Throws<InvalidOperationException>();

    [Test]
    public async Task SetItem() =>
        await Assert.That(() => new NrbfValue("42")["Value"] = "0")
            .Throws<InvalidOperationException>();

    [Test]
    public async Task GetHashCodeInvoke() =>
        await Assert.That(() => new NrbfValue("42").GetHashCode())
            .ThrowsNothing();

    [Test]
    public async Task ConvertToBoolean()
    {
        var value = true;
        await Assert.That(Convert.ToBoolean(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToByte()
    {
        byte value = 42;
        await Assert.That(Convert.ToByte(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToChar()
    {
        var value = 'Z';
        await Assert.That(Convert.ToChar(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToDecimal()
    {
        var value = 42m;
        await Assert.That(Convert.ToDecimal(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToDouble()
    {
        var value = 42.0;
        await Assert.That(Convert.ToDouble(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToInt16()
    {
        short value = 42;
        await Assert.That(Convert.ToInt16(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToInt32()
    {
        var value = 42;
        await Assert.That(Convert.ToInt32(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToInt64()
    {
        long value = 42;
        await Assert.That(Convert.ToInt64(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToSByte()
    {
        sbyte value = 42;
        await Assert.That(Convert.ToSByte(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToSingle()
    {
        var value = 42.0f;
        await Assert.That(Convert.ToSingle(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToDateTime()
    {
        var value = DateTime.MaxValue;
        await Assert.That(Convert.ToDateTime(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToUInt16()
    {
        ushort value = 42;
        await Assert.That(Convert.ToUInt16(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToUInt32()
    {
        uint value = 42;
        await Assert.That(Convert.ToUInt32(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToUInt64()
    {
        ulong value = 42;
        await Assert.That(Convert.ToUInt64(new NrbfValue(value)))
            .IsEqualTo(value);
    }

    [Test]
    public async Task ConvertToString()
    {
        var value = "42";
        await Assert.That(Convert.ToString(new NrbfValue(value), CultureInfo.InvariantCulture))
            .IsEqualTo(value);
    }

    [Test]
    public async Task GetTypeCode() =>
        await Assert.That(Convert.GetTypeCode(new NrbfValue(42)))
            .IsEqualTo(TypeCode.Int32);

    [Test]
    public async Task ToType()
    {
        var value = new NrbfValue(42);
        var converted = value.ToType(typeof(long), CultureInfo.CurrentCulture);
        await Assert.That(converted).IsEqualTo(42L);
    }
}
