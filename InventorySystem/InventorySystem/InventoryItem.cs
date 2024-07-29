using System;

namespace InventorySystem
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public InventoryItem(string name, int quantity, string description, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Description = description;
            Price = price;
        }

        public void DisplayItem()
        {
            Console.WriteLine($"{Name,-15} {Quantity,-10} {Description,-30} {Price,-10} Gems");
        }
    }
}
