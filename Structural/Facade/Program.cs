using static System.Console;

namespace Facade
{
    /// <summary>
    /// Facade Design Pattern
    /// </summary>
    public class Program
    {
        static void Main()
        {
            // Facade
            var mortgage = new Mortgage();

            // Evaluate mortgage eligibility for customer
            var customer = new Customer("Ann McKinsey");
            bool eligible = mortgage.IsEligible(customer, 125000);

            string result = eligible ? "Approved" : "Rejected";
            WriteLine($"\n{customer.Name} has been {result}");


            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Subsystem ClassA' class
    /// </summary>
    public class Bank
    {
        public bool HasSufficientSavings(Customer c, int amount)
        {
            WriteLine($"Check bank for {c.Name}");
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassB' class
    /// </summary>
    public class Credit
    {
        public bool HasGoodCredit(Customer c)
        {
            WriteLine($"Check credit for {c.Name}");
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassC' class
    /// </summary>
    public class Loan
    {
        public bool HasNoBadLoans(Customer c)
        {
            WriteLine($"Check loans for {c.Name}");
            return true;
        }
    }

    /// <summary>
    /// The 'Facade' class
    /// </summary>
    public class Mortgage
    {
        private readonly Bank bank = new();
        private readonly Loan loan = new();
        private readonly Credit credit = new();

        public bool IsEligible(Customer cust, int amount)
        {
            WriteLine("{0} applies for {1:C} loan\n",
                cust.Name, amount);

            bool eligible = true;

            // Check creditworthyness of applicant
            if (!bank.HasSufficientSavings(cust, amount))
            {
                eligible = false;
            }
            else if (!loan.HasNoBadLoans(cust))
            {
                eligible = false;
            }
            else if (!credit.HasGoodCredit(cust))
            {
                eligible = false;
            }

            return eligible;
        }
    }

    /// <summary>
    /// Customer class
    /// </summary>
    public record Customer(string Name);
}
