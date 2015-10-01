using static System.Console;
using System;

namespace Observer
{
    /// <summary>
    /// Observer Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create IBM stock and attach investors
            var ibm = new IBM(120.00);

            // Attach 'listeners', i.e. Investors
            ibm.Attach(new Investor { Name = "Sorros" });
            ibm.Attach(new Investor { Name = "Berkshire" });

            // Fluctuating prices will notify listening investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;

            // Wait for user
            ReadKey();
        }
    }

    // Custom event arguments
    public class ChangeEventArgs : EventArgs
    {
        // Gets or sets symbol
        public string Symbol { get; set; }

        // Gets or sets price
        public double Price { get; set; }
    }

    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    public abstract class Stock(string symbol, double price)
    {
        protected string symbol = symbol;
        protected double price = price;

        // Event
        public event EventHandler<ChangeEventArgs> Change = null!;

        // Invoke the Change event
        public virtual void OnChange(ChangeEventArgs e)
        {
            Change?.Invoke(this, e);
        }

        public void Attach(IInvestor investor)
        {
            Change += investor.Update;
        }

        public void Detach(IInvestor investor)
        {
            Change -= investor.Update;
        }

        // Gets or sets the price
        public double Price
        {
            get => price;
            set
            {
                if (price != value)
                {
                    price = value;
                    OnChange(new ChangeEventArgs { Symbol = symbol, Price = price });
                    WriteLine("");
                }
            }
        }
    }

    /// <summary>
    /// The 'ConcreteSubject' class
    /// </summary>
    public class IBM(double price) : Stock("IBM", price)
    {
    }

    /// <summary>
    /// The 'Observer' interface
    /// </summary>
    public interface IInvestor
    {
        void Update(object sender, ChangeEventArgs e);
    }

    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>
    public class Investor : IInvestor
    {
        // Gets or sets the investor name
        public string Name { get; set; } = null!;

        // Gets or sets the stock
        public Stock Stock { get; set; } = null!;

        public void Update(object sender, ChangeEventArgs e)
        {
            WriteLine("Notified {0} of {1}'s " +
                "change to {2:C}", Name, e.Symbol, e.Price);
        }
    }
}