using static System.Console;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    /// <summary>
    /// Iterator Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create and item collection
            ItemCollection<Item> items = [
                    new("Item 0"),
                new("Item 1"),
                new("Item 2"),
                new("Item 3"),
                new("Item 4"),
                new("Item 5"),
                new("Item 6"),
                new("Item 7"),
                new("Item 8")
                  ];

            WriteLine("Iterate front to back");
            foreach (var item in items)
            {
                WriteLine(item.Name);
            }

            WriteLine("\nIterate back to front");
            foreach (var item in items.BackToFront)
            {
                WriteLine(item.Name);
            }
            WriteLine();

            // Iterate given range and step over even ones
            WriteLine("\nIterate range (1-7) in steps of 2");
            foreach (var item in items.FromToStep(1, 7, 2))
            {
                WriteLine(item.Name);
            }
            WriteLine();

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'ConcreteAggregate' class
    /// </summary>
    /// <typeparam name="T">Collection item type</typeparam>
    public class ItemCollection<T> : IEnumerable<T>
    {
        private readonly List<T> items = [];

        public void Add(T t) => items.Add(t);

        // The 'ConcreteIterator'
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        public IEnumerable<T> FrontToBack { get => this; }

        public IEnumerable<T> BackToFront
        {
            get
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    yield return items[i];
                }
            }
        }

        public IEnumerable<T> FromToStep(int from, int to, int step)
        {
            for (int i = from; i <= to; i += step)
            {
                yield return items[i];
            }
        }

        // Gets number of items
        public int Count { get => items.Count; }

        // System.Collections.IEnumerable member implementation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// The collection item
    /// </summary>
    internal record Item(string Name);
}