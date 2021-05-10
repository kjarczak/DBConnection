namespace DataModel
{
    public class Component
    {
        public ComponentType ComponentType { get; set; } = ComponentType.Mandatory;
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
