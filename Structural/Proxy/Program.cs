//Provide a surrogate or placeholder for another object to control access to it. 

//The classes and objects participating in this pattern are:
//    Proxy   (MathProxy)
//        maintains a reference that lets the proxy access the real subject. Proxy may refer to a Subject if the RealSubject and Subject interfaces are the same.
//        provides an interface identical to Subject's so that a proxy can be substituted for for the real subject.
//        controls access to the real subject and may be responsible for creating and deleting it.
//        other responsibilites depend on the kind of proxy:
//            remote proxies are responsible for encoding a request and its arguments and for sending the encoded request to the real subject in a different address space.
//            virtual proxies may cache additional information about the real subject so that they can postpone accessing it. For example, the ImageProxy from the Motivation caches the real images's extent.
//            protection proxies check that the caller has the access permissions required to perform a request. 
//    Subject   (IMath)
//        defines the common interface for RealSubject and Proxy so that a Proxy can be used anywhere a RealSubject is expected. 
//    RealSubject   (Math)
//        defines the real object that the proxy represents. 

using System;
namespace Proxy
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Proxy Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create math proxy
            MathProxy proxy = new MathProxy();
            // Do the math
            Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
            Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
            Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
            Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));
            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Subject interface
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
    class Math : IMath
    {
        public double Add(double x, double y) { return x + y; }
        public double Sub(double x, double y) { return x - y; }
        public double Mul(double x, double y) { return x * y; }
        public double Div(double x, double y) { return x / y; }
    }
    /// <summary>
    /// The 'Proxy Object' class
    /// </summary>
    class MathProxy : IMath
    {
        private Math _math = new Math();
        public double Add(double x, double y)
        {
            return _math.Add(x, y);
        }
        public double Sub(double x, double y)
        {
            return _math.Sub(x, y);
        }
        public double Mul(double x, double y)
        {
            return _math.Mul(x, y);
        }
        public double Div(double x, double y)
        {
            return _math.Div(x, y);
        }
    }
}