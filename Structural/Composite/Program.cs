using static System.Console;
using System;
using System.Collections.Generic;

namespace Composite
{
    /// <summary>
    /// Composite Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Build a tree of shapes
            var root = new TreeNode<Shape> { Node = new("Picture") };

            root.Add(new("Red Line"));
            root.Add(new("Blue Circle"));
            root.Add(new("Green Box"));

            var branch = root.Add(new Shape("Two Circles"));
            branch.Add(new("Black Circle"));
            branch.Add(new("White Circle"));

            // Add, remove, and add a shape
            var shape = new Shape("Yellow Line");
            root.Add(shape);
            root.Remove(shape);
            root.Add(shape);

            // Display tree using static method
            TreeNode<Shape>.Display(root, 1);

            ReadKey();
        }
    }

    /// <summary>
    /// Generic tree node class
    /// </summary>
    /// <typeparam name="T">Node type</typeparam>
    public class TreeNode<T> where T : IComparable<T>
    {

        // Add a child tree node
        public TreeNode<T> Add(T child)
        {
            var newNode = new TreeNode<T> { Node = child };
            Children.Add(newNode);
            return newNode;
        }

        // Remove a child tree node
        public void Remove(T child)
        {
            foreach (var treeNode in Children)
            {
                if (treeNode.Node.CompareTo(child) == 0)
                {
                    Children.Remove(treeNode);
                    return;
                }
            }
        }

        // Gets or sets the node
        public T Node { get; set; } = default!;

        // Gets treenode children
        public List<TreeNode<T>> Children { get; } = [];

        // Recursively displays node and its children 
        public static void Display(TreeNode<T> node, int indentation)
        {
            var line = new string('-', indentation);
            WriteLine(line + " " + node.Node);

            node.Children.ForEach(n => Display(n, indentation + 1));
        }
    }

    /// <summary>
    /// Shape class
    /// <remarks>
    /// Implements generic IComparable interface
    /// </remarks>
    /// </summary>
    public class Shape(string name) : IComparable<Shape>
    {
        public override string ToString() => name;

        // IComparable<Shape> Member
        public int CompareTo(Shape? other) => (this == other) ? 0 : -1;
    }
}