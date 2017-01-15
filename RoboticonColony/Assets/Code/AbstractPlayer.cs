using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    protected Inventory Inventory;
    protected InputController Input;
    protected EstateAgent EstateAgent;
    protected Market Market;

    public abstract void DoPhaseOne(Timeout timeout);
    public abstract void DoPhaseTwo(Timeout timeout);
    public abstract void DoPhaseThree(Timeout timeout);
    public abstract void DoPhaseFour();
    public abstract void DoPhaseFive();

    protected AbstractPlayer(InputController inputController, Inventory inv, Market market, EstateAgent estateAgent)
    {
        Input = inputController;
        Inventory = inv;
        Market = market;
        EstateAgent = estateAgent;
    }
}
