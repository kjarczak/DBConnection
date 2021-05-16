using System;
using System.Collections.Generic;
using System.Linq;
using DataModel;
using Logic;

namespace Client
{
    public class Application
    {
        private readonly IMixtureManager _mixtureManager;

        public Application(IMixtureManager mixtureManager)
        {
            _mixtureManager = mixtureManager;
        }
        
        public void Run()
        {
            Console.WriteLine("=== Welcome to the mixer app ===");
            while (true)
            {
                Console.Write(">>");
                var tokens = Console.ReadLine()?.Split().ToList();
                switch (tokens?[0])
                {
                    case "create":
                        var mixture = CreateMixture();
                        if (mixture != null)
                        {
                            _mixtureManager.SaveMixture(mixture);
                        }
                        break;


                    case "components":
                        if (tokens.Count < 2)
                        {
                            var components = _mixtureManager.GetMinimalComponents();
                            PrintComponents(components);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var components = _mixtureManager.GetAllComponents();
                                PrintComponents(components);
                            }
                            else
                            {
                                Console.WriteLine("No such command. Try 'help'.");
                            }
                        }

                        break;

                    case "mixtures":
                        if (tokens.Count < 2)
                        {
                            var mixtures = _mixtureManager.GetMinimalMixtures();
                            PrintMixtures(mixtures);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var mixtures = _mixtureManager.GetAllMixtures();
                                PrintMixtures(mixtures);
                            }
                            else
                            {
                                Console.WriteLine("No such command. Try 'help'.");
                            }
                        }
                        
                        break;
                    case "clear":
                        Console.Clear();
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "quit":
                        return;

                    case "":
                        continue;

                    default:
                        Console.WriteLine("No such command. Try 'help'.");
                        break;
                }
            }
        }

        private Mixture CreateMixture()
        {
            var mixtureBuilder = new MixtureBuilder();
            var componentBuilder = new ComponentBuilder();
            var errorFlag = false;
            
            do
            {
                Console.WriteLine("Mixture creation");
                Console.Write("Name: ");
                try
                {
                    mixtureBuilder.AddName(Console.ReadLine());
                    errorFlag = false;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid mixture's name. Try one more time.");
                    errorFlag = true;
                }
            } while (errorFlag);

            string key;
            Console.WriteLine("Components:");
            do
            {
                componentBuilder.Reset();
                do
                {
                    Console.Write("\tItem`s name: ");
                    try
                    {
                        componentBuilder.AddItem(Console.ReadLine());
                        errorFlag = false;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid item's name. Try one more time.");
                        errorFlag = true;
                    }
                } while (errorFlag);

                do
                {
                    Console.Write("\tItem`s quantity: ");
                    try
                    {
                        var input = Console.ReadLine();
                        var quantity = int.Parse(input ?? string.Empty);
                        componentBuilder.AddQuantity(quantity);
                        errorFlag = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid quantity. Try one more time.");
                        errorFlag = true;
                    }
                } while (errorFlag);


                Console.Write("\tIs component optional? [y/n]: ");
                var isOptional = Console.ReadLine();
                if ("y".Equals(isOptional))
                {
                    componentBuilder.AddType(ComponentType.Optional);
                }

                mixtureBuilder.AddComponent(componentBuilder.Build());
                Console.Write("Add another component? [y/n]: ");
                key = Console.ReadLine();
            } while ("y".Equals(key));

            var newMixture = mixtureBuilder.Build();
            Console.WriteLine("\nCreated mixture:");
            PrintMixture(newMixture);
            Console.Write("Do you want to save mixture? [y/n]: ");
            return "y".Equals(Console.ReadLine()) ? newMixture : null;
        }
        private void PrintMixtures(ICollection<Mixture> mixtures)
        {
            if (mixtures == null || mixtures.Count == 0)
            {
                Console.WriteLine("No mixtures in memory.");
                return;
            }

            foreach (var mixture in mixtures)
            {
                PrintMixture(mixture);
                Console.WriteLine();
            }
        }
        private void PrintMixture(Mixture mixture)
        {
            Console.WriteLine(mixture.Name);
            foreach (var component in mixture.Components)
            {
                var line = $"\t{component.Item.Name}:\t{component.Quantity}";
                if (ComponentType.Optional.Equals(component.ComponentType))
                {
                    line += "\t(optional)";
                }
                Console.WriteLine("\t" + line);
            }
        }
        private void PrintComponents(ICollection<Component> components)
        {
            if (components == null || components.Count == 0)
            {
                Console.WriteLine("No components in memory.");
                return;
            }
            
            foreach (var component in components)
            {
                var line = $"{component.Item.Name}:\t{component.Quantity}";
                if (ComponentType.Optional.Equals(component.ComponentType))
                {
                    line += "\t(optional)";
                }
                Console.WriteLine(line);
            };
        }
        private void PrintHelp()
        {
            Console.WriteLine("\nCOMMANDS:" +
                              "\nmixtures \tshow list of mixtures with mandatory components" +
                              "\nmixtures <-a> \tshow list of mixtures with every component"+
                              "\ncomponents \tshow list of mandatory components" +
                              "\ncomponents <-a> \tshow full list of components" +
                              "\ncreate \t\topens ,,mixture creating mode" +
                              "\nclear \t\tclear window" +
                              "\nhelp \t\tshow manual" +
                              "\nquit   \t\tquits application" +
                              "\n");
        }
    }
}
