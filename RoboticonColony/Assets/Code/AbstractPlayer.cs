using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    public string PlayerName { get; private set; }
    public Inventory Inv;
    protected Market Market;

    public abstract bool DoPhaseOne(Tile t);

    public abstract void StartPhaseOne();
    public abstract void StartPhaseTwo();
    public abstract void StartPhaseThree();
    public abstract void StartPhaseFour();
    public abstract void StartPhaseFive();

    protected AbstractPlayer(string playerName, Inventory inv, Market market)
    {
        PlayerName = playerName;
        Inv = inv;
        Market = market;
    }
}