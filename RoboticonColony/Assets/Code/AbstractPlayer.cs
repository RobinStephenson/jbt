using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    public string PlayerName { get; private set; }
    public Sprite TileSprite { get; private set; }
    public Inventory Inv;
    protected Market Market;

    public abstract bool DoPhaseOne(Tile t);
    public abstract bool DoPhaseTwo(int amount);
    public abstract bool DoPhaseThreeInstall(PhysicalTile t);
    public abstract bool DoPhaseThreeCustomise(Roboticon r, RoboticonCustomisation rc);
    public abstract Dictionary<ItemType, int> DoPhaseFour();
    public abstract bool DoPhaseFiveBuy(ItemType t, int amount);
    public abstract bool DoPhaseFiveSell(ItemType t, int amount);

    protected AbstractPlayer(string playerName, Inventory inv, Market market, Sprite tileSprite)
    {
        PlayerName = playerName;
        Inv = inv;
        Market = market;
        TileSprite = tileSprite;
    }
    
    /// <summary>
    /// The total amount of installed roboticons that this player has
    /// </summary>
    public int InstalledRoboticonCount
    {
        get
        {
            int amount = 0;
            foreach(Tile t in Inv.Tiles)
            {
                if(t.InstalledRoboticon != null)
                {
                    amount++;
                }
            }

            return amount;
        }
    }

    /// <summary>
    /// the maximum quanitity of an item a player can buy based on market stock and player money
    /// </summary>
    /// <param name="item">the item the being bought</param>
    /// <returns>the maximum quantity</returns>
    protected int MaxQuantityPlayerCanBuy(ItemType item)
    {
        int ItemPrice = Market.GetBuyPrice(ItemType.Roboticon);
        int QuantityInMarket = Market.Stock.GetItemAmount(item);
        int QuantityPlayerCanAfford = Inv.Money / ItemPrice;
        return Math.Min(QuantityInMarket, QuantityPlayerCanAfford);
    }

    /// <summary>
    /// the maximum quantity of an item a player can sell to the market, based on user stock and market rules
    /// </summary>
    /// <param name="item">the item being sold</param>
    /// <returns>the maximum quantity</returns>
    protected int MaxQuantityPlayerCanSell(ItemType item)
    {
        // TODO: use a market method which will return the max the shop will buy based on the money in the shop and the shop not wanting to buy too many of an item
        int QuantityShopWillBuy = int.MaxValue;
        return Math.Min(Inv.GetItemAmount(item), QuantityShopWillBuy);
    }
}