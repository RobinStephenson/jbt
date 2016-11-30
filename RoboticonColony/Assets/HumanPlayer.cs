using UnityEngine;
using System;

public class HumanPlayer : AbstractPlayer
{
    /// <summary>
    /// Create a HumanPlayer with the given inputController
    /// </summary>
    /// <param name="inputController">object to gather user input</param>
    public HumanPlayer(IInputController inputController) : base(inputController)
    {
        input = inputController;
    }

    /// <summary>
    /// The player may choose to buy a plot of land
    /// </summary>
    public override void DoPhaseOne()
    {

    }

    /// <summary>
    /// The player may purchase and customise roboticons
    /// </summary>
    public override void DoPhaseTwo()
    {

    }

    /// <summary>
    /// The player may install/remove roboticons from their owned tiles
    /// </summary>
    public override void DoPhaseThree()
    {

    }

    /// <summary>
    /// Show the player their inventory after the colony has produced
    /// </summary>
    public override void DoPhaseFour()
    {
        
    }

    /// <summary>
    /// The player may buy or sell to the market
    /// </summary>
    public override void DoPhaseFive()
    {

    }
}
