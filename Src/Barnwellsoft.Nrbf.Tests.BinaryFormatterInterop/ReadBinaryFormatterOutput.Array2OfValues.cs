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

namespace Barnwellsoft.Nrbf.Tests.BinaryFormatterInterop;

public partial class ReadBinaryFormatterOutput
{
    [Test]
    public async Task Array2OfBooleanValues()
    {
        var source = new bool[,]
        {
            { true, false, true },
            { false, true, false },
            { true, true, true },
            { false, false, false },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Boolean"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (bool)x)).IsEquivalentTo(source.Cast<bool>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfByteValues()
    {
        var source = new byte[,]
        {
            { 0xA1, 0x06, 0xFF },
            { 0xA2, 0x07, 0xFE },
            { 0xA3, 0x08, 0xFD },
            { 0xA4, 0x09, 0xFC }
        };

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Byte"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (byte)x)).IsEquivalentTo(source.Cast<byte>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfCharValues()
    {
        var source = new char[,]
        {
            { 'a', 'b', 'c' },
            { 'D', 'E', 'F' },
            { 'G', 'H', 'I' },
            { 'j', 'k', 'l' },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Char"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (char)x)).IsEquivalentTo(source.Cast<char>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfDecimalValues()
    {
        var source = new decimal[,]
        {
            { 123.45m, -234.56m, 345.67m },
            { 456.78m, -567.89m, 98.76m },
            { -87.65m, 76.54m, -65.43m },
            { -54.32m, 43.21m, -3.21m },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Decimal"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (decimal)x)).IsEquivalentTo(source.Cast<decimal>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfDoubleValues()
    {
        var source = new double[,]
        {
            { 123.45, -234.56, 345.67 },
            { 456.78, -567.89, 98.76 },
            { -87.65, 76.54, -65.43 },
            { -54.32, 43.21, -3.21 },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Double"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (double)x)).IsEquivalentTo(source.Cast<double>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfInt16Values()
    {
        var source = new short[,]
        {
            { 0x700E, -0x1234, 0x4321 },
            { 0x781E, -0x2345, 0x5432 },
            { -0x7D3E, 0x3456, -0x6543 },
            { -0x7E7E, 0x4567, -0x7654 },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int16"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (short)x)).IsEquivalentTo(source.Cast<short>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfInt32Values()
    {
        var source = new int[,]
        {
            { 0x7F1234FE, -0x12345678, 0x13572468 },
            { 0x7F2345FE, -0x23456789, 0x35792468 },
            { -0x7F3456FE, 0x3456789A, -0x3579468A },
            { -0x7F4567FE, 0x456789AB, -0x579B468A },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int32"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (int)x)).IsEquivalentTo(source.Cast<int>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfInt64Values()
    {
        var source = new long[,]
        {
            { 0x1234567812345678, -0x1357246813572468, 0x7FFF2345678FFFE },
            { 0x2345678123456789, -0x3579246835792468, 0x7FFF3456789FFFE },
            { -0x345678123456789A, 0x3579468A3579468A, 0x7FFF456789AFFFE },
            { -0x45678123456789AB, 0x579B468A579B468A, 0x7FFF56789ABFFFE },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Int64"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (long)x)).IsEquivalentTo(source.Cast<long>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfSByteValues()
    {
        var source = new sbyte[,]
        {
            { 0x7E, -0x10, 0x5A },
            { 0x3C, -0x20, 0x3B },
            { -0x18, 0x30, -0x1C },
            { -0x13, 0x40, -0x7F },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.SByte"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (sbyte)x)).IsEquivalentTo(source.Cast<sbyte>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfSingleValues()
    {
        var source = new float[,]
        {
            { 123.45f, -234.56f, 345.67f },
            { 456.78f, -567.89f, 98.76f },
            { -87.65f, 76.54f, -65.43f },
            { -54.32f, 43.21f, -3.21f },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Single"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (float)x)).IsEquivalentTo(source.Cast<float>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfTimeSpanValues()
    {
        var source = new TimeSpan[,]
        {
            {
                TimeSpan.FromSeconds(90),
                TimeSpan.FromMinutes(90),
                TimeSpan.FromHours(90),
            },

            {
                TimeSpan.FromSeconds(91),
                TimeSpan.FromMinutes(91),
                TimeSpan.FromHours(91),
            },

            {
                TimeSpan.FromSeconds(92),
                TimeSpan.FromMinutes(92),
                TimeSpan.FromHours(92),
            },

            {
                TimeSpan.FromSeconds(93),
                TimeSpan.FromMinutes(93),
                TimeSpan.FromHours(93),
            },
        };

        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.TimeSpan"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (TimeSpan)x)).IsEquivalentTo(source.Cast<TimeSpan>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfDateTimeValues()
    {
        var source = new DateTime[,]
        {
            {
                DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2016-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2006-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
            },

            {
                DateTime.ParseExact("2023-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2013-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2003-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
            },

            {
                DateTime.ParseExact("2020-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2010-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2000-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
            },

            {
                DateTime.ParseExact("2025-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2015-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2005-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
            },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.DateTime"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (DateTime)x)).IsEquivalentTo(source.Cast<DateTime>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfUInt16Values()
    {
        var source = new ushort[,]
        {
            { 0x8080, 0x1234, 0xFEDC },
            { 0x9080, 0x2345, 0xEDCB },
            { 0xA080, 0x3456, 0xDCBA },
            { 0xB080, 0x4567, 0xCBA9 },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt16"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (ushort)x)).IsEquivalentTo(source.Cast<ushort>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfUInt32Values()
    {
        var source = new uint[,]
        {
            { 0x80706050, 0x12345678, 0xFEDCBA98 },
            { 0x80607050, 0x23456789, 0xEDCBA987 },
            { 0x80507060, 0x3456789A, 0xDCBA9876 },
            { 0x60508070, 0x456789AB, 0xCBA98765 },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt32"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (uint)x)).IsEquivalentTo(source.Cast<uint>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfUInt64Values()
    {
        var source = new ulong[,]
        {
            { 0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98 },
            { 0x4030201080706050, 0x2345678923456789, 0xEDCBA987EDCBA987 },
            { 0x8010702060305040, 0x3456789A3456789A, 0xDCBA9876DCBA9876 },
            { 0x6030504080107020, 0x456789AB456789AB, 0xCBA98765CBA98765 },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.UInt64"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (ulong)x)).IsEquivalentTo(source.Cast<ulong>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfStringValues()
    {
        var source = new string[,]
        {
            { "abc", "def", "xyz" },
            { "ABC", "DEF", "XYZ" },
            { "123", "456", "789" },
            { "!@#", "$%^", "&*+" },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.String"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        await Assert.That(array.Select(x => (string)x)).IsEquivalentTo(source.Cast<string>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Array2OfObjectValues()
    {
        var source = new object[,]
        {
            { new object(), new object(), new object() },
            { new object(), new object(), new object() },
            { new object(), new object(), new object() },
            { new object(), new object(), new object() },
        };
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.Object"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        var element = array.First();
        await Assert.That(element)
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
    public async Task Array2OfValueTupleValues()
    {
        var source = new ValueTuple[4, 3];
        var reader = new NrbfMessageReader();
        var node = reader.ReadMessage(GetSerializedStream(source)).Root;

        await Assert.That(node)
            .Member(
                x => x.Type,
                type => type.IsEqualTo(NrbfNodeType.Array))
            .And.IsTypeOf<NrbfArray>()
            .And.Member(
                x => x.Rank,
                name => name.IsEqualTo(2))
            .And.Member(
                x => x.Dimensions[0].Length,
                length => length.IsEqualTo(4))
            .And.Member(
                x => x.Dimensions[1].Length,
                length => length.IsEqualTo(3))
            .And.Member(
                x => x.ElementType.ClassName,
                name => name.IsEqualTo("System.ValueTuple"))
            .And.Member(
                x => x.ElementType.Library.IsSystemLibrary,
                isSystem => isSystem.IsTrue());

        var array = (NrbfArray)node;
        var element = array.First();
        await Assert.That(element)
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