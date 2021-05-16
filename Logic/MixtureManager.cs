using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using DataModel;

namespace Logic
{
    public class MixtureManager : IMixtureManager
    {
        private readonly IDataProxy _dataProxy;

        public MixtureManager(IDataProxy dataProxy)
        {
            _dataProxy = dataProxy;
        }

        public List<Mixture> GetAllMixtures()
        {
            return _dataProxy.GetMixtures();
        }
        public List<Component> GetAllComponents()
        {
            return _dataProxy.GetComponents();
        }
        public List<Mixture> GetMinimalMixtures()
        {
            var mixtures = _dataProxy.GetMixtures();
            foreach (var mixture in mixtures)
            {
                var trimmedComponents = mixture.Components.Where(s=> ComponentType.Mandatory.Equals(s.ComponentType)).ToList();
                mixture.Components = trimmedComponents;
            }
            return mixtures;
        }
        public List<Component> GetMinimalComponents()
        {
            var components = _dataProxy.GetComponents();
            return components.Where(s => ComponentType.Mandatory.Equals(s.ComponentType)).ToList();
        }
        public void SaveMixture(Mixture mixture)
        {
            if (mixture?.Components is null || mixture.Name is null || mixture.Name == "")
            {
                throw new ArgumentException("Invalid mixture");
            }

            if (mixture.Components.Any(component => component.Item?.Name is null || component.Item.Name == ""))
            {
                throw new ArgumentException($"Invalid component inside mixture: {mixture.Name}");
            }

            _dataProxy.CreateMixture(mixture);
        }
    }
}
