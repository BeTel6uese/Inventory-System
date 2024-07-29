using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Shop
    {
        private List<InventoryItem> itemsForSale;

        public Shop()
        {
            itemsForSale = new List<InventoryItem>
            {
                new InventoryItem("Sword", 10, "A sharp blade", 500),
                new InventoryItem("Dagger", 15, "A small, quick blade", 250),
                new InventoryItem("Axe", 5, "A heavy axe", 600),
                new InventoryItem("Mace", 8, "A spiked mace", 550),
                new InventoryItem("Spear", 12, "A long spear", 450),
                new InventoryItem("Club", 10, "A wooden club", 200),
                new InventoryItem("Helmet", 8, "A protective helmet", 250),
                new InventoryItem("Chestplate", 5, "A sturdy chestplate", 750),
                new InventoryItem("Gauntlets", 10, "Protective gauntlets", 300),
                new InventoryItem("Leggings", 7, "Protective leggings", 400),
                new InventoryItem("Boots", 12, "Sturdy boots", 350),
                new InventoryItem("Shield", 5, "A sturdy shield", 350)
            };
        }

        public void DisplayItemsForSale()
        {
            Console.WriteLine($"{"Name",-15} {"Quantity",-10} {"Description",-30} {"Price",-10} ");
            Console.WriteLine(new string('-', 70));
            foreach (var item in itemsForSale)
            {
                item.DisplayItem();
            }
        }

        public bool IsValidItem(string itemName)
        {
            return itemsForSale.Exists(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public InventoryItem GetItem(string itemName)
        {
            return itemsForSale.Find(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public InventoryItem PurchaseItem(string itemName, int quantity)
        {
            InventoryItem item = itemsForSale.Find(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null && item.Quantity >= quantity)
            {
                item.Quantity -= quantity;
                return new InventoryItem(item.Name, quantity, item.Description, item.Price);
            }
            else
            {
                Console.WriteLine("Item not available or insufficient quantity.");
                return null;
            }
        }

        public void SortItemsByName()
        {
            itemsForSale.Sort((x, y) => x.Name.CompareTo(y.Name));
            Console.WriteLine("Items sorted by name.");
        }

        public void SortItemsByPrice()
        {
            itemsForSale.Sort((x, y) => x.Price.CompareTo(y.Price));
            Console.WriteLine("Items sorted by price.");
        }
    }
}
