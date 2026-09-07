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

public class NrbfArrayWhiteboxRedBlackTreeTests
{
    [Test]
    public async Task EditRoot()
    {
        var array = NewEmptyStringArray(2);

        array[0] = "0";

        var expected = new List<NrbfNode?>() { "0", null };
        await Assert.That(array)
            .IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddAfterRoot(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(4);
        array[0] = "0";

        operation.Invoke(array.AsList(), 2, "2");

        var expected = new List<NrbfNode?>() { "0", null, null, null };
        operation.Invoke(expected, 2, "2");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddAfterRootChild(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(6);
        array[0] = "0";
        array[2] = "2";

        operation.Invoke(array.AsList(), 4, "4");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, null, null };
        operation.Invoke(expected, 4, "4");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddTree4Slot4(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(8);
        array[0] = "0";
        array[2] = "2";
        array[4] = "4";

        operation.Invoke(array.AsList(), 6, "6");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, null, null };
        operation.Invoke(expected, 6, "6");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddTree4Slot3(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(8);
        array[0] = "0";
        array[2] = "2";
        array[6] = "6";

        operation.Invoke(array.AsList(), 4, "4");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, null, null, "6", null };
        operation.Invoke(expected, 4, "4");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddTree4Slot2(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(8);
        array[0] = "0";
        array[4] = "4";
        array[6] = "6";

        operation.Invoke(array.AsList(), 2, "2");

        var expected = new List<NrbfNode?>() { "0", null, null, null, "4", null, "6", null };
        operation.Invoke(expected, 2, "2");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddDirectChildRed(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(12);
        array[0] = "0";
        array[2] = "2";
        array[6] = "6";
        array[4] = "4";
        array[8] = "8";
        array[10] = "10";
        array[10] = null;

        operation.Invoke(array.AsList(), 10, "10");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, "6", null, "8", null, null, null };
        operation.Invoke(expected, 10, "10");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddGrandchildRed(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(12);
        array[0] = "0";
        array[2] = "2";
        array[6] = "6";
        array[4] = "4";
        array[10] = "10";
        array[8] = "8";
        array[8] = null;

        operation.Invoke(array.AsList(), 8, "8");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, "6", null, null, null, "10", null };
        operation.Invoke(expected, 8, "8");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddDescendantRed(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(12);
        array[0] = "0";
        array[2] = "2";
        array[8] = "8";
        array[6] = "6";
        array[10] = "10";
        array[4] = "4";
        array[4] = null;

        operation.Invoke(array.AsList(), 4, "4");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, null, null, "6", null, "8", null, "10", null };
        operation.Invoke(expected, 4, "4");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddBetweenRootAndChild(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(6);
        array[0] = "0";
        array[4] = "4";

        operation.Invoke(array.AsList(), 2, "2");

        var expected = new List<NrbfNode?>() { "0", null, null, null, "4", null };
        operation.Invoke(expected, 2, "2");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RootChildRotationAddPathBeforeAfter(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(10);
        array[0] = "0";
        array[2] = "2";
        array[4] = "4";
        array[8] = "8";

        operation.Invoke(array.AsList(), 6, "6");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, null, null, "8", null };
        operation.Invoke(expected, 6, "6");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RootChildRotationAddPathBeforeBefore(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(10);
        array[0] = "0";
        array[2] = "2";
        array[8] = "8";
        array[6] = "6";

        operation.Invoke(array.AsList(), 4, "4");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, null, null, "6", null, "8", null };
        operation.Invoke(expected, 4, "4");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RootChildRotationAddPathAfterBefore(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(10);
        array[0] = "0";
        array[2] = "2";
        array[4] = "4";
        array[8] = "8";

        operation.Invoke(array.AsList(), 6, "6");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, null, null, "8", null };
        operation.Invoke(expected, 6, "6");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RootChildRotationAddPathAfterAfter(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation)
    {
        var array = NewEmptyStringArray(10);
        array[0] = "0";
        array[2] = "2";
        array[4] = "4";
        array[6] = "6";

        operation.Invoke(array.AsList(), 8, "8");

        var expected = new List<NrbfNode?>() { "0", null, "2", null, "4", null, "6", null, null, null };
        operation.Invoke(expected, 8, "8");
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddToBalance8(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation,
        [Arguments(2, 6, 10, 14, 18, 22, 26)]
        int index)
    {
        var value = index.ToString();
        var array = NewBalance8StringArray();

        operation.Invoke(array.AsList(), index, value);

        var expected = NewBalance8ExpectedStrings();
        operation.Invoke(expected, index, value);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddToBalance16(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation,
        [Arguments(2, 6, 10, 14, 18, 22, 26, 30, 34, 38, 42, 46, 50, 54, 58)]
        int index)
    {
        var value = index.ToString();
        var array = NewBalance16StringArray();

        operation.Invoke(array.AsList(), index, value);

        var expected = NewBalance16ExpectedStrings();
        operation.Invoke(expected, index, value);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddToFullLowTree(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation,
        [Arguments(6, 10, 14, 18, 22, 26, 30, 34)]
        int index)
    {
        var value = index.ToString();
        var array = NewFullLowTreeArray();

        operation.Invoke(array.AsList(), index, value);

        var expected = NewHalfTreeExpectedStrings();
        operation.Invoke(expected, index, value);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task AddToFullHighTree(
        [MethodDataSource(nameof(IncludeOperations))]
        IncludeOperation operation,
        [Arguments(2, 6, 10, 14, 18, 22, 26)]
        int index)
    {
        var value = index.ToString();
        var array = NewFullHighTreeArray();

        operation.Invoke(array.AsList(), index, value);

        var expected = NewHalfTreeExpectedStrings();
        operation.Invoke(expected, index, value);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    public async Task RemoveRoot()
    {
        var array = NewEmptyStringArray(1);

        array.RemoveAt(0);

        await Assert.That(array).IsEmpty();
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveFromBalance4(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation,
        [Arguments(0, 1, 2, 3, 4)]
        int index)
    {
        var array = NewEmptyStringArray(5);
        array[1] = "1";
        array[3] = "3";

        operation.Invoke(array.AsList(), index);

        var expected = new List<NrbfNode?>() { null, "1", null, "3", null };
        operation.Invoke(expected, index);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveRedPeerSide(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation)
    {
        var array = NewEmptyStringArray(23);
        array[1] = "1";
        array[3] = "3";
        array[5] = "5";
        array[7] = "7";
        array[9] = "9";
        array[11] = "11";
        array[13] = "13";
        array[15] = "15";
        array[17] = "17";
        array[19] = "19";
        array[21] = "21";

        array[21] = null;

        operation.Invoke(array.AsList(), 3);

        var expected = new List<NrbfNode?>()
        {
            null, "1", null, "3", null,
            "5", null, "7", null, "9",
            null, "11", null, "13", null,
            "15", null, "17", null, "19",
            null, null, null,
        };
        operation.Invoke(expected, 3);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveRedPeerOtherSide(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation)
    {
        var array = NewEmptyStringArray(23);
        array[1] = "1";
        array[3] = "3";
        array[5] = "5";
        array[7] = "7";
        array[9] = "9";
        array[11] = "11";
        array[13] = "13";
        array[15] = "15";
        array[17] = "17";
        array[19] = "19";
        array[21] = "21";

        array[21] = null;
        array[11] = null;

        operation.Invoke(array.AsList(), 3);

        var expected = new List<NrbfNode?>()
        {
            null, "1", null, "3", null,
            "5", null, "7", null, "9",
            null, null, null, "13", null,
            "15", null, "17", null, "19",
            null, null, null,
        };
        operation.Invoke(expected, 3);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveRedParent(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation)
    {
        var array = NewEmptyStringArray(23);
        array[1] = "1";
        array[3] = "3";
        array[5] = "5";
        array[7] = "7";
        array[9] = "9";
        array[11] = "11";
        array[13] = "13";
        array[15] = "15";
        array[17] = "17";
        array[19] = "19";
        array[21] = "21";

        array[21] = null;
        array[15] = null;
        array[7] = null;

        operation.Invoke(array.AsList(), 3);

        var expected = new List<NrbfNode?>()
        {
            null, "1", null, "3", null,
            "5", null, null, null, "9",
            null, "11", null, "13", null,
            null, null, "17", null, "19",
            null, null, null,
        };
        operation.Invoke(expected, 3);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveRedPeerSideSide(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation,
        [Arguments(0, 1, 2, 3)]
        int index)
    {
        var array = NewEmptyStringArray(69);
        array[1] = "1";
        array[3] = "3";
        array[5] = "5";
        array[15] = "15";
        array[23] = "23";
        array[27] = "27";
        array[37] = "37";
        array[39] = "39";
        array[41] = "41";
        array[43] = "43";
        array[45] = "45";
        array[47] = "47";
        array[49] = "49";
        array[51] = "51";
        array[53] = "53";
        array[55] = "55";
        array[57] = "57";
        array[59] = "59";
        array[61] = "61";
        array[63] = "63";
        array[65] = "65";
        array[67] = "67";

        array[9] = "9";
        array[17] = "17";
        array[25] = "25";
        array[31] = "31";
        array[7] = "7";
        array[11] = "11";
        array[19] = "19";
        array[21] = "21";
        array[29] = "29";
        array[33] = "33";
        array[35] = "35";

        array[13] = "13";
        operation.Invoke(array.AsList(), index);

        var expected = new List<NrbfNode?>()
        {
            null, "1", null, "3", null,
            "5", null, "7", null, "9",
            null, "11", null, "13", null,
            "15", null, "17", null, "19",
            null, "21", null, "23", null,
            "25", null, "27", null, "29",
            null, "31", null, "33", null,
            "35", null, "37", null, "39",
            null, "41", null, "43", null,
            "45", null, "47", null, "49",
            null, "51", null, "53", null,
            "55", null, "57", null, "59",
            null, "61", null, "63", null,
            "65", null, "67", null,
        };
        operation.Invoke(expected, index);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }

    [Test]
    [CombinedDataSources]
    public async Task RemoveBlackPeerSideSide(
        [MethodDataSource(nameof(ExcludeOperations))]
        ExcludeOperation operation,
        [Arguments(0, 1, 2, 3)]
        int index)
    {
        var array = NewEmptyStringArray(69);
        array[1] = "1";
        array[3] = "3";
        array[5] = "5";
        array[15] = "15";
        array[23] = "23";
        array[27] = "27";
        array[37] = "37";
        array[39] = "39";
        array[41] = "41";
        array[43] = "43";
        array[45] = "45";
        array[47] = "47";
        array[49] = "49";
        array[51] = "51";
        array[53] = "53";
        array[55] = "55";
        array[57] = "57";
        array[59] = "59";
        array[61] = "61";
        array[63] = "63";
        array[65] = "65";
        array[67] = "67";

        array[9] = "9";
        array[17] = "17";
        array[25] = "25";
        array[31] = "31";
        array[7] = "7";
        array[11] = "11";
        array[19] = "19";
        array[21] = "21";
        array[29] = "29";
        array[33] = "33";
        array[35] = "35";

        operation.Invoke(array.AsList(), index);

        var expected = new List<NrbfNode?>()
        {
            null, "1", null, "3", null,
            "5", null, "7", null, "9",
            null, "11", null, null, null,
            "15", null, "17", null, "19",
            null, "21", null, "23", null,
            "25", null, "27", null, "29",
            null, "31", null, "33", null,
            "35", null, "37", null, "39",
            null, "41", null, "43", null,
            "45", null, "47", null, "49",
            null, "51", null, "53", null,
            "55", null, "57", null, "59",
            null, "61", null, "63", null,
            "65", null, "67", null,
        };
        operation.Invoke(expected, index);
        await Assert.That(array).IsEquivalentTo(expected, CollectionOrdering.Matching);
    }



    public static NrbfArray NewEmptyStringArray(int length) =>
        new NrbfArray(NrbfMemberType.String, [new NrbfArrayDimension(length)]);

    public static NrbfArray NewBalance8StringArray()
    {
        var array = NewEmptyStringArray(28);
        array[0] = "0";
        array[12] = "12";
        array[20] = "20";
        array[4] = "4";
        array[8] = "8";
        array[16] = "16";
        array[24] = "24";
        return array;
    }

    public static List<NrbfNode?> NewBalance8ExpectedStrings() =>
    [
        "0", null, null, null,
        "4", null, null, null,
        "8", null, null, null,
        "12", null, null, null,
        "16", null, null, null,
        "20", null, null, null,
        "24", null, null, null,
    ];

    public static NrbfArray NewBalance16StringArray()
    {
        var array = NewEmptyStringArray(60);
        array[0] = "0";
        array[28] = "28";
        array[44] = "44";
        array[12] = "12";
        array[20] = "20";
        array[36] = "36";
        array[52] = "52";
        array[4] = "4";
        array[8] = "8";
        array[16] = "16";
        array[24] = "24";
        array[32] = "32";
        array[40] = "40";
        array[48] = "48";
        array[56] = "56";
        return array;
    }

    public static List<NrbfNode?> NewBalance16ExpectedStrings() =>
    [
        "0", null, null, null,
        "4", null, null, null,
        "8", null, null, null,
        "12", null, null, null,
        "16", null, null, null,
        "20", null, null, null,
        "24", null, null, null,
        "28", null, null, null,
        "32", null, null, null,
        "36", null, null, null,
        "40", null, null, null,
        "44", null, null, null,
        "48", null, null, null,
        "52", null, null, null,
        "56", null, null, null,
    ];

    public static NrbfArray NewFullHighTreeArray()
    {
        var array = NewEmptyStringArray(36);
        array[0] = "0";
        array[4] = "4";
        array[20] = "20";
        array[12] = "12";
        array[28] = "28";
        array[8] = "8";
        array[16] = "16";
        array[24] = "24";
        array[32] = "32";

        return array;
    }

    public static NrbfArray NewFullLowTreeArray()
    {
        var array = NewEmptyStringArray(36);
        array[0] = "0";
        array[28] = "28";
        array[32] = "32";
        array[12] = "12";
        array[20] = "20";
        array[4] = "4";
        array[8] = "8";
        array[16] = "16";
        array[24] = "24";

        return array;
    }

    public static List<NrbfNode?> NewHalfTreeExpectedStrings() =>
    [
        "0", null, null, null,
        "4", null, null, null,
        "8", null, null, null,
        "12", null, null, null,
        "16", null, null, null,
        "20", null, null, null,
        "24", null, null, null,
        "28", null, null, null,
        "32", null, null, null,
    ];

    public delegate void IncludeOperation(IList<NrbfNode?> list, int index, NrbfNode value);

    public static ReadOnlyCollection<IncludeOperation> IncludeOperations { get; } =
        [SetValue, InsertValue];

    public static void SetValue(IList<NrbfNode?> list, int index, NrbfNode value) => 
        list[index] = value;

    public static void InsertValue(IList<NrbfNode?> list, int index, NrbfNode value) =>
        list.Insert(index, value);

    public delegate void ExcludeOperation(IList<NrbfNode?> list, int index);

    public static ReadOnlyCollection<ExcludeOperation> ExcludeOperations { get; } =
        [SetSwap, RemoveAt];

    public static void SetSwap(IList<NrbfNode?> list, int index)
    {
        if (list[index] is null)
        {
            list[index] = index.ToString();
        }
        else
        {
            list[index] = null;
        }
    }

    public static void RemoveAt(IList<NrbfNode?> list, int index) =>
        list.RemoveAt(index);
}
