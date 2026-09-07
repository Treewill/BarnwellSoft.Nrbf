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

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// An NRBF class member.
    /// </summary>
    public sealed class NrbfClassMember
    {
        /// <summary>
        /// The name of the member.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the member.
        /// </summary>
        public NrbfMemberType Type { get; }

        /// <summary>
        /// True if the type should be serialized typed;
        /// false otherwise. 
        /// </summary>
        public bool SerializeTyped { get; set; }

        /// <summary>
        /// Creates an NRBF class member.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="type">The type of the member.</param>
        /// <param name="serializeTyped">If the member should be serialized typed.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is null.
        /// -or-
        /// <paramref name="type"/> is null.
        /// </exception>
        public NrbfClassMember(
            string name,
            NrbfMemberType type,
            bool serializeTyped = false)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (type is null)
                throw new ArgumentNullException(nameof(type));

            Name = name;
            Type = type;
            SerializeTyped = serializeTyped;
        }
    }
}