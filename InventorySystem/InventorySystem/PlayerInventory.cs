using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class PlayerInventory
    {
        private List<InventoryItem> playerItems;

        public PlayerInventory()
        {
            playerItems = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            InventoryItem existingItem = playerItems.Find(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                playerItems.Add(item);
            }
            Console.WriteLine($"{item.Name} x{item.Quantity} added to your inventory.");
        }

        public bool IsEmpty()
        {
            return playerItems.Count == 0;
        }

        public void DisplayPlayerInventory()
        {
            if (IsEmpty())
            {
                Console.WriteLine("No items in inventory. Please purchase some items.");
            }
            else
            {
                Console.WriteLine($"{"Name",-15} {"Quantity",-10} {"Description",-30} {"Price",-10} Gems");
                Console.WriteLine(new string('-', 70));
                foreach (var item in playerItems)
                {
                    item.DisplayItem();
                }
            }
        }
    }
}
