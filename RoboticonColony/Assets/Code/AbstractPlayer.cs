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

    public abstract void StartPhaseOne();
    public abstract void StartPhaseTwo();
    public abstract void StartPhaseThree();
    public abstract void StartPhaseFour();
    public abstract void StartPhaseFive();

    protected AbstractPlayer(string playerName, Inventory inv, Market market, Sprite tileSprite)
    {
        PlayerName = playerName;
        Inv = inv;
        Market = market;
        TileSprite = tileSprite;
    }
}