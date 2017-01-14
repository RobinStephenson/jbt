using UnityEngine;
using System;

public class HumanPlayer : AbstractPlayer
{
    /// <summary>
    /// Create a HumanPlayer with the given inputController
    /// </summary>
    /// <param name="inputController">object to gather user input</param>
    /// <param name="inv">a player inventory</param>
    /// <param name="market">the market</param>
    /// <param name="estateAgent">the estate agent</param>
    public HumanPlayer(
        InputController inputController,
        Inventory inv, 
        Market market,
        EstateAgent estateAgent) : base(inputController, inv, market, estateAgent)
    {
    }

    /// <summary>
    /// The player may choose to buy a plot of land
    /// </summary>
    public override void DoPhaseOne(int timeout)
    {
        Tile chosenTile;
        bool tileChosen = false;
        int timeRemaining = timeout;
        do
        {
            // get the user to choose a tile
            chosenTile = ChooseTile(timeRemaining);

            // check the tile is now owned by anyone and the user has enough money
            int tileCost = estateAgent.GetPrice(chosenTile);
            tileChosen = chosenTile.Owner == null && tileCost <= inventory.Money;

            // update time remaining

            // loop until the user has chosen a tile or run out of time
        } while (!tileChosen && timeRemaining > 0);

        if (tileChosen)
        {
            estateAgent.Buy(chosenTile, inventory);
        }
    }

    /// <summary>
    /// The player may purchase and customise roboticons
    /// </summary>
    public override void DoPhaseTwo(int timeout)
    {

    }

    /// <summary>
    /// The player may install/remove roboticons from their owned tiles
    /// </summary>
    public override void DoPhaseThree(int timeout)
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

    /// <summary>
    /// prompt the user to select a tile
    /// </summary>
    /// <param name="timeout" >the time the user has to choose</param>
    /// <returns>the chosen tile or null if the user times out </returns>
    private Tile ChooseTile(int timeout = int.MaxValue)
    {
        return null;
    }
}
