using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Market {
    public Resources Stock { get; private set; }
    public bool Open { get; private set; }

    private Dictionary<TradeableItem, int> _buyPrices;
    private Dictionary<TradeableItem, int> _sellPrices;

    public Market(Resources stock)
    {
        Stock = stock;
        Open = true;
        //How will we populate buy and sell prices at the start of the game? Read from a text file? Randomly generate? Pass in?
    }

    public Market(Resources stock, bool open)
    {
        Stock = stock;
        Open = open;
    }

    public int GetBuyPrice(TradeableItem item)
    {
        return _buyPrices[item];
    }

    public int GetSellPrice(TradeableItem item)
    {
        return _sellPrices[item];
    }

    public bool Buy(TradeableItem item, int Quantity)
    {
        return true;
    }
}

public enum TradeableItem
{
    Ore = 1,
    Power,
    Roboticon
