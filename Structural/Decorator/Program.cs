using static System.Console;
using System.Collections.Generic;

namespace Decorator
{
    /// <summary>
    /// Decorator Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create book
            var book = new Book("Worley", "Inside ASP.NET", 10);
            book.Display();

            // Create video
            var video = new Video("Spielberg", "Jaws", 23, 92);
            video.Display();

            // Make video borrowable, then borrow and display
            WriteLine("\nMaking video borrowable:");

            var borrow = new Borrowable<Video>(video);
            borrow.BorrowItem("Customer #1");
            borrow.BorrowItem("Customer #2");

            borrow.Display();

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    public abstract class LibraryItem<T>
    {
        // Each T has its own NumCopies
        public static int NumCopies { get; set; }

        public abstract void Display();
    }

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    public class Book : LibraryItem<Book>
    {
        private readonly string author;
        private readonly string title;

        // Constructor
        public Book(string author, string title, int numCopies)
        {
            this.author = author;
            this.title = title;
            NumCopies = numCopies;
        }

        public override void Display()
        {
            WriteLine("\nBook ------ ");
            WriteLine($" Author: {author}");
            WriteLine($" Title: {title}");
            WriteLine($" # Copies: {NumCopies}");
        }
    }

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    public class Video : LibraryItem<Video>
    {
        private readonly string director;
        private readonly string title;
        private readonly int playTime;

        // Constructor
        public Video(string director, string title,
            int numCopies, int playTime)
        {
            this.director = director;
            this.title = title;
            NumCopies = numCopies;
            this.playTime = playTime;
        }

        public override void Display()
        {
            WriteLine("\nVideo ----- ");
            WriteLine($" Director: {director}");
            WriteLine($" Title: {title}");
            WriteLine($" # Copies: {NumCopies}");
            WriteLine($" Playtime: {playTime}\n");
        }
    }

    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    public abstract class Decorator<T>(LibraryItem<T> libraryItem) : LibraryItem<T>
    {
        public override void Display() => libraryItem.Display();

    }

    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    public class Borrowable<T> : Decorator<T>
    {
        private readonly List<string> borrowers = [];

        // Constructor
        public Borrowable(LibraryItem<T> libraryItem)
            : base(libraryItem)
        {
        }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            NumCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            NumCopies++;
        }

        public override void Display()
        {
            base.Display();
            borrowers.ForEach(b => WriteLine(" borrower: " + b));
        }
    }
}