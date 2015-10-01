//Without violating encapsulation, capture and externalize an object's internal state so that the object can be restored to this state later.

//The classes and objects participating in this pattern are:
//    Memento  (Memento)
//        stores internal state of the Originator object. The memento may store as much or as little of the originator's internal state as necessary at its originator's discretion.
//        protect against access by objects of other than the originator. Mementos have effectively two interfaces. Caretaker sees a narrow interface to the Memento -- it can only pass the memento to the other objects. Originator, in contrast, sees a wide interface, one that lets it access all the data necessary to restore itself to its previous state. Ideally, only the originator that produces the memento would be permitted to access the memento's internal state.
//    Originator  (SalesProspect)
//        creates a memento containing a snapshot of its current internal state.
//        uses the memento to restore its internal state
//    Caretaker  (Caretaker)
//        is responsible for the memento's safekeeping
//        never operates on or examines the contents of a memento.

using System;
namespace Memento
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Memento Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            SalesProspect s = new SalesProspect();
            s.Name = "Noel van Halen";
            s.Phone = "(412) 256-0990";
            s.Budget = 25000.0;
            // Store internal state
            ProspectMemory m = new ProspectMemory();
            m.Memento = s.SaveMemento();
            // Continue changing originator
            s.Name = "Leo Welch";
            s.Phone = "(310) 209-7111";
            s.Budget = 1000000.0;
            // Restore saved state
            s.RestoreMemento(m.Memento);
            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Originator' class
    /// </summary>
    class SalesProspect
    {
        private string _name;
        private string _phone;
        private double _budget;
        // Gets or sets name
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Console.WriteLine("Name:  " + _name);
            }
        }
        // Gets or sets phone
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                Console.WriteLine("Phone: " + _phone);
            }
        }
        // Gets or sets budget
        public double Budget
        {
            get { return _budget; }
            set
            {
                _budget = value;
                Console.WriteLine("Budget: " + _budget);
            }
        }
        // Stores memento
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(_name, _phone, _budget);
        }
        // Restores memento
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Name = memento.Name;
            this.Phone = memento.Phone;
            this.Budget = memento.Budget;
        }
    }
    /// <summary>
    /// The 'Memento' class
    /// </summary>
    class Memento
    {
        private string _name;
        private string _phone;
        private double _budget;
        // Constructor
        public Memento(string name, string phone, double budget)
        {
            this._name = name;
            this._phone = phone;
            this._budget = budget;
        }
        // Gets or sets name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        // Gets or set phone
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        // Gets or sets budget
        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
    }
    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    class ProspectMemory
    {
        private Memento _memento;
        // Property
        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}