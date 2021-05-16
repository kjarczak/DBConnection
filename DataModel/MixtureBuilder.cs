using System;

namespace DataModel
{
    public class MixtureBuilder
    {
        private Mixture _mixture;

        public MixtureBuilder()
        {
            Reset();
        }

        public MixtureBuilder AddName(string name)
        {
            if (name is null)
            {
                throw new ArgumentException("Mixture's name cannot be null");
            }

            if (name.Equals(""))
            {
                throw new ArgumentException("Mixture's name cannot be empty string");
            }

            _mixture.Name = name;
            return this;
        }
        public MixtureBuilder AddComponent(Component component)
        {
            if (component is null)
            {
                throw new ArgumentException("Component cannot be null");
            }

            if (component.Item is null)
            {
                throw new ArgumentException("Item cannot be null");
            }

            if (component.Item.Name is null)
            {
                throw new ArgumentException("Item's name cannot be null");
            }

            if (component.Item.Name.Equals("") )
            {
                throw new ArgumentException("Item's name cannot be empty string");
            }

            _mixture.Components.Add(component);
            return this;
        }
        public MixtureBuilder AddComponent( Item item, int quantity, ComponentType componentType =ComponentType.Mandatory)
        {
            if (item is null)
            {
                throw new ArgumentException("Item cannot be null");
            }

            if (item.Name is null)
            {
                throw new ArgumentException("Item's name cannot be null");
            }

            var component = new Component()
            {
                ComponentType = componentType,
                Item = item,
                Quantity = quantity
            };

            _mixture.Components.Add(component);
            return this;
        }
        public MixtureBuilder AddComponent(string name, int quantity, ComponentType componentType = ComponentType.Mandatory)
        {
            if (name is null)
            {
                throw new ArgumentException("Item's name cannot be null");
            }

            if (name.Equals(""))
            {
                throw new ArgumentException("Item's name cannot be empty string");
            }

            var component = new Component()
            {
                ComponentType = componentType,
                Item = new Item(){Name = name},
                Quantity = quantity
            };

            _mixture.Components.Add(component);
            return this;
        }
        public Mixture Build()
        {
            return _mixture;
        }
        public MixtureBuilder Reset()
        {
            _mixture = new Mixture();
            return this;
        }
    }
}
