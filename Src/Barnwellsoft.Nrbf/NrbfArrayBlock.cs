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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// A block of values and nulls from an <see cref="NrbfArray"/>.
    /// </summary>
    public class NrbfArrayBlock
    {
        /// <summary>
        /// The values.
        /// </summary>
        public ReadOnlyCollection<NrbfNode> Values { get; }

        /// <summary>
        /// The number of nulls that follow the <see cref="Values"/>.
        /// </summary>
        public int NullCount { get; }

        /// <summary>
        /// Creates a block of values and nulls from an <see cref="NrbfArray"/>.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="nullCount">
        /// The number of nulls that follow the <paramref name="values"/>.
        /// </param>
        public NrbfArrayBlock(
            IList<NrbfNode> values,
            int nullCount)
        {
            Values = new ReadOnlyCollection<NrbfNode>(values);
            NullCount = nullCount;
        }
    }
}