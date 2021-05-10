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
                    Name = "Mickey Mouse",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Tomato juice",
                            Quantity = 30
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Lemon juice",
                            Quantity = 20
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Optional",
                            Item = "Tabasco sauce",
                            Quantity = 1
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Optional",
                            Item = "Ice cube",
                            Quantity = 3
                        }
                    }
                },
                new MixtureDAO()
                {
                    Name = "Apple Pie",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Apple juice",
                            Quantity = 30
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Lemon juice",
                            Quantity = 10
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Optional",
                            Item = "Grenadine syrup",
                            Quantity = 1
                        }
                    }
                },
                new MixtureDAO()
                {
                    Name = "Cranberry Cooler",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Cranberry juice",
                            Quantity = 30
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Apple juice",
                            Quantity = 10
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Sweet & Sour",
                            Quantity = 1
                        },

                        new ComponentDAO()
                        {
                            ComponentType = "Mandatory",
                            Item = "Rosemary",
                            Quantity = 1
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
