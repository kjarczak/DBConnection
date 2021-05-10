using System.Collections.Generic;
using System.Linq;
using Database;
using DataModel;

namespace Logic
{
    public class MixtureManager
    {
        private readonly DataProxy _data;

        public MixtureManager()
        {
            _data = new DataProxy();
        }
        public MixtureManager(DataProxy dataProxy)
        {
            _data = dataProxy;
        }

        public List<Mixture> GetAllMixtures()
        {
            return _data.GetMixtures();
        }
        public List<Component> GetAllComponents()
        {
            return _data.GetComponents();
        }

        public List<Mixture> GetMinimalMixtures()
        {
            var mixtures = _data.GetMixtures();
            foreach (var mixture in mixtures)
            {
                var trimmedComponents = mixture.Components.Where(s=> ComponentType.Mandatory.Equals(s.ComponentType)).ToList();
                mixture.Components = trimmedComponents;
            }
            return mixtures;
        }

        public List<Component> GetMinimalComponents()
        {
            var components = _data.GetComponents();
            return components.Where(s => ComponentType.Mandatory.Equals(s.ComponentType)).ToList();
        }

        public void SaveMixture(Mixture mixture)
        {
            _data.CreateMixture(mixture);
        }
    }
}
