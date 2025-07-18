﻿using static System.Console;
using System;
using System.Collections.Generic;

namespace Strategy
{
    /// <summary>
    /// Strategy Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Two contexts following different strategies
            SortedList students =
                  [
                    new (Name:"Samual", Ssn: "154-33-2009"),
                new (Name:"Jimmy", Ssn: "487-43-1665" ),
                new (Name:"Sandra", Ssn:  "655-00-2944" ),
                new (Name:"Vivek", Ssn:  "133-98-8399" ),
                new (Name:"Anna", Ssn: "760-94-9844" )
                  ];

            students.SortStrategy = new QuickSort();
            students.SortStudents();

            students.SortStrategy = new ShellSort();
            students.SortStudents();

            students.SortStrategy = new MergeSort();
            students.SortStudents();

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Strategy' interface
    /// </summary>
    public interface ISortStrategy
    {
        void Sort(List<Student> list);
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    public class QuickSort : ISortStrategy
    {
        public void Sort(List<Student> list)
        {
            // Call overloaded Sort
            Sort(list, 0, list.Count - 1);
            WriteLine("QuickSorted list ");
        }

        // Recursively sort
        private static void Sort(List<Student> list, int left, int right)
        {
            int lhold = left;
            int rhold = right;

            // Use a random pivot
            var random = new Random();
            int pivot = random.Next(left, right);
            Swap(list, pivot, left);
            pivot = left;
            left++;

            while (right >= left)
            {
                int compareleft = list[left].Name.CompareTo(list[pivot].Name);
                int compareright = list[right].Name.CompareTo(list[pivot].Name);

                if ((compareleft >= 0) && (compareright < 0))
                {
                    Swap(list, left, right);
                }
                else
                {
                    if (compareleft >= 0)
                    {
                        right--;
                    }
                    else
                    {
                        if (compareright < 0)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                            left++;
                        }
                    }
                }
            }
            Swap(list, pivot, right);
            pivot = right;

            if (pivot > lhold) Sort(list, lhold, pivot);
            if (rhold > pivot + 1) Sort(list, pivot + 1, rhold);
        }

        // Swap helper function
        private static void Swap(List<Student> list, int left, int right)
        {
            // Tuple based swap
            (list[left], list[right]) = (list[right], list[left]);
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    public class ShellSort : ISortStrategy
    {
        public void Sort(List<Student> list)
        {
            // ShellSort();  not-implemented
            WriteLine("ShellSorted list ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    public class MergeSort : ISortStrategy
    {
        public void Sort(List<Student> list)
        {
            // MergeSort(); not-implemented
            WriteLine("MergeSorted list ");
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    public class SortedList : List<Student>
    {
        // Sets sort strategy
        public ISortStrategy SortStrategy { get; set; } = null!;

        // Perform sort
        public void SortStudents()
        {
            SortStrategy.Sort(this);

            // Display sort results
            foreach (var student in this)
            {
                WriteLine($" {student.Name}");
            }
            WriteLine();
        }
    }

    /// <summary>
    /// Represents a student
    /// </summary>
    public record Student(string Name, string Ssn);
}