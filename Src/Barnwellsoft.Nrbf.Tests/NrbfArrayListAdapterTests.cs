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

using TUnit.Assertions.Enums;

namespace Barnwellsoft.Nrbf.Tests;

public class NrbfArrayListAdapterTests
{
    [Test]
    public async Task NrbfArrayAsListSample()
    {
        NrbfArrayListAdapter? list;
        {
            var array = new NrbfArray(NrbfMemberType.Int32) { 0, 1, 2, 3, 4 };
            list = array.AsList();

            await Assert.That(list.Array)
                .IsSameReferenceAs(array);
        }

        using var unused = Assert.Multiple();

        await Assert.That(list)
            .Member(x => x.IsReadOnly, x => x.IsFalse());

        await Assert.That(list.IndexOf(3))
            .IsEqualTo(3);

        await Assert.That(list.IndexOf(7))
            .IsEqualTo(-1);


        await Assert.That(list.Contains(4))
            .IsTrue();

        await Assert.That(list.Contains(5))
            .IsFalse();


        await Assert.That(list.Remove(2))
            .IsTrue();

        await Assert.That(list.Remove(6))
            .IsFalse();

        list.RemoveAt(3);

        list.Insert(0, -1);
        list.Add(5);

        list[1] = 42;

        await Assert.That(list)
            .HasProperty(x => x[1])
            .IsEqualTo(42);

        var copy = new NrbfNode[10];
        list.CopyTo(copy, 1);

        var expectedCopy = new NrbfNode?[] { null, -1, 42, 1, 3, 5, null, null, null, null };

        await Assert.That(copy)
            .IsEquivalentTo(expectedCopy, CollectionOrdering.Matching);

        var expectedValues = new NrbfNode[] { -1, 42, 1, 3, 5 };
        await Assert.That(list)
            .IsEquivalentTo(expectedValues, CollectionOrdering.Matching);

        await Assert.That(list)
            .HasProperty(x => x.Count)
            .IsEqualTo(5);

        await Assert.That(list.Select(x => (int)x))
            .IsEquivalentTo([-1, 42, 1, 3, 5], CollectionOrdering.Matching);

        await Assert.That(() => list.GetEnumerator())
            .ThrowsNothing();

        await Assert.That(() => ((System.Collections.IEnumerable)list).GetEnumerator())
            .ThrowsNothing();

        list.Clear();

        await Assert.That(list)
            .IsEmpty();
    }

    [Test]
    public async Task NrbfArrayAsListHighRankError()
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(3),
            new NrbfArrayDimension(3),
        };

        var array = new NrbfArray(NrbfMemberType.Int32, dimensions);

        await Assert.That(array.AsList)
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task NewNullArray() =>
        await Assert.That(() => new NrbfArrayListAdapter(null))
            .Throws<ArgumentNullException>();

    [Test]
    public async Task NewHighRankArray()
    {
        var dimensions = new[]
        {
            new NrbfArrayDimension(3),
            new NrbfArrayDimension(3),
        };

        var array = new NrbfArray(NrbfMemberType.Int32, dimensions);

        await Assert.That(() => new NrbfArrayListAdapter(array))
            .Throws<ArgumentException>();
    }
}