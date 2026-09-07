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

public partial class ReadBinaryFormatterOutput
{
    [Test]
    public async Task BooleanValue()
    {
        var source = true;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Boolean);
        await Assert.That((bool)node).IsEqualTo(source);
    }

    [Test]
    public async Task ByteValue()
    {
        byte source = 0x87;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Byte);
        await Assert.That((byte)node).IsEqualTo(source);
    }

    [Test]
    public async Task CharValue()
    {
        var source = 'X';
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Char);
        await Assert.That((char)node).IsEqualTo(source);
    }

    [Test]
    public async Task DecimalValue()
    {
        var source = 1234.5678m;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Decimal);
        await Assert.That((decimal)node).IsEqualTo(source);
    }

    [Test]
    public async Task DoubleValue()
    {
        var source = 1234.5678d;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Double);
        await Assert.That((double)node).IsEqualTo(source);
    }

    [Test]
    public async Task Int16Value()
    {
        short source = -0x1234;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Int16);
        await Assert.That((short)node).IsEqualTo(source);
    }

    [Test]
    public async Task Int32Value()
    {
        var source = -0x12345678;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Int32);
        await Assert.That((int)node).IsEqualTo(source);
    }

    [Test]
    public async Task Int64Value()
    {
        var source = -0x1234567812345678;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Int64);
        await Assert.That((long)node).IsEqualTo(source);
    }

    [Test]
    public async Task SByteValue()
    {
        sbyte source = -0x40;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.SByte);
        await Assert.That((sbyte)node).IsEqualTo(source);
    }

    [Test]
    public async Task SingleValue()
    {
        var source = 123.456f;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.Single);
        await Assert.That((float)node).IsEqualTo(source);
    }

    [Test]
    public async Task TimeSpanValue()
    {
        var source = TimeSpan.FromSeconds(365);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.TimeSpan);
        await Assert.That((TimeSpan)node).IsEqualTo(source);
    }

    [Test]
    public async Task DateTimeValue()
    {
        var source = DateTime.ParseExact(
            "2026-05-15T11:27:23.3012265Z",
            "O",
            CultureInfo.InvariantCulture);
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.DateTime);
        await Assert.That((DateTime)node).IsEqualTo(source);
    }

    [Test]
    public async Task UInt16Value()
    {
        ushort source = 0xFE12;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.UInt16);
        await Assert.That((ushort)node).IsEqualTo(source);
    }

    [Test]
    public async Task UInt32Value()
    {
        uint source = 0xFE12DC34;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.UInt32);
        await Assert.That((uint)node).IsEqualTo(source);
    }

    [Test]
    public async Task UInt64Value()
    {
        var source = 0xFE12DC34BA564321;
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.UInt64);
        await Assert.That((ulong)node).IsEqualTo(source);
    }

    [Test]
    public async Task StringValue()
    {
        var source = "ABC";
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node.Type).IsEqualTo(NrbfNodeType.String);
        await Assert.That((string)node).IsEqualTo(source);
    }

    [Test]
    public async Task ObjectValue()
    {
        var source = new object();
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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
    public async Task ValueTupleValue()
    {
        var source = new ValueTuple();
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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