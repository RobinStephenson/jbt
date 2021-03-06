﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : AbstractPlayer
{
    /// <summary>
    /// Create a HumanPlayer with the given inputController
    /// </summary>
    /// <param name="inv">a player inventory</param>
    /// <param name="market">the market</param>
    /// <param name="estateAgent">the estate agent</param>
    public HumanPlayer(string playerName, Inventory inv, Market market, Sprite tileSprite) : base(playerName, inv, market, tileSprite)
    {
    }

    public override bool DoPhaseOne(Tile t)
    {
        try
        {
            Market.BuyTile(t, this);
            return true;
        }
        catch (NotEnoughMoneyException)
        {
            return false;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override bool DoPhaseTwo(int amount)
    {
        try
        {
            Market.Buy(ItemType.Roboticon, amount, this);
            return true;
        }
        catch (NotEnoughItemException)
        {
            return false;
        }
        catch (NotEnoughMoneyException)
        {
            return false;
        }
    }

    public override bool DoPhaseThreeInstall(PhysicalTile t)
    {
        try
        {
            t.ContainedTile.InstallRoboticon(this);
            t.SetAttachedRoboticon("Objects/BaseRobo");
            return true;
        }
        catch(RoboticonAlreadyInstalledException)
        {
            return false;
        }
        catch(ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    public override bool DoPhaseThreeCustomise(Roboticon r, RoboticonCustomisation rc)
    {
        try
        {
            Market.BuyCustomisation(rc, r, Inv);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public override Dictionary<ItemType,int> DoPhaseFour()
    {
        Dictionary<ItemType, int> production = new Dictionary<ItemType, int>();
        production[ItemType.Ore] = 0;
        production[ItemType.Power] = 0;

        foreach(Tile t in Inv.Tiles)
        {
            production[ItemType.Ore] += t.Produce()[ItemType.Ore];
            production[ItemType.Power] += t.Produce()[ItemType.Power];
        }

        Inv.AddItem(ItemType.Ore, production[ItemType.Ore]);
        Inv.AddItem(ItemType.Power, production[ItemType.Power]);

        return production;
    }

    public override bool DoPhaseFiveBuy(ItemType t, int amount)
    {
        try
        {
            Market.Buy(t, amount, this);
            return true;
        }
        catch (NotEnoughItemException)
        {
            return false;
        }
        catch (NotEnoughMoneyException)
        {
            return false;
        }
    }

    public override bool DoPhaseFiveSell(ItemType t, int amount)
    {
        try
        {
            Market.Sell(t, amount, this);
            return true;
        }
        catch (NotEnoughItemException)
        {
            return false;
        }
        catch (NotEnoughMoneyException)
        {
            return false;
        }
    }
}
