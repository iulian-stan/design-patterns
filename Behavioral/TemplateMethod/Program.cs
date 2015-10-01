using static System.Console;
using System.Collections.Generic;

namespace TemplateMethod
{
    /// <summary>
    /// Template Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            var categories = new CategoryAccessor();
            categories.Run(5);

            var products = new ProductAccessor();
            products.Run(3);

            // Wait for user
            ReadKey();
        }
    }

    public record Category
    {
        public string CategoryName { get; set; } = null!;
    }

    public record Product
    {
        public string ProductName { get; set; } = null!;
    }

    /// <summary>
    /// The 'AbstractClass' abstract class
    /// </summary>
    public abstract class DataAccessor<T> where T : class, new()
    {
        protected List<T> Items { get; set; } = [];

        public virtual void Connect()
        {
            Items.Clear();
        }
        public abstract void Select();
        public abstract void Process(int top);
        public virtual void Disconnect()
        {
            Items.Clear();
        }

        // The 'Template Method' 
        public void Run(int top)
        {
            Connect();
            Select();
            Process(top);
            Disconnect();
        }
    }

    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    public class CategoryAccessor : DataAccessor<Category>
    {
        public override void Select()
        {
            Items.Add(new() { CategoryName = "Red" });
            Items.Add(new() { CategoryName = "Green" });
            Items.Add(new() { CategoryName = "Blue" });
            Items.Add(new() { CategoryName = "Yellow" });
            Items.Add(new() { CategoryName = "Purple" });
            Items.Add(new() { CategoryName = "White" });
            Items.Add(new() { CategoryName = "Black" });
        }

        public override void Process(int top)
        {
            WriteLine("Categories ---- ");

            for (int i = 0; i < top; i++)
            {
                WriteLine(Items[i].CategoryName);
            }

            WriteLine();
        }
    }

    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    public class ProductAccessor : DataAccessor<Product>
    {
        public override void Select()
        {
            Items.Add(new Product { ProductName = "Car" });
            Items.Add(new Product { ProductName = "Bike" });
            Items.Add(new Product { ProductName = "Boat" });
            Items.Add(new Product { ProductName = "Truck" });
            Items.Add(new Product { ProductName = "Moped" });
            Items.Add(new Product { ProductName = "Rollerskate" });
            Items.Add(new Product { ProductName = "Stroller" });
        }

        public override void Process(int top)
        {
            WriteLine("Products ---- ");

            for (int i = 0; i < top; i++)
            {
                WriteLine(Items[i].ProductName);
            }

            WriteLine();
        }
    }
}