//Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically. 

//The classes and objects participating in this pattern are:
//    Subject  (Stock)
//        knows its observers. Any number of Observer objects may observe a subject
//        provides an interface for attaching and detaching Observer objects.
//    ConcreteSubject  (IBM)
//        stores state of interest to ConcreteObserver
//        sends a notification to its observers when its state changes
//    Observer  (IInvestor)
//        defines an updating interface for objects that should be notified of changes in a subject.
//    ConcreteObserver  (Investor)
//        maintains a reference to a ConcreteSubject object
//        stores state that should stay consistent with the subject's
//        implements the Observer updating interface to keep its state consistent with the subject's

using System;
using System.Collections.Generic;
namespace Observer
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Observer Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create IBM stock and attach investors
            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));
            // Fluctuating prices will notify investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;
            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();
        // Constructor
        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
        }
        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }
        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }
        public void Notify()
        {
            foreach (IInvestor investor in _investors)
            {
                investor.Update(this);
            }
            Console.WriteLine("");
        }
        // Gets or sets the price
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }
        // Gets the symbol
        public string Symbol
        {
            get { return _symbol; }
        }
    }
    /// <summary>
    /// The 'ConcreteSubject' class
    /// </summary>
    class IBM : Stock
    {
        // Constructor
        public IBM(string symbol, double price)
            : base(symbol, price)
        {
        }
    }
    /// <summary>
    /// The 'Observer' interface
    /// </summary>
    interface IInvestor
    {
        void Update(Stock stock);
    }
    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>
    class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;
        // Constructor
        public Investor(string name)
        {
            this._name = name;
        }
        public void Update(Stock stock)
        {
            Console.WriteLine("Notified {0} of {1}'s " +
              "change to {2:C}", _name, stock.Symbol, stock.Price);
        }
        // Gets or sets the stock
        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
    }
}