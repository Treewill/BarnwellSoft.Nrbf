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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Barnwellsoft.Nrbf.Internal;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF array.
    /// </summary>
    public sealed class NrbfArray : NrbfNode, IEnumerable<NrbfNode>
    {
        /// <summary>
        /// Underlying element storage.
        /// </summary>
        private readonly NrbfClusterList _array;

        /// <inheritdoc cref="Dimensions"/>
        private readonly NrbfArrayDimension[] _dimensions;

        /// <summary>
        /// Gets a value indicating whether the <see cref="NrbfArray"/> is read-only.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the <see cref="NrbfArray"/> is read-only;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsReadOnly => false;

        /// <summary>
        /// The dimensions of the <see cref="NrbfArray"/>.
        /// </summary>
        public ReadOnlyCollection<NrbfArrayDimension> Dimensions { get; }

        /// <summary>
        /// Gets the rank (number of dimensions) of the <see cref="NrbfArray"/>.
        /// </summary>
        public int Rank => _dimensions.Length;

        /// <summary>
        /// The (declared) type of the elements within the array.
        /// </summary>
        public NrbfMemberType ElementType { get; }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="NrbfArray"/>.
        /// </summary>
        /// <exception cref="OverflowException">
        /// The array is multidimensional and contains more than <see cref="Int32.MaxValue"/> elements.
        /// </exception>
        public int Count
        {
            get
            {
                var count = 1;
                foreach (var dimension in _dimensions)
                {
                    count = checked(count * dimension.Length);
                    if (count == 0)
                        return 0;
                }

                return count;
            }
        }

        /// <summary>
        /// Gets or sets the value at the specified position in the multidimensional <see cref="NrbfArray"/>.
        /// The indexes are specified as an array of 32-bit integers.
        /// </summary>
        /// <param name="indices">
        /// A one-dimensional array of 32-bit integers that represent the indexes specifying
        /// the position of the <see cref="NrbfArray"/> element to get.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The number of dimensions in the current <see cref="NrbfArray"/> is not equal to
        /// the number of elements in indices.
        /// -or-
        /// <paramref name="value"/> cannot be converted to the array element type.
        /// -or-
        /// An index cannot be converted to a 32-bit integer.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Any element in indices is outside the range of
        /// valid indexes for the corresponding dimension of the current <see cref="NrbfArray"/>.
        /// </exception>
        public override NrbfNode this[params object[] indices]
        {
            get
            {
                if (indices is null)
                    throw new ArgumentNullException(nameof(indices));

                if (indices.Length == 1 && indices[0].GetType().IsArray)
                    return this[indices[0]];

                if (!TryConvertIndices(indices, out var intIndices))
                    throw new ArgumentException("Index cannot be converted to an integer.");

                return this[intIndices];
            }
            set
            {
                if (indices is null)
                    throw new ArgumentNullException(nameof(indices));

                if (indices.Length == 1 && indices[0] is int[] wrappedIndices)
                {
                    this[wrappedIndices] = value;
                    return;
                }

                if (!TryConvertIndices(indices, out var intIndices))
                    throw new ArgumentException("Index cannot be converted to an integer.");

                this[intIndices] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value at the specified position in the multidimensional <see cref="NrbfArray"/>.
        /// The indexes are specified as an array of 32-bit integers.
        /// </summary>
        /// <param name="indices">
        /// A one-dimensional array of 32-bit integers that represent the indexes specifying
        /// the position of the <see cref="NrbfArray"/> element to get.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The number of dimensions in the current <see cref="NrbfArray"/> is not equal to
        /// the number of elements in indices.
        /// -or-
        /// <paramref name="value"/> cannot be converted to the array element type.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Any element in indices is outside the range of
        /// valid indexes for the corresponding dimension of the current <see cref="NrbfArray"/>.
        /// </exception>
        public NrbfNode this[params int[] indices]
        {
            get
            {
                if (indices is null)
                    throw new ArgumentNullException(nameof(indices));

                if (indices.Length != Rank)
                    throw new ArgumentException("Index count does not match Rank.");

                object result = this;
                for (var i = 0; i < Rank; i++)
                {
                    var inner = indices[i] - _dimensions[i].Offset;
                    if (inner >= _dimensions[i].Length)
                        throw new ArgumentOutOfRangeException(nameof(indices), "Index is outside the bounds of the array.");

                    result = ((NrbfArray)result)?._array[inner];
                }

                return (NrbfNode)result ?? GetDefaultValue(ElementType);
            }

            set
            {
                if (indices is null)
                    throw new ArgumentNullException(nameof(indices));

                if (indices.Length != Rank)
                    throw new ArgumentException("Index count does not match Rank.");

                if (!TryConvertNode(ElementType, ref value))
                    throw new ArgumentException("Value cannot be converted to the array element type.");

                var lastRank = Rank - 1;
                var lastArray = this;

                int innerIndex;
                for (var i = 0; i < lastRank; ++i)
                {
                    var dimension = _dimensions[i];
                    innerIndex = indices[i] - dimension.Offset;
                    if (innerIndex >= dimension.Length)
                        throw new ArgumentOutOfRangeException(nameof(indices), "Index is outside the bounds of the array.");

                    var nextArray = (NrbfArray)lastArray._array[innerIndex];
                    if (nextArray is null)
                    {
                        nextArray = new NrbfArray(ElementType, new[] { new NrbfArrayDimension(_dimensions[i + 1].Length) });
                        lastArray._array[innerIndex] = nextArray;
                    }

                    lastArray = nextArray;
                }

                innerIndex = indices[lastRank] - _dimensions[lastRank].Offset;

                lastArray[innerIndex] = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not 1.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> cannot be converted to the array element type.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than <see cref="Count"/>.
        /// </exception>
        public NrbfNode this[int index]
        {
            get
            {
                if (Rank != 1)
                    throw new InvalidOperationException("Index count does not match Rank.");

                return _array[index];
            }
            set
            {
                if (Rank != 1)
                    throw new InvalidOperationException("Index count does not match Rank.");

                if (!TryConvertNode(ElementType, ref value))
                    throw new ArgumentException("Value cannot be converted to the array element type.");

                _array[index] = value;
            }
        }

        /// <summary>
        /// Creates a multidimensional <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="elementType">The type of elements.</param>
        /// <param name="dimensions">The dimensions of the array.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elementType"/> is <see langword="null"/>.
        /// -or-
        /// <paramref name="dimensions"/> is <see langword="mull"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dimensions"/> is empty.
        /// -or-
        /// <paramref name="dimensions"/> has a <see langword="null"/> dimension.
        /// </exception>
        public NrbfArray(
            NrbfMemberType elementType,
            IEnumerable<NrbfArrayDimension> dimensions)
            : base(NrbfNodeType.Array)
        {
            if (dimensions is null)
                throw new ArgumentNullException(nameof(dimensions));

            if (elementType is null)
                throw new ArgumentNullException(nameof(elementType));

            _dimensions = dimensions.ToArray();
            if (_dimensions.Length == 0)
                throw new ArgumentException("At least one dimension is required.", nameof(dimensions));

            if (_dimensions.Any(dimension => dimension is null))
                throw new ArgumentException("Dimensions cannot be null.", nameof(dimensions));

            var arrayFieldType = elementType;
            if (_dimensions.Length != 1) 
                arrayFieldType = NrbfMemberType.Object;

            _array = CreateArray(_dimensions[0].Length, arrayFieldType);

            Dimensions = new ReadOnlyCollection<NrbfArrayDimension>(_dimensions);
            ElementType = elementType;
        }

        /// <summary>
        /// Creates an empty one-dimensional <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="elementType">The type of elements.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elementType"/> is <see langword="null"/>.
        /// </exception>
        public NrbfArray(NrbfMemberType elementType)
            : base(NrbfNodeType.Array)
        {
            if (elementType is null)
                throw new ArgumentNullException(nameof(elementType));

            _dimensions = new[] { new NrbfArrayDimension(0) };
            _array = CreateArray(0, elementType);
            Dimensions = new ReadOnlyCollection<NrbfArrayDimension>(_dimensions);
            ElementType = elementType;
        }

        /// <summary>
        /// Enumerates the array blocks.
        /// </summary>
        /// <returns>Array blocks.</returns>
        public IEnumerable<NrbfArrayBlock> EnumerateBlocks() =>
            AggregateNullBlocks(EnumerateBlocks(0, _dimensions));

        /// <summary>
        /// Enumerates the array blocks.
        /// </summary>
        /// <param name="dimensionIndex">The starting dimension.</param>
        /// <param name="dimensions">The dimensions of the owning array.</param>
        /// <returns>Array blocks.</returns>
        private IEnumerable<NrbfArrayBlock> EnumerateBlocks(
            int dimensionIndex,
            NrbfArrayDimension[] dimensions)
        {
            var nextDimensionIndex = dimensionIndex + 1;

            if (nextDimensionIndex == dimensions.Length)
                return _array.EnumerateBlocks();

            var nullCount = 1;

            foreach (var dimension in dimensions.Skip(nextDimensionIndex))
                checked { nullCount = nullCount * dimension.Length; }

            var blocks = _array
                .Select(node => (NrbfArray)node)
                .SelectMany(array => 
                    array?.EnumerateBlocks(nextDimensionIndex, dimensions)
                    ?? EnumerateSingleNullBlock(nullCount));

            return blocks;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="NrbfArray"/>.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the <see cref="NrbfArray"/>.
        /// </returns>
        public IEnumerator<NrbfNode> GetEnumerator()
        {
            IEnumerable<NrbfNode> enumerable = _array;

            var rank = Rank;
            for (var i = 1; i < rank; ++i)
            {
                var dimensionLength = _dimensions[i].Length;

                enumerable = enumerable
                    .Cast<NrbfArray>()
                    .SelectMany(x => x ?? Enumerable.Repeat<NrbfNode>(null, dimensionLength));
            }

            return enumerable.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Adds an item to the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="NrbfArray"/>.</param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="item"/> cannot be converted to the array element type. 
        /// </exception>
        public void Add(NrbfNode item)
        {
            if (Rank != 1)
                throw new InvalidOperationException("Add unsupported for ranks greater than one.");

            if (!TryConvertNode(ElementType, ref item))
                throw new ArgumentException("Item cannot be converted to the array element type.", nameof(item));

            _array.Add(item);
            _dimensions[0] = new NrbfArrayDimension(_array.Count, _dimensions[0].Offset);
        }

        /// <summary>
        /// Removes all items from the <see cref="NrbfArray"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        public void Clear()
        {
            if (Rank != 1)
                throw new InvalidOperationException("Clear unsupported for ranks greater than one.");

            _array.Clear();
            _dimensions[0] = new NrbfArrayDimension(_array.Count, _dimensions[0].Offset);
        }

        /// <summary>
        /// Determines whether the <see cref="NrbfArray"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="NrbfArray"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if item is found in the <see cref="NrbfArray"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public bool Contains(NrbfNode item) =>
            (item is null && ContainsNull())
            || (TryConvertNode(ElementType, ref item) && ContainsValue(item));

        /// <summary>
        /// Checks if the <see cref="NrbfArray"/> contains <see langword="null"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the <see cref="NrbfArray"/> contains a <see langword="null"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        private bool ContainsNull() =>
            this.Any(value => value is null);

        /// <summary>
        /// Checks if the <see cref="NrbfArray"/> contains a specific value.
        /// </summary>
        /// <param name="item"> The object to locate in the <see cref="NrbfArray"/>. </param>
        /// <returns>
        /// <see langword="true"/> if the <see cref="NrbfArray"/> contains a <see langword="null"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        private bool ContainsValue(NrbfNode item) =>
            this.Any(value => value?.Equals(item) ?? false);

        /// <summary>
        /// Copies the elements of the <see cref="NrbfArray"/> to an Array,
        /// starting at a particular Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional Array that is the destination of the elements copied from <see cref="NrbfArray"/>.
        /// The Array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        public void CopyTo(NrbfNode[] array, int arrayIndex)
        {
            if (Rank != 1)
                throw new InvalidOperationException("CopyTo unsupported for ranks greater than one.");

            _array.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="NrbfArray"/>.</param>
        /// <returns>
        /// <see langword="true"/> if item was successfully removed from the <see cref="NrbfArray"/>;
        /// otherwise, <see langword="false"/>.
        /// This method also returns <see langword="false"/> if item is not found in the original <see cref="NrbfArray"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        public bool Remove(NrbfNode item)
        {
            if (Rank != 1)
                throw new InvalidOperationException("Remove unsupported for ranks greater than one.");

            if (!TryConvertNode(ElementType, ref item))
                return false;

            if (!_array.Remove(item))
                return false;

            _dimensions[0] = new NrbfArrayDimension(_array.Count, _dimensions[0].Offset);
            return true;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="NrbfArray"/>.</param>
        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        public int IndexOf(NrbfNode item)
        {
            if (Rank != 1)
                throw new InvalidOperationException("IndexOf unsupported for ranks greater than one.");

            if (!TryConvertNode(ElementType, ref item))
                return -1;

            var index = _array.IndexOf(item);
            if (index == -1)
                return -1;

            return index + _dimensions[0].Offset;
        }

        /// <summary>
        /// Inserts an item to the <see cref="NrbfArray"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="NrbfArray"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="NrbfArray"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="item"/> cannot be converted to the array element type. 
        /// </exception>
        public void Insert(int index, NrbfNode item)
        {
            if (Rank != 1)
                throw new InvalidOperationException("Insert unsupported for ranks greater than one.");

            if (!TryConvertNode(ElementType, ref item))
                throw new ArgumentException("Item cannot be converted to the array element type.", nameof(item));

            _array.Insert(index - _dimensions[0].Offset, item);
            _dimensions[0] = new NrbfArrayDimension(_array.Count, _dimensions[0].Offset);
        }

        /// <summary>
        /// Removes the <see cref="NrbfArray"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="NrbfArray"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is not equal to 1.
        /// </exception>
        public void RemoveAt(int index)
        {
            if (Rank != 1)
                throw new InvalidOperationException("RemoveAt unsupported for ranks greater than one.");

            _array.RemoveAt(index - _dimensions[0].Offset);
            _dimensions[0] = new NrbfArrayDimension(_array.Count, _dimensions[0].Offset);
        }

        /// <summary>
        /// Gets this <see cref="NrbfArray"/> as a 1 dimensional list.
        /// </summary>
        /// <returns>This <see cref="NrbfArray"/> as a 1 dimensional list.</returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="Rank"/> is greater than 1.
        /// </exception> 
        public NrbfArrayListAdapter AsList()
        {
            if (Rank != 1)
                throw new InvalidOperationException("AsList unsupported for ranks greater than one.");

            return new NrbfArrayListAdapter(this);
        }

        /// <summary>
        /// Creates the underlying collection for the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="length">The length of the <see cref="NrbfArray"/>.</param>
        /// <param name="type">The element type.</param>
        /// <returns>The underlying collection for the <see cref="NrbfArray"/>.</returns>
        private static NrbfClusterList CreateArray(
            int length,
            NrbfMemberType type)
        {
            var defaultValue = GetDefaultValue(type);
            var array = new NrbfClusterList();

            if (defaultValue is null)
            {
                array.AddNulls(length);
                return array;
            }

            for (var i = 0; i < length; ++i) 
                array.Add(defaultValue);

            return array;
        }

        /// <summary>
        /// Aggregates consecutive null blocks.
        /// </summary>
        /// <param name="blocks">the blocks to aggregate.</param>
        /// <returns>The blocks with nulls grouped together.</returns>
        private static IEnumerable<NrbfArrayBlock> AggregateNullBlocks(
            IEnumerable<NrbfArrayBlock> blocks)
        {
            using (var block = blocks.GetEnumerator())
            {
                IList<NrbfNode> items = EmptyArray;
                var nulls = 0;
                while (block.MoveNext())
                {
                    var blockItems = block.Current.Values;
                    var blockNulls = block.Current.NullCount;

                    var nullSpace = int.MaxValue - nulls;

                    if (blockItems.Count != 0)
                    {
                        if (items.Count > 0 || nulls > 0)
                            yield return new NrbfArrayBlock(items, nulls);

                        items = blockItems;
                        nulls = blockNulls;
                    }
                    else if (nullSpace <= blockNulls)
                    {
                        yield return new NrbfArrayBlock(items, nulls);
                        items = blockItems;
                        nulls = blockNulls - nullSpace;
                    }
                    else
                    {
                        nulls += blockNulls;
                    }
                }

                if (items.Count == 0 && nulls == 0)
                    yield break;

                yield return new NrbfArrayBlock(items, nulls);
            }
        }

        /// <inheritdoc cref="NrbfValueDefaulting.GetDefaultValue"/>
        private static NrbfNode GetDefaultValue(NrbfMemberType type) =>
            NrbfValueDefaulting.GetDefaultValue(type);

        /// <inheritdoc cref="NrbfValueConversion.TryConvertNode"/>
        private static bool TryConvertNode(NrbfMemberType type, ref NrbfNode node) =>
            NrbfValueConversion.TryConvertNode(type, ref node);

        /// <summary>
        /// Converts the object indices to Int32 indices.
        /// </summary>
        /// <param name="indices">The object indices.</param>
        /// <param name="intIndices">The resulting Int32 indices.</param>
        /// <returns>
        /// <see langword="true"/> if successful;
        /// otherwise <see langword="false"/>.
        /// </returns>
        private static bool TryConvertIndices(object[] indices, out int[] intIndices)
        {
            intIndices = new int[indices.Length];
            for (var i = 0; i < indices.Length; ++i)
            {
                if (!(indices[i] is int q))
                    return false;

                intIndices[i] = q;
            }
            return true;
        }

        /// <summary>
        /// Enumerates a single block of nulls.
        /// </summary>
        /// <param name="nullCount">The number of nulls for the block.</param>
        /// <returns>The single block of nulls.</returns>
        private static IEnumerable<NrbfArrayBlock> EnumerateSingleNullBlock(int nullCount) =>
            new[] { new NrbfArrayBlock(EmptyArray, nullCount) };

        /// <summary>
        /// An empty array of <see cref="NrbfNode"/> values.
        /// </summary>
#if NETSTANDARD1_0
        private static NrbfNode[] EmptyArray { get; } = new NrbfNode[0];
#else
        private static NrbfNode[] EmptyArray { get; } = Array.Empty<NrbfNode>();
#endif
    }
}
