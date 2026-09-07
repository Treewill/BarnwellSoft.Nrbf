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
    public async Task ArrayOfBooleanValues()
    {
        var source = new[] { true, false, true };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (bool)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfByteValues()
    {
        var source = new byte[] { 0xA0, 0x05, 0xFF };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (byte)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfCharValues()
    {
        var source = new char[] { 'X', 'y', 'Z' };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (char)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfDecimalValues()
    {
        var source = new decimal[] { 123.45m, -234.56m, 345.67m };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (decimal)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfDoubleValues()
    {
        var source = new double[] { 123.45, -234.56, 345.67 };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (double)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt16Values()
    {
        var source = new short[] { 0x700E, -0x1234, 0x4321 };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (short)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt32Values()
    {
        var source = new int[] { 0x7F1234FE, -0x12345678, 0x13572468 };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (int)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt64Values()
    {
        var source = new long[] { 0x1234567812345678, -0x1357246813572468, 0x7FFF12345678FFFE };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (long)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfSByteValues()
    {
        var source = new sbyte[] { 0x7E, -0x10, 0x5A };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (sbyte)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfSingleValues()
    {
        var source = new float[] { 123.45f, -234.56f, 345.67f };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (float)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfTimeSpanValues()
    {
        var source = new TimeSpan[]
        {
            TimeSpan.FromSeconds(90),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromHours(90),
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
                name => name.IsEqualTo("System.TimeSpan"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (TimeSpan)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfDateTimeValues()
    {
        var source = new DateTime[]
        {
            DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2016-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
            DateTime.ParseExact("2006-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
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
                name => name.IsEqualTo("System.DateTime"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (DateTime)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt16Values()
    {
        var source = new ushort[] { 0x8080, 0x1234, 0xFEDC };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (ushort)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt32Values()
    {
        var source = new uint[] { 0x80706050, 0x12345678, 0xFEDCBA98 };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (uint)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt64Values()
    {
        var source = new ulong[] { 0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98 };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (ulong)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfStringValues()
    {
        var source = new string[] { "ABC", "DEF", "XYZ" };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.String"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(3));

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (string)x))
            .IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfObjectValues()
    {
        var source = new object[] { new object(), new object(), new object() };
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
                isSystem => isSystem.IsTrue())
            .And.Member(
                x => x.Count,
                count => count.IsEqualTo(3));
    }

    [Test]
    public async Task ArrayOfValueTupleValues()
    {
        var source = new ValueTuple[3];
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
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