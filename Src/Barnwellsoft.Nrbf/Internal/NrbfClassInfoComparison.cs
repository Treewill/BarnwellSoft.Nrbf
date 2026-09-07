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

using System.Linq;

namespace Barnwellsoft.Nrbf.Internal
{
    internal static class NrbfClassInfoComparison
    {
        /// <summary>
        /// Checks is two class info match.
        /// </summary>
        /// <param name="x">The first <see cref="NrbfClassInfo"/>.</param>
        /// <param name="y">The second <see cref="NrbfClassInfo"/>.</param>
        /// <returns>
        /// True if the two match;
        /// false otherwise.
        /// </returns>
        public static bool IsMatch(
            this NrbfClassInfo x,
            NrbfClassInfo y)
        {
            var equal =
                x.MemberType.Equals(y.MemberType)
                && x.Members.Count == y.Members.Count
                && x.Members.All(y.HasMember);

            return equal;
        }

        /// <summary>
        /// Checks if the class info has an equivalent member.
        /// </summary>
        /// <param name="info">The class info.</param>
        /// <param name="member">The member.</param>
        /// <returns>
        /// True if the class info has an equivalent member;
        /// false otherwise.
        /// </returns>
        public static bool HasMember(
            this NrbfClassInfo info,
            NrbfClassMember member)
        {
            if (!info.MemberIndices.TryGetValue(member.Name, out var index))
                return false;

            return member.Type.Equals(info.Members[index].Type);
        }
    }
}