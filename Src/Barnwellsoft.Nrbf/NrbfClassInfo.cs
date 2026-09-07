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

namespace Barnwellsoft.Nrbf
{
    /// <summary>
    /// Class info for an NRBF class.
    /// </summary>
    public sealed class NrbfClassInfo
    {
        /// <summary>
        /// The member type for the class.
        /// </summary>
        public NrbfMemberType MemberType { get; }

        /// <summary>
        /// The name of the class.
        /// </summary>
        public string ClassName => MemberType.ClassName;

        /// <summary>
        /// The library containing the class.
        /// </summary>
        public NrbfLibrary Library => MemberType.Library;

        /// <summary>
        /// The members of the class.
        /// </summary>
        public ReadOnlyCollection<NrbfClassMember> Members { get; }

        /// <summary>
        /// The member indices.
        /// </summary>
        internal ReadOnlyDictionary<string, int> MemberIndices { get; }

        /// <summary>
        /// Creates class info for an NRBF class.
        /// </summary>
        /// <param name="memberType">The class type.</param>
        /// <param name="members">The members of the class.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="memberType"/> is null.
        /// -or-
        /// <paramref name="members"/> is null.
        /// </exception>
        public NrbfClassInfo(
            NrbfMemberType memberType,
            IEnumerable<NrbfClassMember> members)
        {
            if (memberType is null)
                throw new ArgumentNullException(nameof(memberType));

            if (members is null)
                throw new ArgumentNullException(nameof(members));

            MemberType = memberType;
            var membersArray = members.ToArray();

            if (membersArray.Any(member => member is null))
                throw new ArgumentException("Members cannot be null.", nameof(members));

            Members = new ReadOnlyCollection<NrbfClassMember>(membersArray);
            MemberIndices = new ReadOnlyDictionary<string, int>(CreateMemberIndices(membersArray));
        }

        /// <summary>
        /// Creates the member indices dictionary.
        /// </summary>
        /// <param name="members">The class members.</param>
        /// <returns>The member indices dictionary.</returns>
        private static Dictionary<string, int> CreateMemberIndices(
            NrbfClassMember[] members)
        {
            var indices = new Dictionary<string, int>();
            for (var i = 0; i < members.Length; ++i) 
                indices.Add(members[i].Name, i);

            return indices;
        }

        /// <summary>
        /// The <see cref="System.Boolean"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Boolean { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Boolean);

        /// <summary>
        /// The <see cref="System.Byte"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Byte { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Byte);

        /// <summary>
        /// The <see cref="System.Char"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Char { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Char);

        /// <summary>
        /// The <see cref="System.Double"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Double { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Double);

        /// <summary>
        /// The <see cref="System.Int16"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Int16 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Int16);

        /// <summary>
        /// The <see cref="System.Int32"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Int32 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Int32);

        /// <summary>
        /// The <see cref="System.Int64"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Int64 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Int64);

        /// <summary>
        /// The <see cref="System.SByte"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo SByte { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.SByte);

        /// <summary>
        /// The <see cref="System.Single"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Single { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.Single);

        /// <summary>
        /// The <see cref="System.UInt16"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo UInt16 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.UInt16);

        /// <summary>
        /// The <see cref="System.UInt32"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo UInt32 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.UInt32);

        /// <summary>
        /// The <see cref="System.UInt64"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo UInt64 { get; } =
            CreatePrimitiveClassInfo(NrbfMemberType.UInt64);

        /// <summary>
        /// The <see cref="System.Decimal"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo Decimal { get; } =
            new NrbfClassInfo(
                NrbfMemberType.Decimal, 
                new[]
                {
                    new NrbfClassMember("lo", NrbfMemberType.Int32),
                    new NrbfClassMember("mid", NrbfMemberType.Int32),
                    new NrbfClassMember("hi", NrbfMemberType.Int32),
                    new NrbfClassMember("flags", NrbfMemberType.Int32),
                });

        /// <summary>
        /// The <see cref="System.TimeSpan"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo TimeSpan { get; } =
            new NrbfClassInfo(
                NrbfMemberType.TimeSpan,
                new[]
                {
                    new NrbfClassMember("_ticks", NrbfMemberType.Int64),
                });

        /// <summary>
        /// The <see cref="System.DateTime"/> <see cref="NrbfClassInfo"/>.
        /// </summary>
        public static NrbfClassInfo DateTime { get; } =
            new NrbfClassInfo(
                NrbfMemberType.DateTime,
                new[]
                {
                    new NrbfClassMember("ticks", NrbfMemberType.Int64),
                    new NrbfClassMember("dateData", NrbfMemberType.UInt64),
                });


        /// <summary>
        /// Creates class info for the primitive.
        /// </summary>
        /// <param name="type">The primitive type.</param>
        /// <returns>The class info for the primitive.</returns>
        private static NrbfClassInfo CreatePrimitiveClassInfo(NrbfMemberType type) =>
            new NrbfClassInfo(
                type,
                new[]
                {
                    new NrbfClassMember("m_value", type),
                });
    }
}