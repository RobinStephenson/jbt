using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class AbstractPlayer
{
    public string PlayerName { get; private set; }
    public Sprite TileSprite { get; private set; }
    public Inventory Inv;
    protected Market Market;

    public abstract bool DoPhaseOne(Tile t);
    public abstract bool DoPhaseTwo(int amount);
    public abstract bool DoPhaseThreeInstall(PhysicalTile t);
    public abstract bool DoPhaseThreeCustomise(PhysicalTile t, RoboticonCustomisation rc);
    public abstract Dictionary<ItemType, int> DoPhaseFour();

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
}