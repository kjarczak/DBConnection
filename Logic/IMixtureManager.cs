using System.Collections.Generic;
using DataModel;

namespace Logic
{
    public interface IMixtureManager
    {
        public List<Mixture> GetAllMixtures();
        public List<Component> GetAllComponents();
        public List<Mixture> GetMinimalMixtures();
        public List<Component> GetMinimalComponents();
        public void SaveMixture(Mixture mixture);
    }
}
