using static System.Console;

namespace AbstractFactory
{
    /// <summary>
    /// Abstract Factory Design Pattern.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Create and run the African animal world
            var africa = new AnimalWorld<Africa>();
            africa.RunFoodChain();

            // Create and run the American animal world
            var america = new AnimalWorld<America>();
            america.RunFoodChain();

            // Wait for user input
            ReadKey();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' interface. 
    /// </summary>
    public interface IContinentFactory
    {
        IHerbivore CreateHerbivore();
        ICarnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class.
    /// </summary>
    public class Africa : IContinentFactory
    {
        public IHerbivore CreateHerbivore() => new Wildebeest();

        public ICarnivore CreateCarnivore() => new Lion();
    }

    /// <summary>
    /// The 'ConcreteFactory2' class.
    /// </summary>
    public class America : IContinentFactory
    {
        public IHerbivore CreateHerbivore() => new Bison();

        public ICarnivore CreateCarnivore() => new Wolf();
    }

    /// <summary>
    /// The 'AbstractProductA' interface
    /// </summary>
    public interface IHerbivore
    {
    }

    /// <summary>
    /// The 'AbstractProductB' interface
    /// </summary>
    public interface ICarnivore
    {
        void Eat(IHerbivore h);
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    public class Wildebeest : IHerbivore
    {
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    public class Lion : ICarnivore
    {
        // Eat Wildebeest
        public void Eat(IHerbivore h) =>
            WriteLine($"{GetType().Name} eats {h.GetType().Name}");
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    public class Bison : IHerbivore
    {
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    public class Wolf : ICarnivore
    {
        // Eat Bison
        public void Eat(IHerbivore h) =>
            WriteLine($"{GetType().Name} eats {h.GetType().Name}");
    }

    /// <summary>
    /// The 'Client' interface
    /// </summary>
    public interface IAnimalWorld
    {
        void RunFoodChain();
    }

    /// <summary>
    /// The 'Client' class
    /// </summary>
    public class AnimalWorld<T> : IAnimalWorld where T : IContinentFactory, new()
    {
        private readonly IHerbivore herbivore;
        private readonly ICarnivore carnivore;

        public AnimalWorld()
        {
            // Create new continent factory
            var factory = new T();

            // Factory creates carnivores and herbivores
            carnivore = factory.CreateCarnivore();
            herbivore = factory.CreateHerbivore();
        }

        /// <summary>
        /// Runs the foodchain: carnivores are eating herbivores.
        /// </summary>
        public void RunFoodChain()
        {
            carnivore.Eat(herbivore);
        }
    }
}