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
    public async Task ArrayOfArrayOfBooleanValues()
    {
        var source = new[] { new[] { true, false, true } };
        var elementType = NrbfMemberType.Boolean;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<bool[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfByteValues()
    {
        var source = new[] { new byte[] { 0xA0, 0x05, 0xFF } };
        var elementType = NrbfMemberType.Byte;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<byte[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfCharValues()
    {
        var source = new[] { new char[] { 'X', 'y', 'Z' } };
        var elementType = NrbfMemberType.Char;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<char[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfDecimalValues()
    {
        var source = new[] { new decimal[] { 123.45m, -234.56m, 345.67m } };
        var elementType = NrbfMemberType.Decimal;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<decimal[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfDoubleValues()
    {
        var source = new[] { new double[] { 123.45, -234.56, 345.67 } };
        var elementType = NrbfMemberType.Double;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<double[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfInt16Values()
    {
        var source = new[] { new short[] { 0x700E, -0x1234, 0x4321 } };
        var elementType = NrbfMemberType.Int16;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<short[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfInt32Values()
    {
        var source = new[] { new int[] { 0x7F1234FE, -0x12345678, 0x13572468 } };
        var elementType = NrbfMemberType.Int32;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<int[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfInt64Values()
    {
        var source = new[] { new long[] { 0x1234567812345678, -0x1357246813572468, 0x7FFF12345678FFFE } };
        var elementType = NrbfMemberType.Int64;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<long[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfSByteValues()
    {
        var source = new[] { new sbyte[] { 0x7E, -0x10, 0x5A } };
        var elementType = NrbfMemberType.SByte;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<sbyte[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfSingleValues()
    {
        var source = new[] { new float[] { 123.45f, -234.56f, 345.67f } };
        var elementType = NrbfMemberType.Single;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<float[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfTimeSpanValues()
    {
        var source = new[]
        {
            new TimeSpan[]
            {
                TimeSpan.FromSeconds(90),
                TimeSpan.FromMinutes(90),
                TimeSpan.FromHours(90),
            },
        };
        var elementType = NrbfMemberType.TimeSpan;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<TimeSpan[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfDateTimeValues()
    {
        var source = new[]
        {
            new DateTime[]
            {
                DateTime.ParseExact("2026-05-15T11:27:23.1234567Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2016-03-20T13:33:20.2345678Z", "O", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2006-01-25T09:24:26.3456789Z", "O", CultureInfo.InvariantCulture),
            },
        };
        var elementType = NrbfMemberType.DateTime;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<DateTime[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfUInt16Values()
    {
        var source = new[] { new ushort[] { 0x8080, 0x1234, 0xFEDC } };
        var elementType = NrbfMemberType.UInt16;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ushort[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfUInt32Values()
    {
        var source = new[] { new uint[] { 0x80706050, 0x12345678, 0xFEDCBA98 } };
        var elementType = NrbfMemberType.UInt32;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<uint[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfUInt64Values()
    {
        var source = new[] { new ulong[] { 0x8070605040302010, 0x1234567812345678, 0xFEDCBA98FEDCBA98 } };
        var elementType = NrbfMemberType.UInt64;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ulong[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfStringValues()
    {
        var source = new[] { new string[] { "ABC", "DEF", "XYZ" } };
        var elementType = NrbfMemberType.String;
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(value);

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<string[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }

    [Test]
    public async Task ArrayOfArrayOfObjectValues()
    {
        var source = new[] { new object[] { new object(), new object(), new object() } };
        var elementType = NrbfMemberType.Object;
        var elementClassInfo = new NrbfClassInfo(elementType, []);
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(new NrbfClass(elementClassInfo));

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<object?[][]>()
            .And.Member(
                x => x[0],
                item =>
                    item.Count().IsEqualTo(3)
                        .And.All(x => x is not null));
    }

    [Test]
    public async Task ArrayOfArrayOfValueTupleValues()
    {
        var source = new[] { new ValueTuple[3] };
        var elementType = new NrbfMemberType("System.ValueTuple", NrbfLibrary.SystemLibrary);
        var elementClassInfo = new NrbfClassInfo(elementType, []);
        var innerNode = new NrbfArray(elementType);
        var node = new NrbfArray(elementType.GetArray())
        {
            innerNode,
        };
        foreach (var value in source[0])
            innerNode.Add(new NrbfClass(elementClassInfo));

        var actual = ReadWithBinaryFormatter(node);

        await Assert.That(actual).IsTypeOf<ValueTuple[][]>()
            .And.Member(
                x => x[0],
                item => item.IsEquivalentTo(source[0], CollectionOrdering.Matching));
    }
}