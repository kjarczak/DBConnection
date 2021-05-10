using System.Collections.Generic;
using System.Linq;
using Database.DAOModels;

namespace Database
{
    public class MockedDatabaseDataSource :IDataSource
    {
        private readonly List<MixtureDAO> _mixtures;
        public MockedDatabaseDataSource()
        {
            _mixtures = new List<MixtureDAO>()
            {
                new MixtureDAO()
                {
                    Name = "Test Mixture 1",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO()
                        {
                            ComponentType = "Optional",
                            Item = "Test Component 1",
                            Quantity = 1
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Optional",
                            Item = "Test Component 2",
                            Quantity = 2
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Test Component 3",
                            Quantity = 3
                        }
                    }
                },
                new MixtureDAO()
                {
                    Name = "Test Mixture 2",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Test Component 21",
                            Quantity = 1
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Test Component 22",
                            Quantity = 2
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Test Component 23",
                            Quantity = 3
                        }
                    }
                }

            };
        }

        public MockedDatabaseDataSource(List<MixtureDAO> mixtures)
        {
            _mixtures = mixtures;
        }

        public List<MixtureDAO> GetMixtures()
        {
            return _mixtures;
        }

        public List<ComponentDAO> GetComponents()
        {
            return _mixtures.SelectMany(s=> s.Components).ToList();
        }

        public void CreateMixture(MixtureDAO mixtureDAO)
        {
            _mixtures.Add(mixtureDAO);
        }
    }
}
