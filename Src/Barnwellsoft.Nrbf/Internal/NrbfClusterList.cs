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
using System.Linq;

using Barnwellsoft.Nrbf.Internal.RedBlackTree;

namespace Barnwellsoft.Nrbf.Internal
{
    using TreePointer = RedBlackTreeNodePointer<NrbfClusterListNodeData>;
    using TreeNode = RedBlackTreeNode<NrbfClusterListNodeData>;

    internal sealed class NrbfClusterList : IList<NrbfNode>
    {
        private TreePointer _tree = new TreePointer(null);

        public int Count => _tree.Target?.Value.TotalCount ?? 0;

        public bool IsReadOnly => false;

        public NrbfNode this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                return GetValue(index);
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                SetValue(index, value);
            }
        }

        private NrbfNode GetValue(int index)
        {
            var values = GetNode(ref index).Value.Values;

            if (index < values.Count)
                return values[index];

            return null;
        }

        private void SetValue(int index, NrbfNode value)
        {
            var node = GetNode(ref index);
            SetItem(node, index, value);
        }

        public IEnumerable<NrbfArrayBlock> EnumerateBlocks() =>
            EnumerateTreeNodes(_tree.Target)
                .Select(node => node.Value)
                .Select(data => new NrbfArrayBlock(data.Values, data.NullCount));

        public IEnumerator<NrbfNode> GetEnumerator() =>
            EnumerateTreeNodes(_tree.Target)
                .SelectMany(EnumerateTreeNodeItems)
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(NrbfNode item)
        {
            if (item is null)
            {
                AddNulls(1);
                return;
            }

            var node = EnsureRoot();

            var next = node.GetSide(RedBlackTreeSide.After);
            while (!(next is null))
            {
                node = next;
                next = node.GetSide(RedBlackTreeSide.After);
            }

            var data = node.Value;

            if (data.NullCount != 0)
            {
                node = InsertAfter(node);
                data = node.Value;
            }

            data.Values.Add(item);
            UpdateTree(node);
        }

        public void AddNulls(int count)
        {
            var node = EnsureRoot();

            var next = node.GetSide(RedBlackTreeSide.After);
            while (!(next is null))
            {
                node = next;
                next = node.GetSide(RedBlackTreeSide.After);
            }

            node.Value.NullCount += count;
            UpdateTree(node);
        }

        public void Clear()
        {
            _tree.Target = null;
        }

        public bool Contains(NrbfNode item) => 
            IndexOf(item) != -1;

        public void CopyTo(NrbfNode[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length - arrayIndex < Count)
                throw new ArgumentException(
                    "The number of elements in the source is greater than the available space from arrayIndex to the end of the destination array.",
                    nameof(array));

            foreach (var item in this)
            {
                array[arrayIndex] = item;
                ++arrayIndex;
            }
        }

        public bool Remove(NrbfNode item)
        {
            if (IndexOf(item, out var node, out var elementIndex) == -1)
                return false;

            RemoveItem(node, elementIndex);
            return true;
        }

        public int IndexOf(NrbfNode item) =>
            IndexOf(item, out _, out _);

        private int IndexOf(NrbfNode item, out TreeNode node, out int elementIndex)
        {
            node = null;
            elementIndex = -1;
            var index = 0;
            var iterator = EnumerateTreeNodes(_tree.Target)
                .GetEnumerator();
            using (iterator)
            {
                if (!iterator.MoveNext())
                    return -1;

                var data = iterator.Current.Value;
                if (item is null)
                {
                    if (data.NullCount == 0)
                        return -1;
                    node = iterator.Current;
                    elementIndex = data.Values.Count;
                    return index + elementIndex;
                }

                do
                {
                    data = iterator.Current.Value;
                    elementIndex = data.Values.IndexOf(item);
                    if (elementIndex != -1)
                    {
                        node = iterator.Current;
                        return index + elementIndex;
                    }

                    index += data.Values.Count + data.NullCount;
                } while (iterator.MoveNext());

                return -1;
            }
        }

        public void Insert(int index, NrbfNode item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            EnsureRoot();

            var node = GetNode(ref index);
            InsertItem(node, index, item);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var node = GetNode(ref index);
            RemoveItem(node, index);
        }

        private TreeNode GetNode(ref int index)
        {
            var node = _tree.Target;
            if (node is null)
                return null;

            while (true)
            {
                var data = node.Value;

                if (index < data.PriorCount)
                {
                    node = node.Before;
                    continue;
                }

                index -= data.PriorCount;

                var nodeElementCount = data.Values.Count + data.NullCount;
                if (index < nodeElementCount)
                    return node;

                if (node.After is null && index == nodeElementCount)
                    return node;

                index -= nodeElementCount;
                node = node.After;
            }
        }

        private static IEnumerable<TreeNode> EnumerateTreeNodes(TreeNode node)
        {
            while (!(node?.Before is null))
                node = node.Before;

            while (!(node is null))
            {
                yield return node;
                node = node.Traverse(RedBlackTreeSide.After);
            }
        }

        private static IEnumerable<NrbfNode> EnumerateTreeNodeItems(TreeNode node)
        {
            var data = node.Value;
            foreach (var item in data.Values)
                yield return item;

            for (var i = 0; i < data.NullCount; ++i)
                yield return null;
        }

        private static void RemoveItem(TreeNode node, int elementIndex)
        {
            var data = node.Value;

            if (elementIndex < data.Values.Count)
            {
                var nodeBefore = node.Traverse(RedBlackTreeSide.Before);
                if (data.Values.Count == 1 && !(nodeBefore is null))
                {
                    nodeBefore.Value.NullCount += data.NullCount;
                    node.Remove(UpdateTreeNode);
                    UpdateTree(nodeBefore);
                    return;
                }

                data.Values.RemoveAt(elementIndex);
                UpdateTree(node);
                return;
            }

            var nodeAfter = node.Traverse(RedBlackTreeSide.After);
            if (data.NullCount == 1 && !(nodeAfter is null))
            {
                var dataAfter = nodeAfter.Value;
                data.Values.AddRange(dataAfter.Values);
                data.NullCount = dataAfter.NullCount;
                nodeAfter.Remove(UpdateTreeNode);
                UpdateTree(node);
                return;
            }

            data.NullCount -= 1;
            UpdateTree(node);
        }

        private static void InsertItem(TreeNode node, int elementIndex, NrbfNode item)
        {
            var data = node.Value;

            TreeNode nodeAfter;
            NrbfClusterListNodeData dataAfter;

            if (elementIndex < data.Values.Count)
            {
                if (!(item is null))
                {
                    data.Values.Insert(elementIndex, item);
                    UpdateTree(node);
                    return;
                }

                var nodeBefore = node.Traverse(RedBlackTreeSide.Before);
                if (elementIndex == 0 && !(nodeBefore is null))
                {
                    nodeBefore.Value.NullCount += 1;
                    UpdateTree(nodeBefore);
                    return;
                }

                nodeAfter = InsertAfter(node);
                dataAfter = nodeAfter.Value;
                dataAfter.Values.AddRange(data.Values.Skip(elementIndex));
                dataAfter.NullCount = data.NullCount;

                data.NullCount = 1;
                data.Values.RemoveRange(elementIndex, data.Values.Count - elementIndex);

                UpdateTree(node);
                UpdateTree(nodeAfter);
                return;
            }

            if (item is null)
            {
                data.NullCount += 1;
                UpdateTree(node);
                return;
            }

            var nullIndex = elementIndex - data.Values.Count;

            nodeAfter = InsertAfter(node);
            dataAfter = nodeAfter.Value;

            dataAfter.Values.Add(item);
            dataAfter.NullCount = data.NullCount - nullIndex;

            data.NullCount = nullIndex;

            UpdateTree(node);
            UpdateTree(nodeAfter);
        }

        private static void SetItem(TreeNode node, int elementIndex, NrbfNode item)
        {
            var data = node.Value;

            TreeNode nodeAfter;
            NrbfClusterListNodeData dataAfter;

            if (elementIndex < data.Values.Count)
            {
                if (!(item is null))
                {
                    data.Values[elementIndex] = item;
                    return;
                }

                if (data.Values.Count == 1)
                {
                    var nodeBefore = node.Traverse(RedBlackTreeSide.Before);
                    if (!(nodeBefore is null))
                    {
                        var dataBefore = nodeBefore.Value;
                        dataBefore.NullCount += 1 + data.NullCount;
                        data.Values.RemoveAt(0);
                        node.Remove(UpdateTreeNode);
                        UpdateTree(nodeBefore);
                        return;
                    }
                }

                if (elementIndex + 1 == data.Values.Count)
                {
                    data.Values.RemoveAt(elementIndex);
                    data.NullCount += 1;
                    return;
                }

                nodeAfter = InsertAfter(node);
                dataAfter = nodeAfter.Value;

                dataAfter.Values.AddRange(data.Values.Skip(elementIndex + 1));
                dataAfter.NullCount = data.NullCount;

                data.Values.RemoveRange(elementIndex, data.Values.Count - elementIndex);
                data.NullCount = 1;

                UpdateTree(node);
                UpdateTree(nodeAfter);
                return;
            }

            if (item is null)
                return;

            var nullIndex = elementIndex - data.Values.Count;

            if (nullIndex == 0)
            {
                nodeAfter = node.Traverse(RedBlackTreeSide.After);
                if (data.NullCount == 1 && !(nodeAfter is null))
                {
                    dataAfter = nodeAfter.Value;
                    data.Values.Add(item);
                    data.Values.AddRange(dataAfter.Values);
                    data.NullCount = dataAfter.NullCount;
                    nodeAfter.Remove(UpdateTreeNode);
                    UpdateTree(node);
                    return;
                }

                data.Values.Add(item);
                data.NullCount -= 1;
                return;
            }

            nodeAfter = node.Traverse(RedBlackTreeSide.After);
            if (nullIndex + 1 == data.NullCount && !(nodeAfter is null))
            {
                dataAfter = nodeAfter.Value;
                data.NullCount -= 1;
                dataAfter.Values.Insert(0, item);
                UpdateTree(node);
                UpdateTree(nodeAfter);
                return;
            }

            nodeAfter = InsertAfter(node);
            dataAfter = nodeAfter.Value;

            dataAfter.Values.Add(item);
            dataAfter.NullCount = data.NullCount - nullIndex - 1;
            data.NullCount = nullIndex;
            UpdateTree(node);
            UpdateTree(nodeAfter);
        }

        private TreeNode EnsureRoot()
        {
            var node = _tree.Target;
            if (!(node is null))
                return node;

            node = new TreeNode(_tree);
            node.Value = new NrbfClusterListNodeData();
            return node;
        }

        private static TreeNode InsertAfter(TreeNode node) =>
            node.Insert(
                RedBlackTreeSide.After,
                new NrbfClusterListNodeData(),
                UpdateTreeNode);


        private static void UpdateTree(TreeNode node)
        {
            foreach (var ancestor in TreeNode.Ancestry(node))
                UpdateTreeNode(ancestor);
        }

        private static void UpdateTreeNode(TreeNode node)
        {
            var beforeCount = node.Before?.Value.TotalCount ?? 0;
            var afterCount = node.After?.Value.TotalCount ?? 0;

            var data = node.Value;
            var innerCount = data.Values.Count + data.NullCount;

            node.Value.PriorCount = beforeCount;
            node.Value.TotalCount =
                beforeCount + innerCount + afterCount;
        }
    }
}