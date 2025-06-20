﻿//Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. Chain the receiving objects and pass the request along the chain until an object handles it.

//The classes and objects participating in this pattern are:
//    Handler   (Approver)
//        defines an interface for handling the requests
//        (optional) implements the successor link
//    ConcreteHandler   (Director, VicePresident, President)
//        handles requests it is responsible for
//        can access its successor
//        if the ConcreteHandler can handle the request, it does so; otherwise it forwards the request to its successor
//    Client   (ChainApp)
//        initiates the request to a ConcreteHandler object on the chain

using System;
namespace ChainOfResponsibility
{
    /// <summary>
    /// MainApp startup class for Real-World
    /// Chain of Responsibility Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Setup Chain of Responsibility
            Approver larry = new Director();
            Approver sam = new VicePresident();
            Approver tammy = new President();
            larry.SetSuccessor(sam);
            sam.SetSuccessor(tammy);
            // Generate and process purchase requests
            Purchase p = new Purchase(2034, 350.00, "Assets");
            larry.ProcessRequest(p);
            p = new Purchase(2035, 32590.10, "Project X");
            larry.ProcessRequest(p);
            p = new Purchase(2036, 122100.00, "Project Y");
            larry.ProcessRequest(p);
            // Wait for user
            Console.ReadKey();
        }
    }
    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    abstract class Approver
    {
        protected Approver successor;
        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }
        public abstract void ProcessRequest(Purchase purchase);
    }
    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }
    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }
    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine(
                  "Request# {0} requires an executive meeting!",
                  purchase.Number);
            }
        }
    }
    /// <summary>
    /// Class holding request details
    /// </summary>
    class Purchase
    {
        private int _number;
        private double _amount;
        private string _purpose;
        // Constructor
        public Purchase(int number, double amount, string purpose)
        {
            this._number = number;
            this._amount = amount;
            this._purpose = purpose;
        }
        // Gets or sets purchase number
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        // Gets or sets purchase amount
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        // Gets or sets purchase purpose
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
    }
}