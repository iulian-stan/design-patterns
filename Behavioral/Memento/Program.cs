using static System.Console;
using System.Text.Json;

namespace Memento
{
    /// <summary>
    /// Memento Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Init sales prospect through object initialization
            var s = new SalesProspect
            {
                Name = "Joel van Halen",
                Phone = "(412) 256-0990",
                Budget = 25000.0
            };

            // Store internal state
            var m = new ProspectMemory(s.SaveMemento());

            // Change originator
            s.Name = "Leo Welch";
            s.Phone = "(310) 209-7111";
            s.Budget = 1000000.0;

            // Restore saved state
            s.RestoreMemento(m.Memento);

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    public class SalesProspect
    {
        private string name = null!;
        private string phone = null!;
        private double budget;

        // Gets or sets name
        public string Name
        {
            get => name;
            set
            {
                name = value;
                WriteLine("Name:   " + name);
            }
        }

        // Gets or sets phone
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                WriteLine("Phone:  " + phone);
            }
        }

        // Gets or sets budget
        public double Budget
        {
            get => budget;
            set
            {
                budget = value;
                WriteLine("Budget: " + budget);
            }
        }

        // Stores (serializes) memento
        public Memento SaveMemento()
        {
            WriteLine("\nSaving state --\n");

            var memento = new Memento();
            return memento.Serialize(this);
        }

        // Restores (deserializes) memento
        public void RestoreMemento(Memento memento)
        {
            WriteLine("\nRestoring state --\n");

            var s = (SalesProspect)memento.Deserialize();
            Name = s.Name;
            Phone = s.Phone;
            Budget = s.Budget;
        }
    }

    /// <summary>
    /// The 'Memento' class
    /// </summary>
    public class Memento
    {
        private string store = null!;

        public Memento Serialize(object o)
        {
            store = JsonSerializer.Serialize(o);
            return this;
        }

        public object Deserialize()
        {
            return JsonSerializer.Deserialize<SalesProspect>(store)!;
        }
    }

    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    public record ProspectMemory(Memento Memento);
}