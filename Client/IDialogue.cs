using System;
using System.Collections.Generic;
using System.Text;
using DataModel;

namespace Client
{
    public interface IDialogue
    {
        public void PrintGreetings();
        public void PrintMixtures(ICollection<Mixture> mixtures);
        public void PrintMixture(Mixture mixture);
        public void PrintComponents(ICollection<Component> components);
        public void PrintHelp();
        public void PrintPrompt();
        public void PrintInvalidCommandError();
        public Mixture RetrieveMixture();
    }
}
