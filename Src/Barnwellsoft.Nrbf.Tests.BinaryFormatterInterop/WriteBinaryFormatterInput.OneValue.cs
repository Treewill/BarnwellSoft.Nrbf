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
    public async Task BooleanValue()
    {
        NrbfNode node = true;
        var actual = ReadWithBinaryFormatter(node);
        await Assert.That(actual).IsTypeOf<bool>()
            .And.IsTrue();
    }



    [Test]
    public async Task ByteValue()
    {
        byte source = 0x87;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<byte>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task CharValue()
    {
        var source = 'X';
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<char>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task DecimalValue()
    {
        var source = 1234.5678m;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<decimal>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task DoubleValue()
    {
        var source = 1234.5678d;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<double>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task Int16Value()
    {
        short source = -0x1234;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<short>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task Int32Value()
    {
        var source = -0x12345678;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<int>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task Int64Value()
    {
        var source = -0x1234567812345678;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<long>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task SByteValue()
    {
        sbyte source = -0x40;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<sbyte>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task SingleValue()
    {
        var source = 123.456f;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<float>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task TimeSpanValue()
    {
        var source = TimeSpan.FromSeconds(365);
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<TimeSpan>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task DateTimeValue()
    {
        var source = DateTime.ParseExact(
            "2026-05-15T11:27:23.3012265Z",
            "O",
            CultureInfo.InvariantCulture);
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<DateTime>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task UInt16Value()
    {
        ushort source = 0xFE12;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ushort>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task UInt32Value()
    {
        uint source = 0xFE12DC34;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<uint>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task UInt64Value()
    {
        var source = 0xFE12DC34BA564321;
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ulong>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task StringValue()
    {
        var source = "ABC";
        NrbfNode node = source;

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<string>()
            .And.IsEqualTo(source);
    }

    [Test]
    public async Task ObjectValue()
    {
        var info = new NrbfClassInfo(NrbfMemberType.Object, []);
        var node = new NrbfClass(info);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object>();
    }

    [Test]
    public async Task ValueTupleValue()
    {
        var type = new NrbfMemberType(
            "System.ValueTuple",
            NrbfLibrary.SystemLibrary);

        var info = new NrbfClassInfo(type, []);
        var node = new NrbfClass(info);

        var actual = ReadWithBinaryFormatter(node);
        await Assert.That(actual).IsTypeOf<ValueTuple>()
            .And.IsEqualTo(new ValueTuple());
    }
}