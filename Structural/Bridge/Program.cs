using static System.Console;
using System.Collections.Generic;

namespace Bridge
{
    /// <summary>
    /// Bridge Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create RefinedAbstraction and
            // set ConcreteImplementor
            var customers = new Customers(
                new CustomersData("Chicago"));

            // Exercise the bridge
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();

            customers.Add("Henry Velasquez");
            customers.ShowAll();

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Abstraction' class
    /// </summary>
    public class CustomersBase(IDataObject<string> dataObject)
    {
        public virtual void Next() => dataObject.NextRecord();
        public virtual void Prior() => dataObject.PriorRecord();
        public virtual void Add(string name) => dataObject.AddRecord(name);
        public virtual void Delete(string name) => dataObject.DeleteRecord(name);
        public virtual void Show() => dataObject.ShowRecord();
        public virtual void ShowAll() => dataObject.ShowAllRecords();
    }

    /// <summary>
    /// The 'RefinedAbstraction' class
    /// </summary>
    public class Customers(IDataObject<string> dataObject) : CustomersBase(dataObject)
    {
        public override void ShowAll()
        {
            // Add separator lines
            WriteLine();
            WriteLine("------------------------");
            base.ShowAll();
            WriteLine("------------------------");
        }
    }

    /// <summary>
    /// The 'Implementor' interface
    /// </summary>
    public interface IDataObject<T>
    {
        void NextRecord();
        void PriorRecord();
        void AddRecord(T t);
        void DeleteRecord(T t);
        T GetCurrentRecord();
        void ShowRecord();
        void ShowAllRecords();
    }

    /// <summary>
    /// The 'ConcreteImplementor' class
    /// </summary>
    public class CustomersData : IDataObject<string>
    {
        private readonly string city;
        private readonly List<string> customers;
        private int current = 0;

        // Constructor
        public CustomersData(string city)
        {
            this.city = city;

            // Simulate loading from database
            customers =
                  ["Jim Jones", "Samual Jackson", "Allan Good",
                "Ann Stills", "Lisa Giolani" ];
        }

        public void NextRecord()
        {
            if (current <= customers.Count - 1)
            {
                current++;
            }
        }

        public void PriorRecord()
        {
            if (current > 0)
            {
                current--;
            }
        }

        public void AddRecord(string customer) =>
            customers.Add(customer);

        public void DeleteRecord(string customer) =>
            customers.Remove(customer);

        public string GetCurrentRecord() =>
            customers[current];

        public void ShowRecord() =>
            WriteLine(customers[current]);

        public void ShowAllRecords()
        {
            WriteLine("Customer Group: " + city);
            customers.ForEach(customer =>
                WriteLine(" " + customer));
        }
    }
}