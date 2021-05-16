using System;

namespace DataModel
{
    public class ComponentBuilder
    {
        private Component _component;

        public ComponentBuilder()
        {
            Reset();
        }

        public ComponentBuilder AddItem(string name)
        {
            if (name is null)
            {
                throw new ArgumentException("Item's name cannot be null");
            }

            if (name.Equals(""))
            {
                throw new ArgumentException("Item's name cannot be empty string");
            }

            _component.Item = new Item() {Name = name};
            return this;
        }
        public ComponentBuilder AddItem(Item item)
        {
            if (item is null)
            {
                throw new ArgumentException("Item cannot be null");
            }
            _component.Item = item;
            return this;
        }
        public ComponentBuilder AddQuantity(int quantity)
        {
            _component.Quantity = quantity;
            return this;
        }
        public ComponentBuilder AddType(ComponentType type)
        {
            _component.ComponentType = type;
            return this;
        }
        public Component Build()
        {
            return _component;
        }
        public ComponentBuilder Reset()
        {
            _component = new Component();
            return this;
        }
    }
}
