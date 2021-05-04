﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Interpreter.Collections;

namespace Lab5
{
    internal class SyntaxTreeNode : IEnumerable<SyntaxTreeNode>
    {
        public NodeType NodeType { get; init; }
        public NodeData Data { get; set; }
        private readonly List<SyntaxTreeNode> _children = new ();

        public SyntaxTreeNode(NodeType nodeType, NodeData data) => (NodeType, Data) = (nodeType, data);
        public SyntaxTreeNode(NodeType nodeType, NodeData data, params SyntaxTreeNode[] children) : this(nodeType, data) 
            => _children.AddRange(children);

        public void AddChild(SyntaxTreeNode child) => _children.Add(child);
        public SyntaxTreeNode GetChild(int i) => _children[i];

        public IEnumerator<SyntaxTreeNode> GetEnumerator() => 
            _children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}