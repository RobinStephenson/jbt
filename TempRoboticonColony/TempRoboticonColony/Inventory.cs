using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoboticonColony
{
    sealed public class Inventory
    {
        private Dictionary<ItemType, int> Items;

        /// <summary>
        /// Create an inventory instance, which holds all resources for a player or market.
        /// </summary>
        /// <param name="money">The money in the inventory.</param>
        /// <param name="ore">The ore in the inventory.</param>
        /// <param name="power">The power in the inventory.</param>
        /// <param name="roboticons">The roboticons in the inventory.</param>
        public Inventory(int money, int ore, int power, int roboticons)
        {
            Items[ItemType.Money] = money;
            Items[ItemType.Ore] = ore;
            Items[ItemType.Power] = power;
            Items[ItemType.Roboticon] = roboticons;

        }

        public int GetAmount(ItemType item)
        {
            return Items[item];
        }

        public void AddAmount(ItemType item, int amount)
        {
            Items[item] += amount;
        }

        /// <summary>
        /// Transfers a select quantity of an item to the provided inventory.
        /// </summary>
        /// <param name="item">The item being transferred.</param>
        /// <param name="quantity">The quantity being transferred.</param>
        /// <param name="to">The inventory to transfer to.</param>
        /// <returns>True if the transfer was successful, false otherwise.</returns>
        public bool Transfer(ItemType item, int quantity, Inventory to)
        {
            if (Items[item] - quantity < 0)
                return false;

            Items[item] -= quantity;
            to.AddAmount(item, quantity);
            return true;
        }
    }
}
