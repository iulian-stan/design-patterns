using System.Collections.Generic;
using static System.Console;

namespace Flyweight
{
    /// <summary>
    /// Flyweight Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Build document with text
            var document = "AAZZBBZB";

            var factory = new CharacterFactory();

            // extrinsic state
            int pointSize = 10;

            // For each character use a flyweight object
            foreach (char c in document)
            {
                var character = factory.GetCharacter(c);
                character.Display(++pointSize);
            }

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'FlyweightFactory' class
    /// </summary>
    public class CharacterFactory
    {
        private readonly Dictionary<char, Character> characters = [];

        public Character GetCharacter(char key)
        {
            // Uses "lazy initialization"
            Character character;
            if (characters.TryGetValue(key, out Character? value))
            {
                character = value;
            }
            else
            {
                character = key switch
                {
                    'A' => new CharacterA(),
                    'B' => new CharacterB(),
                    // ...
                    'Z' => new CharacterZ(),
                    _ => null!
                };
                characters.Add(key, character);
            }
            return character;
        }
    }

    /// <summary>
    /// The 'Flyweight' class
    /// </summary>
    public class Character
    {
        protected char symbol;
        protected int width;
        protected int height;
        protected int ascent;
        protected int descent;

        public void Display(int pointSize) =>
           WriteLine($"{symbol} (pointsize {pointSize})");
    }

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    public class CharacterA : Character
    {
        // Constructor
        public CharacterA()
        {
            symbol = 'A';
            height = 100;
            width = 120;
            ascent = 70;
            descent = 0;
        }
    }

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    public class CharacterB : Character
    {
        // Constructor
        public CharacterB()
        {
            symbol = 'B';
            height = 100;
            width = 140;
            ascent = 72;
            descent = 0;
        }
    }

    // ... C, D, E, etc.

    /// <summary>
    /// A 'ConcreteFlyweight' class
    /// </summary>
    public class CharacterZ : Character
    {
        // Constructor
        public CharacterZ()
        {
            symbol = 'Z';
            height = 100;
            width = 100;
            ascent = 68;
            descent = 0;
        }
    }
}