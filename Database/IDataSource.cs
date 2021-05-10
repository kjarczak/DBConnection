using System.Collections.Generic;
using Database.DAOModels;

namespace Database
{
    public interface IDataSource
    {
        public List<MixtureDAO> GetMixtures();
        public List<ComponentDAO> GetComponents();
        void CreateMixture(MixtureDAO mixtureDAO);
    }
}
