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

namespace Barnwellsoft.Nrbf.Internal
{
    /// <summary>
    /// A library ID.
    /// </summary>
    internal readonly struct NrbfLibraryId : IEquatable<NrbfLibraryId>
    {
        /// <summary>
        /// The ID value.
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Creates a library ID.
        /// </summary>
        /// <param name="value">The ID value.</param>
        public NrbfLibraryId(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Indicates whether the current library ID is equal to another library ID.
        /// </summary>
        /// <param name="other">A library ID to compare with this library ID.</param>
        /// <returns>
        /// <see langword="true"/>if this library ID is equal to <paramref name="other"/>;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool Equals(NrbfLibraryId other) => 
            Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object obj) => 
            obj is NrbfLibraryId other && Equals(other);

        /// <inheritdoc/>
        public override int GetHashCode() => 
            Value;
    }
}