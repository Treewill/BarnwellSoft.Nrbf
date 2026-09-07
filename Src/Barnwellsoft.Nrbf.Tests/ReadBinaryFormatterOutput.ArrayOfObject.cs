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
    public async Task ArrayOfObjectWithBooleanValue()
    {
        var source = new object[] { true };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Boolean));

        await Assert.That((bool)item1).IsEqualTo((bool)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithByteValue()
    {
        var source = new object[] { (byte)0x87 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Byte));

        await Assert.That((byte)item1).IsEqualTo((byte)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithCharValue()
    {
        var source = new object[] { 'X' };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Char));

        await Assert.That((char)item1).IsEqualTo((char)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithDecimalValue()
    {
        var source = new object[] { 1234.5678m };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Decimal));

        await Assert.That((decimal)item1).IsEqualTo((decimal)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithDoubleValue()
    {
        var source = new object[] { 1234.5678d };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Double));

        await Assert.That((double)item1).IsEqualTo((double)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithInt16Value()
    {
        var source = new object[] { (short)-0x1234 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int16));

        await Assert.That((short)item1).IsEqualTo((short)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithInt32Value()
    {
        var source = new object[] { -0x12345678 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int32));

        await Assert.That((int)item1).IsEqualTo((int)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithInt64Value()
    {
        var source = new object[] { -0x1234567812345678 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Int64));

        await Assert.That((long)item1).IsEqualTo((long)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithSByteValue()
    {
        var source = new object[] { (sbyte)-0x40 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.SByte));

        await Assert.That((sbyte)item1).IsEqualTo((sbyte)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithSingleValue()
    {
        var source = new object[] { 123.456f };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Single));

        await Assert.That((float)item1).IsEqualTo((float)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithTimeSpanValue()
    {
        var source = new object[] { TimeSpan.FromSeconds(365) };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.TimeSpan));

        await Assert.That((TimeSpan)item1).IsEqualTo((TimeSpan)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithDateTimeValue()
    {
        var source = new object[]
        {
            DateTime.ParseExact(
                "2026-05-15T11:27:23.3012265Z",
                "O",
                CultureInfo.InvariantCulture)
        };

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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.DateTime));

        await Assert.That((DateTime)item1).IsEqualTo((DateTime)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt16Value()
    {
        var source = new object[] { (ushort)0xFE12 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt16));

        await Assert.That((ushort)item1).IsEqualTo((ushort)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt32Value()
    {
        var source = new object[] { 0xFE12DC34 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt32));

        await Assert.That((uint)item1).IsEqualTo((uint)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithUInt64Value()
    {
        var source = new object[] { 0xFE12DC34BA564321 };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.UInt64));

        await Assert.That((ulong)item1).IsEqualTo((ulong)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithStringValue()
    {
        var source = new object[] { "ABC" };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
        await Assert.That(item1)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.String));

        await Assert.That((string)item1).IsEqualTo((string)source[0]);
    }

    [Test]
    public async Task ArrayOfObjectWithValueTupleValue()
    {
        var source = new object[] { new ValueTuple() };
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
                isSystem => isSystem.IsTrue());

        var item1 = node[0];
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