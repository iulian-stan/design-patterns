using static System.Console;

namespace Adapter
{
    /// <summary>
    /// Adapter Design Pattern
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            // Non-adapted chemical compound 
            var unknown = new Compound();
            unknown.Display();

            // Adapted chemical compounds
            var water = new RichCompound(Chemical.Water);
            water.Display();

            var benzene = new RichCompound(Chemical.Benzene);
            benzene.Display();

            var ethanol = new RichCompound(Chemical.Ethanol);
            ethanol.Display();

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Target' class
    /// </summary>
    public class Compound
    {
        public Chemical Chemical { get; protected set; }
        public float BoilingPoint { get; protected set; }
        public float MeltingPoint { get; protected set; }
        public double MolecularWeight { get; protected set; }
        public string? MolecularFormula { get; protected set; }

        public virtual void Display()
        {
            WriteLine("\nCompound: Unknown ------ ");
        }
    }

    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    public class RichCompound : Compound
    {
        private readonly ChemicalDatabank bank = new();

        // Constructor
        public RichCompound(Chemical chemical)
        {
            Chemical = chemical;
        }

        public override void Display()
        {
            // Adaptee request methods
            BoilingPoint = bank.GetCriticalPoint(Chemical, State.Boiling);
            MeltingPoint = bank.GetCriticalPoint(Chemical, State.Melting);
            MolecularWeight = bank.GetMolecularWeight(Chemical);
            MolecularFormula = bank.GetMolecularStructure(Chemical);

            WriteLine($"\nCompound: {Chemical} ------ ");
            WriteLine($" Formula: {MolecularFormula}");
            WriteLine($" Weight : {MolecularWeight}");
            WriteLine($" Melting Pt: {MeltingPoint}");
            WriteLine($" Boiling Pt: {BoilingPoint}");
        }
    }

    /// <summary>
    /// The 'Adaptee' class
    /// </summary>
    public class ChemicalDatabank
    {
        // The databank 'legacy API'
        public float GetCriticalPoint(Chemical compound, State point)
        {
            // Melting Point
            if (point == State.Melting)
            {
                return compound switch
                {
                    Chemical.Water => 0.0f,
                    Chemical.Benzene => 5.5f,
                    Chemical.Ethanol => -114.1f,
                    _ => 0f,
                };
            }
            // Boiling Point
            else
            {
                return compound switch
                {
                    Chemical.Water => 100.0f,
                    Chemical.Benzene => 80.1f,
                    Chemical.Ethanol => 78.3f,
                    _ => 0f,
                };
            }
        }

        public string GetMolecularStructure(Chemical compound)
        {
            return compound switch
            {
                Chemical.Water => "H20",
                Chemical.Benzene => "C6H6",
                Chemical.Ethanol => "C2H5OH",
                _ => "",
            };
        }

        public double GetMolecularWeight(Chemical compound)
        {
            return compound switch
            {
                Chemical.Water => 18.015d,
                Chemical.Benzene => 78.1134d,
                Chemical.Ethanol => 46.0688d,
                _ => 0d
            };
        }
    }


    /// <summary>
    /// Chemical enumeration
    /// </summary>
    public enum Chemical
    {
        Water,
        Benzene,
        Ethanol
    }

    /// <summary>
    /// State enumeration
    /// </summary>
    public enum State
    {
        Boiling,
        Melting
    }
}