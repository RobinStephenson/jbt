using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    protected Inventory Inventory;
    protected InputController Input;
    protected Market Market;

    public abstract void StartPhaseOne(Timeout timeout);
    public abstract void StartPhaseTwo(Timeout timeout);
    public abstract void StartPhaseThree(Timeout timeout);
    public abstract void StartPhaseFour();
    public abstract void StartPhaseFive();

    protected AbstractPlayer(InputController inputController, Inventory inv, Market market)
    {
        Input = inputController;
        Inventory = inv;
        Market = market;
    }
}
