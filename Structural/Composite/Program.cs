﻿//Compose objects into tree structures to represent part-whole hierarchies. Composite lets clients treat individual objects and compositions of objects uniformly.

//The classes and objects participating in this pattern are:
//    Component   (DrawingElement)
//        declares the interface for objects in the composition.
//        implements default behavior for the interface common to all classes, as appropriate.
//        declares an interface for accessing and managing its child components.
//        (optional) defines an interface for accessing a component's parent in the recursive structure, and implements it if that's appropriate.
//    Leaf   (PrimitiveElement)
//        represents leaf objects in the composition. A leaf has no children.
//        defines behavior for primitive objects in the composition.
//    Composite   (CompositeElement)
//        defines behavior for components having children.
//        stores child components.
//        implements child-related operations in the Component interface.
//    Client  (CompositeApp)
//        manipulates objects in the composition through the Component interface.

using System;
using System.Collections.Generic;
namespace Composite
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Composite Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create a tree structure
            CompositeElement root =
              new CompositeElement("Picture");
            root.Add(new PrimitiveElement("Red Line"));
            root.Add(new PrimitiveElement("Blue Circle"));
            root.Add(new PrimitiveElement("Green Box"));
            // Create a branch
            CompositeElement comp =
              new CompositeElement("Two Circles");
            comp.Add(new PrimitiveElement("Black Circle"));
            comp.Add(new PrimitiveElement("White Circle"));
            root.Add(comp);
            // Add and remove a PrimitiveElement
            PrimitiveElement pe =
              new PrimitiveElement("Yellow Line");
            root.Add(pe);
            root.Remove(pe);
            // Recursively display nodes
            root.Display(1);
            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Component' Treenode
    /// </summary>
    abstract class DrawingElement
    {
        protected string _name;
        // Constructor
        public DrawingElement(string name)
        {
            this._name = name;
        }
        public abstract void Add(DrawingElement d);
        public abstract void Remove(DrawingElement d);
        public abstract void Display(int indent);
    }
    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    class PrimitiveElement : DrawingElement
    {
        // Constructor
        public PrimitiveElement(string name)
            : base(name)
        {
        }
        public override void Add(DrawingElement c)
        {
            Console.WriteLine(
              "Cannot add to a PrimitiveElement");
        }
        public override void Remove(DrawingElement c)
        {
            Console.WriteLine(
              "Cannot remove from a PrimitiveElement");
        }
        public override void Display(int indent)
        {
            Console.WriteLine(
              new String('-', indent) + " " + _name);
        }
    }
    /// <summary>
    /// The 'Composite' class
    /// </summary>
    class CompositeElement : DrawingElement
    {
        private List<DrawingElement> elements =
          new List<DrawingElement>();
        // Constructor
        public CompositeElement(string name)
            : base(name)
        {
        }
        public override void Add(DrawingElement d)
        {
            elements.Add(d);
        }
        public override void Remove(DrawingElement d)
        {
            elements.Remove(d);
        }
        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) +
              "+ " + _name);
            // Display each child element on this node
            foreach (DrawingElement d in elements)
            {
                d.Display(indent + 2);
            }
        }
    }
}