using System.Collections.Generic;
using DataModel;

namespace Database
{
    public interface IDataProxy
    {
        public List<Mixture> GetMixtures();

        public List<Component> GetComponents();

        public void CreateMixture(Mixture mixture);
    }
}
