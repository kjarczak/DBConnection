using System.Collections.Generic;
using System.Linq;
using Database;
using Database.DAOModels;
using DataModel;
using Logic;
using NUnit.Framework;

namespace LogicTests
{
    public class Tests
    {
        private MixtureManager _normalManager;
        private MixtureManager _emptyManager;

        [SetUp]
        public void Setup()
        {
            IDataSource dataSource;
            DataProxy dataProxy;

            List<MixtureDAO> normalMixtures = new List<MixtureDAO>()
            {
                new MixtureDAO{
                    Name = "Mixture 1",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 1",
                            Quantity = 1
                        },

                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 2",
                            Quantity = 2
                        },

                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 3",
                            Quantity = 3
                        }
                    }
                },
                new MixtureDAO{
                    Name = "Mixture 2",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 4",
                            Quantity = 4
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 5",
                            Quantity = 5
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 6",
                            Quantity = 6
                        }
                    }
                },
                new MixtureDAO{
                    Name = "Mixture 3",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 7",
                            Quantity = 7
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 8",
                            Quantity = 8
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 9",
                            Quantity = 9
                        }
                    }
                },
                new MixtureDAO{
                    Name = "Mixture 4",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 10",
                            Quantity = 10
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 11",
                            Quantity = 11
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 12",
                            Quantity = 12
                        }
                    }
                },
                new MixtureDAO{
                    Name = "Mixture 5",
                    Components = new List<ComponentDAO>()
                    {
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Mandatory.ToString(),
                            Item = "Item 13",
                            Quantity = 13
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 14",
                            Quantity = 14
                        },
                        new ComponentDAO
                        {
                            ComponentType = ComponentType.Optional.ToString(),
                            Item = "Item 15",
                            Quantity = 15
                        }
                    }
                }
            };
            dataSource = new MockedDatabaseDataSource(normalMixtures);
            dataProxy = new DataProxy(dataSource);
            _normalManager = new MixtureManager(dataProxy);

            List<MixtureDAO> emptyMixtures = new List<MixtureDAO>();
            dataSource = new MockedDatabaseDataSource(emptyMixtures);
            dataProxy = new DataProxy(dataSource);
            _emptyManager = new MixtureManager(dataProxy);
        }

        [Test]
        public void GetAllComponentsNormalTest()
        {
            var actual = _normalManager.GetAllComponents().Count;
            Assert.AreEqual(15, actual);
        }

        [Test]
        public void GetAllComponentsEmptyTest()
        {
            var actual = _emptyManager.GetAllComponents().Count;
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void GetAllMixturesNormalTest()
        {
            var actual= _normalManager.GetAllMixtures().Count;
            Assert.AreEqual(5, actual);
        }

        [Test]
        public void GetAllMixturesEmptyTest()
        {
            var actual = _emptyManager.GetAllMixtures().Count;
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void GetMinimalComponentsNormalTest()
        {
            var actual = _normalManager.GetMinimalComponents().Count;
            Assert.AreEqual(8, actual);
        }

        [Test]
        public void GetMinimalComponentsEmptyTest()
        {
            var actual = _emptyManager.GetMinimalComponents().Count;
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void GetMinimalMixturesNormalTest()
        {
            var actual = _normalManager.GetMinimalMixtures().SelectMany(s=>s.Components).Count();
            Assert.AreEqual(8, actual);
        }

        [Test]
        public void GetMinimalMixturesEmptyTest()
        {
            var actual = _emptyManager.GetMinimalMixtures().SelectMany(s => s.Components).Count();
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void SaveMixtureTest()
        {
            _emptyManager.SaveMixture(new Mixture());
            var actual = _emptyManager.GetAllMixtures().Count;
            Assert.AreEqual(1, actual);
        }
    }
}