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

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An <see cref="IList{NrbfNode}"/> wrapper for an <see cref="NrbfArray"/>.
    /// </summary>
    public class NrbfArrayListAdapter : IList<NrbfNode>
    {
        /// <summary>
        /// The underlying <see cref="NrbfArray"/>.
        /// </summary>
        public NrbfArray Array { get; }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="NrbfArray"/>.
        /// </summary>
        public int Count =>
            Array.Count;

        /// <inheritdoc cref="NrbfArray.IsReadOnly"/>
        public bool IsReadOnly =>
            Array.IsReadOnly;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> cannot be converted to the array element type.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than <see cref="NrbfArray.Count"/>.
        /// </exception>
        public NrbfNode this[int index]
        {
            get => Array[index];
            set => Array[index] = value;
        }

        /// <summary>
        /// Creates an <see cref="IList{NrbfNode}"/> wrapper for an <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="array">The underlying <see cref="NrbfArray"/>.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="array"/> does not have <see cref="NrbfArray.Rank"/> equal to 1.
        /// </exception>
        public NrbfArrayListAdapter(NrbfArray array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (array.Rank != 1)
                throw new ArgumentException("The array must have exactly one dimension.");

            Array = array;
        }

        /// <inheritdoc cref="NrbfArray.GetEnumerator"/>
        public IEnumerator<NrbfNode> GetEnumerator() =>
            Array.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Adds an item to the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="NrbfArray"/>.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="item"/> cannot be converted to the array element type. 
        /// </exception>
        public void Add(NrbfNode item) =>
            Array.Add(item);

        /// <summary>
        /// Removes all items from the <see cref="NrbfArray"/>.
        /// </summary>
        public void Clear() => 
            Array.Clear();

        /// <inheritdoc cref="NrbfArray.Contains"/>
        public bool Contains(NrbfNode item) => 
            Array.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="NrbfArray"/> to an Array,
        /// starting at a particular Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional Array that is the destination of the elements copied from <see cref="NrbfArray"/>.
        /// The Array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(NrbfNode[] array, int arrayIndex) => 
            Array.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="NrbfArray"/>.</param>
        /// <returns>
        /// <see langword="true"/> if item was successfully removed from the <see cref="NrbfArray"/>;
        /// otherwise, <see langword="false"/>.
        /// This method also returns <see langword="false"/> if item is not found in the original <see cref="NrbfArray"/>.
        /// </returns>
        public bool Remove(NrbfNode item) => 
            Array.Remove(item);

        /// <summary>
        /// Determines the index of a specific item in the <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="NrbfArray"/>.</param>
        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
        public int IndexOf(NrbfNode item) => 
            Array.IndexOf(item);

        /// <summary>
        /// Inserts an item to the <see cref="NrbfArray"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="NrbfArray"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="NrbfArray"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="item"/> cannot be converted to the array element type. 
        /// </exception>
        public void Insert(int index, NrbfNode item) => 
            Array.Insert(index, item);

        /// <summary>
        /// Removes the <see cref="NrbfArray"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is not a valid index in the <see cref="NrbfArray"/>.
        /// </exception>
        public void RemoveAt(int index) => 
            Array.RemoveAt(index);
    }
}