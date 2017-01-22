using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    public string PlayerName { get; private set; }
    protected Inventory Inventory;
    protected InputController Input;
    protected Market Market;

    public abstract void StartPhaseOne(Timeout timeout);
    public abstract void StartPhaseTwo(Timeout timeout);
    public abstract void StartPhaseThree(Timeout timeout);
    public abstract void StartPhaseFour();
    public abstract void StartPhaseFive();

    protected AbstractPlayer(string playerName, InputController inputController, Inventory inv, Market market)
    {
        PlayerName = playerName;
        Input = inputController;
        Inventory = inv;
        Market = market;
    }
}
