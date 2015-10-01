using static System.Console;

namespace State
{
    /// <summary>
    /// State Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Open a new account
            var account = new Account("Jim Johnson");

            // Apply financial transactions
            account.Deposit(500.0);
            account.Deposit(300.0);
            account.Deposit(550.0);
            account.PayInterest();
            account.Withdraw(2000.00);
            account.Withdraw(1100.00);

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'State' abstract class
    /// </summary>
    public abstract class State
    {
        protected double interest;
        protected double lowerLimit;
        protected double upperLimit;

        // Gets or sets the account
        public Account Account { get; set; } = null!;

        // Gets or sets the balance
        public double Balance { get; set; }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void PayInterest();
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Red state indicates account is overdrawn 
    /// </remarks>
    /// </summary>
    public class RedState : State
    {
        private double serviceFee;

        // Constructor
        public RedState(State state)
        {
            Balance = state.Balance;
            Account = state.Account;
            Initialize();
        }

        private void Initialize()
        {
            // Should come from a datasource
            interest = 0.0;
            lowerLimit = -100.0;
            upperLimit = 0.0;
            serviceFee = 15.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            amount -= serviceFee;
            WriteLine("No funds available for withdrawal!");
        }

        public override void PayInterest()
        {
            // No interest is paid
        }

        private void StateChangeCheck()
        {
            if (Balance > upperLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Silver state is non-interest bearing state
    /// </remarks>
    /// </summary>
    public class SilverState : State
    {
        // Overloaded constructors

        public SilverState(State state) :
            this(state.Balance, state.Account)
        {
        }

        public SilverState(double balance, Account account)
        {
            Balance = balance;
            Account = account;
            Initialize();
        }

        private void Initialize()
        {
            // Should come from a datasource
            interest = 0.0;
            lowerLimit = 0.0;
            upperLimit = 1000.0;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < lowerLimit)
            {
                Account.State = new RedState(this);
            }
            else if (Balance > upperLimit)
            {
                Account.State = new GoldState(this);
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Gold incidates interest bearing state
    /// </remarks>
    /// </summary>
    public class GoldState : State
    {
        // Overloaded constructors
        public GoldState(State state)
            : this(state.Balance, state.Account)
        {
        }

        public GoldState(double balance, Account account)
        {
            Balance = balance;
            Account = account;
            Initialize();
        }

        private void Initialize()
        {
            // Should come from a database
            interest = 0.05;
            lowerLimit = 1000.0;
            upperLimit = 10000000.0;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < 0.0)
            {
                Account.State = new RedState(this);
            }
            else if (Balance < lowerLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    public class Account
    {
        private readonly string owner;

        // Constructor
        public Account(string owner)
        {
            // New accounts are 'Silver' by default
            this.owner = owner;
            this.State = new SilverState(0.0, this);
        }

        // Gets the balance
        public double Balance
        {
            get { return State.Balance; }
        }

        // Gets or sets state
        public State State { get; set; }

        public void Deposit(double amount)
        {
            State.Deposit(amount);
            WriteLine($"Owner: {owner}");
            WriteLine("Deposited {0:C} --- ", amount);
            WriteLine(" Balance = {0:C}", this.Balance);
            WriteLine(" Status  = {0}", this.State.GetType().Name);
            WriteLine("");
        }

        public void Withdraw(double amount)
        {
            State.Withdraw(amount);
            WriteLine($"Owner: {owner}");
            WriteLine("Withdrew {0:C} --- ", amount);
            WriteLine(" Balance = {0:C}", this.Balance);
            WriteLine(" Status  = {0}\n", this.State.GetType().Name);
        }

        public void PayInterest()
        {
            State.PayInterest();
            WriteLine("Interest Paid --- ");
            WriteLine(" Balance = {0:C}", this.Balance);
            WriteLine(" Status  = {0}\n", this.State.GetType().Name);
        }
    }
}