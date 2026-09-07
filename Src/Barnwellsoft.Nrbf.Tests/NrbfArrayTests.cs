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

using System.Collections.ObjectModel;
using TUnit.Assertions.Enums;

namespace Barnwellsoft.Nrbf.Tests;

[NotInParallel]
public class NrbfArrayTests
{
    [Test]
    public async Task NewSingleArrayNullElementError() =>
        await Assert.That(() => new NrbfArray(null))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task NewNullElementError() =>
        await Assert.That(() => new NrbfArray(null, [new NrbfArrayDimension(1)]))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task NewNullDimensionsError() =>
        await Assert.That(() => new NrbfArray(NrbfMemberType.Object, null))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task NewNullDimensionError() =>
        await Assert.That(() => new NrbfArray(NrbfMemberType.Object, [null]))
            .Throws<ArgumentException>();

    [Test]
    public async Task NewNoDimensionError() =>
        await Assert.That(() => new NrbfArray(NrbfMemberType.Object, []))
            .Throws<ArgumentException>();

    [Test]
    public async Task Rank()
    {
        var array = CreateExample2DArray();
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Rank)
            .IsEqualTo(2);
    }

    [Test]
    [MatrixDataSource]
    public async Task Index(
        [Matrix(2, 3, 4)]
        int x,

        [Matrix(5, 6)]
        int y)
    {
        var array = CreateExample2DArray();
        var value = $"{x * 10 + y}";
        array[x, y] = value;

        await Assert.That(array[x, y]).IsEqualTo(value);
    }

    [Test]
    public async Task GetEnumerator2D()
    {
        var nrbfArray = CreateExample2DArray();
        var array = Array.CreateInstance(typeof(string), [3, 2], [2, 5]);

        for (var x = 2; x < 5; ++x)
        {
            for (var y = 5; y < 7; ++y)
            {
                var value = $"{x * 10 + y}";
                nrbfArray[x, y] = value;
                array.SetValue(value, x, y);
            }
        }

        var nrbfArrayValues = new List<NrbfNode?>();
        foreach (var value in nrbfArray)
            nrbfArrayValues.Add(value);

        var arrayValues = new List<NrbfNode?>();
        foreach (string value in array)
            arrayValues.Add(value);

        await Assert.That(nrbfArrayValues)
            .IsEquivalentTo(arrayValues, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetEnumerator3D()
    {
        var nrbfArray = new NrbfArray(
            NrbfMemberType.String,
            [
                new NrbfArrayDimension(6, 1),
                new NrbfArrayDimension(6, 2),
                new NrbfArrayDimension(6, 3),
            ]);

        var array = Array.CreateInstance(typeof(string), [6, 6, 6], [1, 2, 3]);

        nrbfArray[3, 4, 5] = "345";
        array.SetValue("345", 3, 4, 5);

        var nrbfArrayValues = new List<string?>();
        foreach (var value in nrbfArray) 
            nrbfArrayValues.Add((string?)value);

        var arrayValues = new List<string?>();
        foreach (string? value in array) 
            arrayValues.Add(value);

        await Assert.That(nrbfArrayValues)
            .IsEquivalentTo(arrayValues, CollectionOrdering.Matching);
    }

    [Test]
    public async Task BooleanDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Boolean);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(false));
    }

    [Test]
    public async Task GetEnumeratorEmptyDimension()
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(10),
            new NrbfArrayDimension(10),
            new NrbfArrayDimension(0),
        };

        var array = new NrbfArray(NrbfMemberType.Int32, dimensions);
        var enumerator = ((System.Collections.IEnumerable)array).GetEnumerator();
        try
        {
            await Assert.That(enumerator.MoveNext()).IsFalse();
        }
        finally
        {
            (enumerator as IDisposable)?.Dispose();
        }

    }

    [Test]
    public async Task ByteDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Byte);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue((byte)0));
    }

    [Test]
    public async Task CharDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Char);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue('\0'));
    }

    [Test]
    public async Task DecimalDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Decimal);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0m));
    }

    [Test]
    public async Task DoubleDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Double);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0.0d));
    }

    [Test]
    public async Task Int16DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Int16);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue((short)0));
    }

    [Test]
    public async Task Int32DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Int32);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0));
    }

    [Test]
    public async Task Int64DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Int64);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0L));
    }

    [Test]
    public async Task SByteDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.SByte);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue((sbyte)0));
    }

    [Test]
    public async Task SingleDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.Single);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0.0f));
    }

    [Test]
    public async Task TimeSpanDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.TimeSpan);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(TimeSpan.Zero));
    }

    [Test]
    public async Task DateTimeDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.DateTime);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(default(DateTime)));
    }

    [Test]
    public async Task UInt16DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.UInt16);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue((ushort)0));
    }

    [Test]
    public async Task UInt32DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.UInt32);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0U));
    }

    [Test]
    public async Task UInt64DefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.UInt64);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsEqualTo(new NrbfValue(0UL));
    }

    [Test]
    public async Task StringDefaultValue()
    {
        var array = CreateSingleElementExample2DArray(NrbfMemberType.String);
        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[0, 0])
            .IsNull();
    }

    [Test]
    [MethodDataSource(nameof(Samples1D))]
    public async Task Add(ReadOnlyCollection<string?> values)
    {
        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(values.Count);

        await Assert.That(array)
            .IsEquivalentTo(values.Select(x => (NrbfNode)x), CollectionOrdering.Matching);
    }

    [Test]
    public async Task AddTypeMismatch()
    {
        var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2 };
        await Assert.That(() => array.Add("Z"))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task AddHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().Add("Test"))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task GetEnumerator()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };

        using var enumerator = array.GetEnumerator();
        using var unused = Assert.Multiple();

        var move = enumerator.MoveNext();
        await Assert.That(move).IsTrue();

        if (!move)
            return;


        var current = enumerator.Current;
        await Assert.That(current).IsEqualTo("0");

        move = enumerator.MoveNext();
        await Assert.That(move).IsTrue();

        if (!move)
            return;


        current = enumerator.Current;
        await Assert.That(current).IsEqualTo("1");

        move = enumerator.MoveNext();
        await Assert.That(move).IsTrue();

        if (!move)
            return;


        current = enumerator.Current;
        await Assert.That(current).IsEqualTo("2");

        move = enumerator.MoveNext();
        await Assert.That(move).IsFalse();
    }

    [Test]
    [CombinedDataSources]
    public async Task GetItem(
        [MethodDataSource(nameof(SamplesIndexes))]
        int index,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[index])
            .IsEqualTo(values[index]);
    }

    [Test]
    [CombinedDataSources]
    public async Task SetItem(
        [MethodDataSource(nameof(SamplesIndexes))]
        int index,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        array[index] = "NewValue";

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(values.Count);

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[index])
            .IsEqualTo("NewValue");
    }

    [Test]
    [CombinedDataSources]
    public async Task SetItemNull(
        [MethodDataSource(nameof(SamplesIndexes))]
        int index,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        array[index] = null;

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(values.Count);

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x[index])
            .IsNull();
    }

    [Test]
    public async Task Clear()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };
        array.Clear();

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(0);

        await Assert.That(array.Select(_ => 0)).IsEmpty();
    }

    [Test]
    public async Task ClearHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().Clear())
            .Throws<InvalidOperationException>();

    [Test]
    [CombinedDataSources]
    public async Task Insert(
        [MethodDataSource(nameof(SamplesInsertionIndexes))]
        int index,

        [Arguments("X", null)]
        string? item,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var collection = new List<NrbfNode>(
            values.Select(x => (NrbfNode)x));

        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        collection.Insert(index, item);
        array.Insert(index, item);

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(collection.Count);

        await Assert.That(array)
            .IsEquivalentTo(collection, CollectionOrdering.Matching);
    }

    [Test]
    public async Task InsertTypeMismatch()
    {
        var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2 };
        await Assert.That(() => array.Insert(0, "Z"))
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task InsertHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().Insert(0, "Test"))
            .Throws<InvalidOperationException>();

    [Test]
    [CombinedDataSources]
    public async Task RemoveAt(
        [MethodDataSource(nameof(SamplesIndexes))]
        int index,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var collection = new List<NrbfNode>(
            values.Select(x => (NrbfNode)x));

        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        collection.RemoveAt(index);
        array.RemoveAt(index);

        using var unused = Assert.Multiple();

        await Assert.That<NrbfArray>(array)
            .HasProperty(x => x.Count)
            .IsEqualTo(collection.Count);

        await Assert.That(array)
            .IsEquivalentTo(collection, CollectionOrdering.Matching);
    }

    [Test]
    public async Task RemoveAtHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().RemoveAt(0))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task IndexOfSample()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };

        using var unused = Assert.Multiple();

        await Assert.That(array.IndexOf("1")).IsEqualTo(1);
        await Assert.That(array.IndexOf("0")).IsEqualTo(0);
        await Assert.That(array.IndexOf("2")).IsEqualTo(2);
        await Assert.That(array.IndexOf("3")).IsEqualTo(-1);
        await Assert.That(array.IndexOf(null)).IsEqualTo(-1);
    }

    [Test]
    [CombinedDataSources]
    public async Task IndexOf(
        [MethodDataSource(nameof(SampleSearchValues))]
        string? item,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var collection = new List<NrbfNode>(
            values.Select(x => (NrbfNode)x));

        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        var arrayIndex = array.IndexOf(item);
        var collectionIndex = collection.IndexOf(item);

        await Assert.That(arrayIndex)
            .IsEqualTo(collectionIndex);
    }

    [Test]
    public async Task IndexOfTypeMismatch()
    {
        var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2 };
        await Assert.That(array.IndexOf("Z"))
            .IsEqualTo(-1);
    }

    [Test]
    public async Task IndexOfHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().IndexOf("Test"))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task ContainsSample()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };

        using var unused = Assert.Multiple();

        await Assert.That(array.Contains("1")).IsTrue();
        await Assert.That(array.Contains("0")).IsTrue();
        await Assert.That(array.Contains("2")).IsTrue();
        await Assert.That(array.Contains("3")).IsFalse();
        await Assert.That(array.Contains(null)).IsFalse();
    }

    [Test]
    [CombinedDataSources]
    public async Task Contains(
        [MethodDataSource(nameof(SampleSearchValues))]
        string? item,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var collection = new List<NrbfNode>(
            values.Select(x => (NrbfNode)x));

        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        var arrayContains = array.Contains(item);
        var collectionContains = collection.Contains(item);

        await Assert.That(arrayContains)
            .IsEqualTo(collectionContains);
    }

    [Test]
    public async Task ContainsTypeMismatch()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };
        await Assert.That(array.Contains(1))
            .IsFalse();
    }

    [Test]
    public async Task RemoveSample()
    {
        var array = new NrbfArray(NrbfMemberType.String) { "0", "1", "2" };

        var nonElementRemoved = array.Remove("X");
        var elementRemoved = array.Remove("1");

        using var unused = Assert.Multiple();

        await Assert.That(nonElementRemoved).IsFalse();
        await Assert.That(elementRemoved).IsTrue();
        await Assert.That(array).IsEquivalentTo(new NrbfNode[] { "0", "2" }, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task Remove(
        [MethodDataSource(nameof(SampleSearchValues))]
        string? item,

        [MethodDataSource(nameof(Samples1D))]
        ReadOnlyCollection<string?> values)
    {
        var collection = new List<NrbfNode>(
            values.Select(x => (NrbfNode)x));

        var array = new NrbfArray(NrbfMemberType.String);
        foreach (var value in values)
            array.Add(value);

        var arrayRemoved = array.Remove(item);
        var collectionRemoved = collection.Remove(item);

        using var unused = Assert.Multiple();

        await Assert.That(arrayRemoved)
            .IsEqualTo(collectionRemoved);

        await Assert.That(array)
            .IsEquivalentTo(collection, CollectionOrdering.Matching);
    }

    [Test]
    public async Task RemoveTypeMismatch()
    {
        var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2 };
        await Assert.That(array.Remove("Z"))
            .IsFalse();
    }

    [Test]
    public async Task RemoveHighRankUnsupported() =>
        await Assert.That(() => CreateExample2DArray().Remove("Test"))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task GetItemObject() =>
        await Assert.That(CreateExample2DArray()[new object[] {2, 5}])
            .IsNull();

    [Test]
    public async Task SetItemObject()
    {
        var array = CreateExample2DArray();
        array[new object[] {2, 5}] = "Z";
        await Assert.That(array[2, 5])
            .IsEqualTo("Z");
    }

    [Test]
    public async Task GetItemObjectIndicesRankMismatch()
    {
        await Assert.That(() => CreateExample2DArray()[new object[]{0, 0, 0, 0}])
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemObjectIndicesInvalidIndex()
    {
        await Assert.That(() => CreateExample2DArray()[0, "Z"])
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemObjectIndicesOutOfRange()
    {
        await Assert.That(() => CreateExample2DArray()[new object[] {100, 0}])
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task SetItemObjectIndicesRankMismatch()
    {
        await Assert.That(() => CreateExample2DArray()[new object[]{0, 0, 0, 0}] = "Z")
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SetItemObjectIndicesInvalidIndex()
    {
        await Assert.That(() => CreateExample2DArray()[0, "Z"] = "Z")
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task SetItemObjectIndicesOutOfRange()
    {
        await Assert.That(() => CreateExample2DArray()[new object[] {100, 0}] = "Z")
            .Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task SetItemObjectIndicesInvalidValue()
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(3),
            new NrbfArrayDimension(3),
            new NrbfArrayDimension(3),
        };
        var array = new NrbfArray(NrbfMemberType.Int32, dimensions);
        await Assert.That(() => array[new object[] { 0, 1, 2 }] = "Z")
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemSingleIndexInvalidRank()
    {
        await Assert.That(() => CreateExample2DArray()[0])
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task SetItemSingleIndexInvalidRank()
    {
        await Assert.That(() => CreateExample2DArray()[0] = "Z")
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task SetItemSingleIndexInvalidValue()
    {
        var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2 };
        await Assert.That(() => array[0] = "Z")
            .Throws<ArgumentException>();
    }

    [Test]
    public async Task GetItemObjectIndicesNull()
    {
        object[] indices = null!;
        await Assert.That(() => CreateExample2DArray()[indices])
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task SetItemObjectIndicesNull()
    {
        object[] indices = null!;
        await Assert.That(() => CreateExample2DArray()[indices] = "0")
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task GetItemInt32IndicesNull()
    {
        int[] indices = null!;
        await Assert.That(() => CreateExample2DArray()[indices])
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task SetItemInt32IndicesNull()
    {
        int[] indices = null!;
        await Assert.That(() => CreateExample2DArray()[indices] = "0")
            .Throws<ArgumentNullException>();
    }

    [Test]
    public async Task CopyToHighRank() =>
        await Assert.That(() => CreateExample2DArray().CopyTo(new NrbfNode[1000], 0))
            .Throws<InvalidOperationException>();

    [Test]
    public async Task EnumerateBlocks()
    {
        var array = CreateExample2DArray();
        array[3, 5] = "35";

        var blocks = array.EnumerateBlocks().ToList();

        var nullCountBefore = blocks
            .TakeWhile(block => block.Values.Count == 0)
            .Select(block => block.NullCount)
            .Sum();

        var valueBlock = blocks
            .Single(block => block.Values.Count == 1);

        var nullCountAfter = blocks
            .SkipWhile(block => block.Values.Count == 0)
            .Select(block => block.NullCount)
            .Sum();

        using var unused = Assert.Multiple();
        await Assert.That(nullCountBefore).IsEqualTo(2);
        await Assert.That(valueBlock.Values)
            .IsEquivalentTo(new NrbfNode?[] { "35" }, CollectionOrdering.Matching);
        await Assert.That(nullCountAfter).IsEqualTo(3);
    }

    private static NrbfArray CreateExample2DArray()
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(3, 2),
            new NrbfArrayDimension(2, 5),
        };
        var elementType = NrbfMemberType.String;
        return new NrbfArray(elementType, dimensions);
    }

    private static NrbfArray CreateSingleElementExample2DArray(
        NrbfMemberType elementType)
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(1),
            new NrbfArrayDimension(1),
        };
        return new NrbfArray(elementType, dimensions);
    }

    public static ReadOnlyCollection<string?> SampleSearchValues { get; } =
        [null, "0", "1", "2", "3", "4", "5", "6"];

    public static ReadOnlyCollection<string?> SampleAlternationStringNull { get; } =
        ["0" , null, "2", null, "4", null];

    public static ReadOnlyCollection<string?> SampleAlternationNullString { get; } =
        [null, "1", null, "3", null, "5"];

    public static ReadOnlyCollection<string?> SampleStringsNulls { get; } =
        ["0", "1", "2", null, null, null];

    public static ReadOnlyCollection<string?> SampleNullsStrings { get; } =
        [null, null, null, "3", "4", "5"];

    public static ReadOnlyCollection<int> SamplesIndexes { get; } =
        [0, 1, 2, 3, 4, 5];

    public static ReadOnlyCollection<int> SamplesInsertionIndexes { get; } =
        [0, 1, 2, 3, 4, 5, 6];

    public static ReadOnlyCollection<ReadOnlyCollection<string?>> Samples1D { get; } =
        [
            SampleAlternationNullString,
            SampleAlternationStringNull,
            SampleStringsNulls,
            SampleNullsStrings,
        ];
}
