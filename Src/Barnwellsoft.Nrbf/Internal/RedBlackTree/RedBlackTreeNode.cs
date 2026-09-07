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

namespace Barnwellsoft.Nrbf.Internal.RedBlackTree
{
    internal sealed class RedBlackTreeNode<TValue>
    {
        public TValue Value { get; set; }

        public RedBlackTreeNodeColor Color { get; set; }

        public RedBlackTreeNodePointer<TValue> ParentPointer { get; set; }

        public RedBlackTreeNodePointer<TValue> BeforePointer { get; }

        public RedBlackTreeNodePointer<TValue> AfterPointer { get; }

        public RedBlackTreeNode<TValue> Parent =>
            ParentPointer.Host;

        public RedBlackTreeNode<TValue> Before
        {
            get => BeforePointer.Target;
            set => BeforePointer.Target = value;
        }

        public RedBlackTreeNode<TValue> After
        {
            get => AfterPointer.Target;
            set => AfterPointer.Target = value;
        }

        public RedBlackTreeNode(RedBlackTreeNodePointer<TValue> parentPointer)
        {
            if (parentPointer is null)
                throw new ArgumentNullException(nameof(parentPointer));

            parentPointer.Target = this;
            BeforePointer = new RedBlackTreeNodePointer<TValue>(this);
            AfterPointer = new RedBlackTreeNodePointer<TValue>(this);
        }

        public RedBlackTreeNodePointer<TValue> GetSidePointer(RedBlackTreeSide side)
        {
            switch (side)
            {
                case RedBlackTreeSide.Before:
                    return BeforePointer;

                case RedBlackTreeSide.After:
                    return AfterPointer;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public RedBlackTreeNode<TValue> Traverse(RedBlackTreeSide side)
        {
            var node = GetSide(side);
            if (!(node is null))
            {
                var otherSide = side.GetOther();
                while (true)
                {
                    var next = node.GetSide(otherSide);
                    if (next is null)
                        return node;
                    node = next;
                }
            }

            var child = this;
            var parent = child.Parent;
            while (!(parent is null))
            {
                if (parent.GetChildSide(child) != side)
                    return parent;

                child = parent;
                parent = child.Parent;
            }

            return null;
        }

        public RedBlackTreeNode<TValue> GetSide(RedBlackTreeSide side)
        {
            switch (side)
            {
                case RedBlackTreeSide.Before:
                    return Before;

                case RedBlackTreeSide.After:
                    return After;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public void SetSide(RedBlackTreeSide side, RedBlackTreeNode<TValue> node)
        {
            switch (side)
            {
                case RedBlackTreeSide.Before:
                    Before = node;
                    break;

                case RedBlackTreeSide.After:
                    After = node;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        public RedBlackTreeSide GetChildSide(RedBlackTreeNode<TValue> child)
        {
            if (ReferenceEquals(child, Before))
                return RedBlackTreeSide.Before;

            if (ReferenceEquals(child, After))
                return RedBlackTreeSide.After;

            throw new ArgumentException("Child is not a direct child of this node", nameof(child));
        }

        public RedBlackTreeNode<TValue> GetEditNode(RedBlackTreeSide side)
        {
            if (GetSide(side) is null)
                return null;

            var other = side.GetOther();
            var node = GetSide(side);
            var next = node.GetSide(other);
            while (!(next is null))
            {
                node = next;
                next = next.GetSide(other);
            }

            return node;
        }

        public RedBlackTreeNode<TValue> Insert(
            RedBlackTreeSide side,
            TValue value,
            Action<RedBlackTreeNode<TValue>> onUpdate = null)
        {
            var parent = GetEditNode(side);

            RedBlackTreeNodePointer<TValue> pointer;
            if (parent is null)
            {
                pointer = GetSidePointer(side);
            }
            else
            {
                pointer = parent.GetSidePointer(side.GetOther());
            }

            var node = new RedBlackTreeNode<TValue>(pointer)
            {
                Color = RedBlackTreeNodeColor.Red,
                Value = value,
            };

            InsertFix(node, onUpdate);
            return node;
        }

        public void Remove(Action<RedBlackTreeNode<TValue>> onUpdate = null)
        {
            const RedBlackTreeSide side = RedBlackTreeSide.After; // Arbitrary.
            var editNode = GetEditNode(side);
            if (!(editNode is null))
                SwapNodes(editNode, this);

            var next = GetSide(side);

            if (!(next is null))
            {
                ParentPointer.Target = next;
                next.Color = RedBlackTreeNodeColor.Black;
                UpdateAncestry(ParentPointer.Host, onUpdate);
                return;
            }

            if (Color == RedBlackTreeNodeColor.Red)
            {
                ParentPointer.Target = null;
                UpdateAncestry(ParentPointer.Host, onUpdate);
                return;
            }

            RemoveFix(this, onUpdate);
        }

        private static void InsertFix(
            RedBlackTreeNode<TValue> node,
            Action<RedBlackTreeNode<TValue>> onUpdate)
        {
            // Note onUpdate will be called in a reverse BFS but child order is not guaranteed.
            // Note onUpdate may be called too many times on a given node.

            RedBlackTreeNode<TValue> parent = node.Parent;
            RedBlackTreeSide side;
            RedBlackTreeSide otherSide;
            RedBlackTreeNode<TValue> peer;

            while (true)
            {
                if (IsBlack(parent))
                {
                    UpdateAncestry(parent, onUpdate);
                    return;
                }

                node = parent;
                parent = node.Parent;
                node.Color = RedBlackTreeNodeColor.Black;

                onUpdate?.Invoke(node);

                if (parent is null)
                    return;

                side = parent.GetChildSide(node);
                otherSide = side.GetOther();

                peer = parent.GetSide(otherSide);

                if (IsBlack(peer))
                    break;

                peer.Color = RedBlackTreeNodeColor.Black;

                node = parent;
                parent = node.Parent;
                node.Color = RedBlackTreeNodeColor.Red;

                onUpdate?.Invoke(node);
            }

            var topPtr = parent.ParentPointer;
            if (IsBlack(node.GetSide(side)))
            {
                var root = node.GetSide(otherSide);

                parent.Color = RedBlackTreeNodeColor.Black;
                parent.SetSide(side, root.GetSide(otherSide));

                node.Color = RedBlackTreeNodeColor.Black;
                node.SetSide(otherSide, root.GetSide(side));

                root.Color = RedBlackTreeNodeColor.Black;
                root.SetSide(side, node);
                root.SetSide(otherSide, parent);

                topPtr.Target = root;

                if (onUpdate is null)
                    return;

                onUpdate.Invoke(parent);
                onUpdate.Invoke(node);
                UpdateAncestry(root, onUpdate);
            }
            else
            {
                parent.Color = RedBlackTreeNodeColor.Red;
                parent.SetSide(side, node.GetSide(otherSide));

                node.SetSide(otherSide, parent);

                topPtr.Target = node;

                if (onUpdate is null)
                    return;

                onUpdate.Invoke(parent);
                UpdateAncestry(node, onUpdate);
            }
        }

        private static void RemoveFix(
            RedBlackTreeNode<TValue> node,
            Action<RedBlackTreeNode<TValue>> onUpdate)
        {
            // Note onUpdate will be called in a reverse BFS but child order is not guaranteed.
            // Note onUpdate may be called too many times on a given node.

            var start = node;

            RedBlackTreeNode<TValue> parent;
            RedBlackTreeSide side;
            RedBlackTreeSide otherSide;
            RedBlackTreeNode<TValue> peer;
            RedBlackTreeNode<TValue> peerSide;
            RedBlackTreeNode<TValue> peerOther;

            while (true)
            {
                parent = node.Parent;
                if (parent is null)
                {
                    if (ReferenceEquals(node, start)) 
                        node.ParentPointer.Target = null;

                    return;
                }

                side = parent.GetChildSide(node);
                otherSide = side.GetOther();
                peer = parent.GetSide(otherSide);
                peerSide = peer.GetSide(side);
                peerOther = peer.GetSide(otherSide);

                var fullBlack =
                    parent.Color == RedBlackTreeNodeColor.Black
                    && peer.Color == RedBlackTreeNodeColor.Black
                    && IsBlack(peerSide)
                    && IsBlack(peerOther);

                if (!fullBlack)
                    break;

                peer.Color = RedBlackTreeNodeColor.Red;
                if (ReferenceEquals(node, start))
                {
                    start.ParentPointer.Target = null;
                }
                else
                {
                    onUpdate?.Invoke(node);
                }

                node = parent;
            }


            var topPtr = parent.ParentPointer;
            if (!IsBlack(peerSide))
            {
                parent.SetSide(otherSide, peerSide.GetSide(side));
                peer.SetSide(side, peerSide.GetSide(otherSide));

                peerSide.SetSide(side, parent);
                peerSide.SetSide(otherSide, peer);
                peerSide.Color = parent.Color;

                parent.Color = RedBlackTreeNodeColor.Black;

                topPtr.Target = peerSide;

                if (ReferenceEquals(node, start))
                {
                    start.ParentPointer.Target = null;
                }
                else
                {
                    onUpdate?.Invoke(node);
                }

                if (onUpdate is null)
                    return;

                onUpdate.Invoke(parent);
                onUpdate.Invoke(peer);
                UpdateAncestry(peerSide, onUpdate);
            }
            else if (!IsBlack(peerOther))
            {
                parent.SetSide(otherSide, peer.GetSide(side));

                peer.SetSide(side, parent);

                topPtr.Target = peer;

                if (ReferenceEquals(node, start))
                {
                    start.ParentPointer.Target = null;
                }
                else
                {
                    onUpdate?.Invoke(node);
                }

                if (onUpdate is null)
                    return;

                onUpdate.Invoke(parent);
                UpdateAncestry(peer, onUpdate);
            }
            else if (parent.Color == RedBlackTreeNodeColor.Red)
            {
                parent.Color = RedBlackTreeNodeColor.Black;
                peer.Color = RedBlackTreeNodeColor.Red;

                if (ReferenceEquals(node, start))
                {
                    start.ParentPointer.Target = null;
                }
                else
                {
                    onUpdate?.Invoke(node);
                }

                UpdateAncestry(parent, onUpdate);
            }
            else
            {
                var peerSideSide = peerSide.GetSide(side);
                if (IsBlack(peerSideSide))
                {
                    parent.Color = RedBlackTreeNodeColor.Red;
                    parent.SetSide(otherSide, peerSideSide);

                    peerSide.SetSide(side, parent);
                    peerSide.SetSide(otherSide, peerSide.GetSide(otherSide));

                    peer.Color = RedBlackTreeNodeColor.Black;

                    topPtr.Target = peer;

                    if (ReferenceEquals(node, start))
                    {
                        start.ParentPointer.Target = null;
                    }
                    else
                    {
                        onUpdate?.Invoke(node);
                    }

                    if (onUpdate is null)
                        return;

                    onUpdate.Invoke(parent);
                    onUpdate.Invoke(peerSide);
                    UpdateAncestry(peer, onUpdate);
                }
                else
                {
                    parent.SetSide(otherSide, peerSideSide.GetSide(side));

                    peerSide.SetSide(side, peerSideSide.GetSide(otherSide));

                    peerSideSide.SetSide(side, parent);
                    peerSideSide.SetSide(otherSide, peerSide);

                    peer.Color = RedBlackTreeNodeColor.Black;
                    peer.SetSide(side, peerSideSide);

                    topPtr.Target = peer;

                    if (ReferenceEquals(node, start))
                    {
                        start.ParentPointer.Target = null;
                    }
                    else
                    {
                        onUpdate?.Invoke(node);
                    }

                    if (onUpdate is null)
                        return;

                    onUpdate.Invoke(parent);
                    onUpdate.Invoke(peerSide);
                    onUpdate.Invoke(peerSideSide);

                    UpdateAncestry(peer, onUpdate);
                }
            }
        }

        private static void UpdateAncestry(
            RedBlackTreeNode<TValue> node,
            Action<RedBlackTreeNode<TValue>> onUpdate)
        {
            if (onUpdate is null)
                return;

            foreach (var ancestor in Ancestry(node)) 
                onUpdate.Invoke(ancestor);
        }

        public static bool IsBlack(RedBlackTreeNode<TValue> node) =>
            node is null || node.Color == RedBlackTreeNodeColor.Black;

        public static void SwapNodes(RedBlackTreeNode<TValue> a, RedBlackTreeNode<TValue> b)
        {
            RedBlackTreeNode<TValue> x;
            RedBlackTreeNode<TValue> y;

            if (ReferenceEquals(a, b.Parent))
            {
                y = a;
                x = b;
            }
            else
            {
                x = a;
                y = b;
            }

            var xColor = x.Color;
            var xBefore = x.Before;
            var xAfter = x.After;
            var xParentPtr = x.ParentPointer;

            var yColor = y.Color;
            var yBefore = y.Before;
            var yAfter = y.After;
            var yParentPtr = y.ParentPointer;

            y.Before = xBefore;
            y.After = xAfter;
            y.Color = xColor;

            yParentPtr.Target = x;
            x.Color = yColor;

            if (!ReferenceEquals(xParentPtr.Host, y))
            {
                xParentPtr.Target = y;
                x.Before = yBefore;
                x.After = yAfter;
            }
            else if (ReferenceEquals(yBefore, x))
            {
                x.Before = y;
                x.After = yAfter;
            }
            else
            {
                x.Before = yBefore;
                x.After = y;
            }
        }

        public static IEnumerable<RedBlackTreeNode<TValue>> Ancestry(RedBlackTreeNode<TValue> node)
        {
            while (!(node is null))
            {
                yield return node;
                node = node.Parent;
            }
        }
    }
}