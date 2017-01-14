using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    protected Inventory inventory;
    protected IInputController input;

    public abstract void DoPhaseOne(int timeout);
    public abstract void DoPhaseTwo(int timeout);
    public abstract void DoPhaseThree(int timeout);
    public abstract void DoPhaseFour();
    public abstract void DoPhaseFive();

    protected AbstractPlayer(IInputController inputController, Inventory inv)
    {
        input = inputController;
        inventory = inv;
    }
}
