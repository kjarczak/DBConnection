using System;
using System.Linq;
using Logic;

namespace Client
{
    public class Application : IApplication
    {
        private readonly IMixtureManager _mixtureManager;
        private readonly IDialogue _dialogue;

        public Application(IMixtureManager mixtureManager, IDialogue dialogue)
        {
            _mixtureManager = mixtureManager;
            _dialogue = dialogue;
        }
        
        public void Run()
        {
            _dialogue.PrintGreetings();

            while (true)
            {
                _dialogue.PrintPrompt();
                var tokens = Console.ReadLine()?.Split().ToList();
                switch (tokens?[0])
                {
                    case "create":
                        var mixture = _dialogue.RetrieveMixture();
                        if (mixture != null)
                        {
                            _mixtureManager.SaveMixture(mixture);
                        }
                        break;


                    case "components":
                        if (tokens.Count < 2)
                        {
                            var components = _mixtureManager.GetMinimalComponents();
                            _dialogue.PrintComponents(components);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var components = _mixtureManager.GetAllComponents();
                                _dialogue.PrintComponents(components);
                            }
                            else
                            {
                                _dialogue.PrintInvalidCommandError();
                            }
                        }

                        break;

                    case "mixtures":
                        if (tokens.Count < 2)
                        {
                            var mixtures = _mixtureManager.GetMinimalMixtures();
                            _dialogue.PrintMixtures(mixtures);
                        }
                        else
                        {
                            if ("-a".Equals(tokens[1]))
                            {
                                var mixtures = _mixtureManager.GetAllMixtures();
                                _dialogue.PrintMixtures(mixtures);
                            }
                            else
                            {
                                _dialogue.PrintInvalidCommandError();
                            }
                        }
                        
                        break;
                    case "clear":
                        Console.Clear();
                        break;

                    case "help":
                        _dialogue.PrintHelp();
                        break;

                    case "quit":
                        return;

                    case "":
                        continue;

                    default:
                        _dialogue.PrintInvalidCommandError();
                        break;
                }
            }
        }
    }
}
