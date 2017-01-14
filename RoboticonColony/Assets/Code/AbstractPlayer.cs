using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class AbstractPlayer
{
    protected Inventory inventory;
    protected IInputController input;

    public abstract void DoPhaseOne();
    public abstract void DoPhaseTwo();
    public abstract void DoPhaseThree();
    public abstract void DoPhaseFour();
    public abstract void DoPhaseFive();

    protected AbstractPlayer(IInputController inputController, Inventory inv)
    {
        input = inputController;
        inventory = inv;
    }
}
