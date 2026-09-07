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

public partial class WriteBinaryFormatterInput
{
    [Test]
    public async Task ArrayOfBooleanValues()
    {
        var source = new[] { true, false, true };
        var node = new NrbfArray(NrbfMemberType.Boolean);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<bool[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfByteValues()
    {
        var source = new byte[] { 0xA0, 0x05, 0xFF };
        var node = new NrbfArray(NrbfMemberType.Byte);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<byte[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfCharValues()
    {
        var source = new char[] { 'X', 'y', 'Z' };
        var node = new NrbfArray(NrbfMemberType.Char);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<char[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfDecimalValues()
    {
        var source = new decimal[] { 123.45m, -234.56m, 345.67m };
        var node = new NrbfArray(NrbfMemberType.Decimal);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<decimal[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfDoubleValues()
    {
        var source = new double[] { 123.45, -234.56, 345.67 };
        var node = new NrbfArray(NrbfMemberType.Double);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<double[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt16Values()
    {
        var source = new short[] { 0x700E, -0x1234, 0x4321 };
        var node = new NrbfArray(NrbfMemberType.Int16);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<short[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt32Values()
    {
        var source = new int[] { 0x7F1234FE, -0x12345678, 0x13572468 };
        var node = new NrbfArray(NrbfMemberType.Int32);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<int[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfInt64Values()
    {
        var source = new long[] { 0x1234567812345678, -0x1357246813572468, 0x7FFF12345678FFFE };
        var node = new NrbfArray(NrbfMemberType.Int64);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<long[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfSByteValues()
    {
        var source = new sbyte[] { 0x7E, -0x10, 0x5A };
        var node = new NrbfArray(NrbfMemberType.SByte);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<sbyte[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfSingleValues()
    {
        var source = new float[] { 123.45f, -234.56f, 345.67f };
        var node = new NrbfArray(NrbfMemberType.Single);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<float[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
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
        var node = new NrbfArray(NrbfMemberType.TimeSpan);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<TimeSpan[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
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
        var node = new NrbfArray(NrbfMemberType.DateTime);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<DateTime[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt16Values()
    {
        var source = new ushort[] { 0x8080, 0x1234, 0xFEDC };
        var node = new NrbfArray(NrbfMemberType.UInt16);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ushort[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt32Values()
    {
        var source = new uint[] { 0x80706050, 0x12345678, 0xFEDCBA98 };
        var node = new NrbfArray(NrbfMemberType.UInt32);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<uint[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfUInt64Values()
    {
        var source = new ulong[] { 0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98 };
        var node = new NrbfArray(NrbfMemberType.UInt64);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ulong[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfStringValues()
    {
        var source = new string[] { "ABC", "DEF", "XYZ" };
        var node = new NrbfArray(NrbfMemberType.String);
        foreach (var element in source)
            node.Add(element);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<string[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ArrayOfObjectValues()
    {
        var source = new object[] { new object(), new object(), new object() };
        var node = new NrbfArray(NrbfMemberType.Object);
        foreach (var element in source)
            node.Add(new NrbfClass(new NrbfClassInfo(NrbfMemberType.Object, [])));

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[]>()
            .And.Member(
                x => x.Length,
                length => length.IsEqualTo(3))
            .And.All<object?[], object?>(
                x => x is not null,
                "NotNull");
    }

    [Test]
    public async Task ArrayOfValueTupleValues()
    {
        var source = new ValueTuple[3];
        var valueTupleType = new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary);
        var valueTupleClassInfo = new NrbfClassInfo(valueTupleType, []);
        var node = new NrbfArray(valueTupleType);

        foreach (var element in source)
            node.Add(new NrbfClass(valueTupleClassInfo));

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple[]>()
            .And.IsEquivalentTo(source, CollectionOrdering.Matching);
    }
}