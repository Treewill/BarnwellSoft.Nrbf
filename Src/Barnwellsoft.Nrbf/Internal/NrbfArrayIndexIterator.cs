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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Barnwellsoft.Nrbf.Internal
{
    internal sealed class NrbfArrayIndexIterator
    {
        private int[] _skip;

        public int[] Current { get; }

        public ReadOnlyCollection<NrbfArrayDimension> Dimensions { get; }

        public NrbfArrayIndexIterator(
            IEnumerable<NrbfArrayDimension> dimensions)
        {
            Dimensions = new ReadOnlyCollection<NrbfArrayDimension>(dimensions.ToArray());
            Current = Dimensions.Select(dimension => dimension.Offset).ToArray();
            _skip = new int[Dimensions.Count];
        }

        public bool Skip(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var rank = Dimensions.Count;
            var index = rank;
            while (count > 0 && index > 0)
            {
                --index;
                _skip[index] = count % Dimensions[index].Length;
                count = count / Dimensions[index].Length;
            }

            if (count > 0)
                return false;

            while (index > 0)
            {
                --index;
                _skip[index] = 0;
            }

            index = rank;
            var carry = 0;
            while (index > 0)
            {
                --index;
                var a = Current[index] + carry - Dimensions[index].Offset;
                var b = _skip[index];
                var m = Dimensions[index].Length;
                if (m - a <= b)
                {
                    carry = 1;
                    Current[index] = a - m + b + Dimensions[index].Offset;
                }
                else
                {
                    carry = 0;
                    Current[index] = a + b + Dimensions[index].Offset;
                }
            }

            return carry == 0;
        }
    }
}