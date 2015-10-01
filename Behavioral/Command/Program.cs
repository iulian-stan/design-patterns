using static System.Console;
using System;
using System.Collections.Generic;

namespace Command
{
    /// <summary>
    /// Command Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create user and let her compute
            var user = new User();

            // Issue several compute commands
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            // Undo 4 commands
            user.Undo(4);

            // Redo 3 commands
            user.Redo(3);

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Command' interface
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class CalculatorCommand(Calculator calculator,
                                    char @operator,
                                    int operand) : ICommand
    {

        // Sets operator
        public char Operator { set => @operator = value; }

        // Sets operand
        public int Operand { set => operand = value; }

        // Execute command
        public void Execute()
        {
            calculator.Operation(@operator, operand);
        }

        // Unexecute command
        public void UnExecute()
        {
            calculator.Operation(Undo(@operator), operand);
        }

        // Return opposite operator for given operator
        private static char Undo(char @operator)
        {
            return @operator switch
            {
                '+' => '-',
                '-' => '+',
                '*' => '/',
                '/' => '*',
                _ => throw new ArgumentException("@operator"),
            };
        }
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Calculator
    {
        private int current = 0;

        // Perform operation for given operator and operand
        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': current += operand; break;
                case '-': current -= operand; break;
                case '*': current *= operand; break;
                case '/': current /= operand; break;
            }
            WriteLine(
                "Current value = {0,3} (following {1} {2})",
                current, @operator, operand);
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class User
    {
        private readonly Calculator calculator = new();
        private readonly List<ICommand> commands = [];
        private int current = 0;

        // Redo original commands
        public void Redo(int levels)
        {
            WriteLine($"\n---- Redo {levels} levels ");

            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
                if (current < commands.Count - 1)
                {
                    commands[current++].Execute();
                }
            }
        }

        // Undo prior commands
        public void Undo(int levels)
        {
            WriteLine($"\n---- Undo {levels} levels ");

            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
                if (current > 0)
                {
                    commands[--current].UnExecute();
                }
            }
        }

        // Compute new value given operator and operand
        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            var command = new CalculatorCommand(calculator, @operator, operand);
            command.Execute();

            // Add command to undo list
            commands.Add(command);
            current++;
        }
    }
}