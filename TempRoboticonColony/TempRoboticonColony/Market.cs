using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempRoboticonColony
{
    using System.Collections;
    using System.Collections.Generic;

    sealed public class Market
    {
        public Inventory Stock { get; private set; }
        public bool Open { get; private set; }

        private Dictionary<ItemType, int> _buyprice;
        private Dictionary<ItemType, int> _sellprice;

        public Market(Inventory stock)
        {
            Stock = stock;

            //TEMP: Set buy and sell price manually, will probably populate them from a text file in future
            _buyprice[ItemType.Ore] = 10;
            _buyprice[ItemType.Power] = 11;
            _buyprice[ItemType.Roboticon] = 12;
            _sellprice[ItemType.Ore] = 9;
            _sellprice[ItemType.Power] = 8;
            _sellprice[ItemType.Roboticon] = 7;
        }

        public Market(Inventory stock, bool open) : this(stock)
        {
            Open = open;
        }

        public int GetBuyPrice(ItemType item)
        {
            return _buyprice[item];
        }

        public int GetSellPrice(ItemType item)
        {
            return _sellprice[item];
        }

        /// <summary>
        /// Allows a player to buy from the market.
        /// </summary>
        /// <param name="item">The item the player wishes to buy.</param>
        /// <param name="Quantity">The quantity the player withes to buy.</param>
        /// <param name="playerInventory">Reference to the players inventory.</param>
        /// <returns></returns>
        public bool Buy(ItemType item, int quantity, Inventory playerInventory)
        {
            //Attempt to transfer money from the player to the market. If successful, then try to transfer the purchased item(s)
            if (playerInventory.Transfer(ItemType.Money, _buyprice[item] * quantity, Stock))
            {
                //Attempt to transfer the requested item(s) into the players inventory. If true, then the transaction is complete, if false, then revert the money transaction and return false.
                if (Stock.Transfer(item, quantity, playerInventory))
                    return true;
                else
                    Stock.Transfer(ItemType.Money, _buyprice[item] * quantity, playerInventory);
            }

            return false;
        }

        ///
        public bool Sell(ItemType item, int quantity, Inventory playerInventory)
        {
            //Attempt to transfer money from the market to the player. If successful, then try to transfer the purchased item(s)
            if (Stock.Transfer(ItemType.Money, _buyprice[item] * quantity, playerInventory))
            {
                //Attempt to transfer the requested item(s) into the markets inventory. If true, then the transaction is complete, if false, then revert the money transaction and return false.
                if (playerInventory.Transfer(item, quantity, Stock))
                    return true;
                else
                    playerInventory.Transfer(ItemType.Money, _buyprice[item] * quantity, Stock);
            }

            return false;
        }
    }

    public enum ItemType
    {
        Money = 0,
        Ore,
        Power,
        Roboticon
    }
}
