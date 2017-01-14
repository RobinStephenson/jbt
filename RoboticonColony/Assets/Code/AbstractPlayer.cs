using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    protected Inventory inventory;
    protected InputController input;
    protected EstateAgent estateAgent;
    protected Market market;

    public abstract void DoPhaseOne(Timeout timeout);
    public abstract void DoPhaseTwo(Timeout timeout);
    public abstract void DoPhaseThree(Timeout timeout);
    public abstract void DoPhaseFour();
    public abstract void DoPhaseFive();

    protected AbstractPlayer(InputController inputController, Inventory inv, Market mark, EstateAgent estate)
    {
        input = inputController;
        inventory = inv;
        market = mark;
        estateAgent = estate;
    }
}
