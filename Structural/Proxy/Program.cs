using static System.Console;
using System.Threading;

namespace Proxy
{
    /// <summary>
    /// Proxy Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create math proxy
            var proxy = new MathProxy();

            // Do the math
            WriteLine($"4 + 2 = {proxy.Add(4, 2)}");
            WriteLine($"4 - 2 = {proxy.Sub(4, 2)}");
            WriteLine($"4 * 2 = {proxy.Mul(4, 2)}");
            WriteLine($"4 / 2 = {proxy.Div(4, 2)}");

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Subject' interface
    /// </summary>
    public interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
        double Mul(double x, double y);
        double Div(double x, double y);
    }

    /// <summary>
    /// The 'RealSubject' class
    /// </summary>
    public class Math : IMath
    {
        public double Add(double x, double y) => x + y;
        public double Sub(double x, double y) => x - y;
        public double Mul(double x, double y) => x * y;
        public double Div(double x, double y) => x / y;
    }

    /// <summary>
    /// The remote 'Proxy Object' class
    /// </summary>
    public class MathProxy : IMath
    {
        private readonly Math math = new();

        public double Add(double x, double y)
        {
            Thread.Sleep(800);
            return math.Add(x, y);
        }

        public double Sub(double x, double y)
        {
            Thread.Sleep(800);
            return math.Sub(x, y);
        }

        public double Mul(double x, double y)
        {
            Thread.Sleep(800);
            return math.Mul(x, y);
        }

        public double Div(double x, double y)
        {
            Thread.Sleep(800);
            return math.Div(x, y);
        }
    }
}