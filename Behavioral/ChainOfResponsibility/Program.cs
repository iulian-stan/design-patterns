using static System.Console;
using System;

namespace ChainOfResponsibility
{
    /// <summary>
    /// Chain of Responsibility Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Setup Chain of Responsibility
            var larry = new Director();
            var sam = new VicePresident();
            var tammy = new President();

            larry.Successor = sam;
            sam.Successor = tammy;

            // Generate and process purchase requests
            var purchase = new Purchase { Number = 2034, Amount = 350.00, Purpose = "Supplies" };
            larry.ProcessRequest(purchase);

            purchase = new Purchase { Number = 2035, Amount = 32590.10, Purpose = "Project X" };
            larry.ProcessRequest(purchase);

            purchase = new Purchase { Number = 2036, Amount = 122100.00, Purpose = "Project Y" };
            larry.ProcessRequest(purchase);

            // Wait for user
            ReadKey();
        }
    }
    // Purchase event argument holds purchase info
    public class PurchaseEventArgs : EventArgs
    {
        internal Purchase Purchase { get; init; } = null!;
    }

    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    public abstract class Approver
    {
        // Purchase event 
        public EventHandler<PurchaseEventArgs> Purchase;

        // Purchase event handler
        public abstract void PurchaseHandler(object sender, PurchaseEventArgs e);

        // Constructor
        public Approver()
        {
            Purchase += PurchaseHandler!;
        }

        public void ProcessRequest(Purchase purchase)
        {
            OnPurchase(new PurchaseEventArgs { Purchase = purchase });
        }

        // Invoke the Purchase event
        public virtual void OnPurchase(PurchaseEventArgs e)
        {
            Purchase?.Invoke(this, e);
        }

        // Sets or gets the next approver
        public Approver Successor { get; set; } = null!;
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    public class Director : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs e)
        {
            if (e.Purchase.Amount < 10000.0)
            {
                WriteLine("{0} approved request# {1}",
                    this.GetType().Name, e.Purchase.Number);
            }
            else
            {
                Successor?.PurchaseHandler(this, e);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    public class VicePresident : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs e)
        {
            if (e.Purchase.Amount < 25000.0)
            {
                WriteLine("{0} approved request# {1}",
                    this.GetType().Name, e.Purchase.Number);
            }
            else
            {
                Successor?.PurchaseHandler(this, e);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' clas
    /// </summary>
    public class President : Approver
    {
        public override void PurchaseHandler(object sender, PurchaseEventArgs e)
        {
            if (e.Purchase.Amount < 100000.0)
            {
                WriteLine("{0} approved request# {1}",
                    sender.GetType().Name, e.Purchase.Number);
            }
            else if (Successor != null)
            {
                Successor.PurchaseHandler(this, e);
            }
            else
            {
                WriteLine(
                    "Request# {0} requires an executive meeting!",
                    e.Purchase.Number);
            }
        }
    }

    /// <summary>
    /// Record with request details
    /// </summary>
    public record Purchase
    {
        public required double Amount { get; init; }
        public required string Purpose { get; init; }
        public required int Number { get; init; }
    }
}