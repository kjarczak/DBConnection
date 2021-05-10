using System;
using System.Collections.Generic;
using System.Linq;
using DataModel;
using Logic;

namespace Client
{
    public static class UserInterface
    {
        public static void Run()
        {
            var manager = new MixtureManager();
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
                            manager.SaveMixture(mixture);
                        }
                        break;


                    case "components":
                        if (tokens.Count < 2)
                        {
                            var components = manager.GetMinimalComponents();
                            PrintComponents(components);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var components = manager.GetAllComponents();
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
                            var mixtures = manager.GetMinimalMixtures();
                            PrintMixtures(mixtures);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var mixtures = manager.GetAllMixtures();
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


        private static Mixture CreateMixture()
        {
            Console.WriteLine("Mixture creation");
            Console.Write("Name: ");
            var name = Console.ReadLine();
            var components = new List<Component>();

            string key;
            Console.WriteLine("Components:");
            do
            {
                var component = new Component();

                Console.Write("\tItem`s name: ");
                var itemName = Console.ReadLine();
                component.Item = new Item() { Name = itemName };

                int quantity;
                bool isParsed;
                do
                {
                    Console.Write("\tItem`s quantity: ");
                    var itemQuantityString = Console.ReadLine();
                    isParsed = int.TryParse(itemQuantityString, out quantity);
                    if (!isParsed)
                    {
                        Console.WriteLine("\tNumber invalid");
                    }
                } while (!isParsed);
                component.Quantity = quantity;

                Console.Write("\tIs component optional? [y/n]: ");
                var isOptional = Console.ReadLine();
                if ("y".Equals(isOptional))
                {
                    component.ComponentType = ComponentType.Optional;
                }


                components.Add(component);
                Console.Write("Add another component? [y/n]: ");
                key = Console.ReadLine();
            } while ("y".Equals(key));

            var newMixture = new Mixture()
            {
                Name = name,
                Components = components
            };

            Console.WriteLine("\nCreated mixture:");
            PrintMixture(newMixture);
            Console.WriteLine("Do you want to save mixture? [y/n]");
            var toSave = Console.ReadLine();
            return "y".Equals(toSave) ? newMixture : null;
        }


        private static void PrintMixtures(List<Mixture> mixtures)
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

        private static void PrintMixture(Mixture mixture)
        {
            Console.WriteLine(mixture.Name);
            foreach (var component in mixture.Components)
            {
                var line = $"\t{component.Item.Name}: {component.Quantity}";
                if (ComponentType.Optional.Equals(component.ComponentType))
                {
                    line += "\t(optional)";
                }
                Console.WriteLine("\t" + line);
            }
        }

        private static void PrintComponents(List<Component> components)
        {
            if (components == null || components.Count == 0)
            {
                Console.WriteLine("No components in memory.");
                return;
            }
            
            foreach (var component in components)
            {
                var line = $"{component.Item.Name}: {component.Quantity}";
                if (ComponentType.Optional.Equals(component.ComponentType))
                {
                    line += " (optional)";
                }
                Console.WriteLine(line);
            };
        }

        private static void PrintHelp()
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
