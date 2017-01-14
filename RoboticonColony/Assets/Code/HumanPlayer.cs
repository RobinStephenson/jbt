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
    /// <param name="timeout">a time for which the phase can run</param>
    public override void DoPhaseOne(Timeout timeout)
    {
        // does the player want to buy a tile?
        bool wishesToPurchase = false;
        try
        {
            wishesToPurchase = input.PromptBool("Do you wish to purchase a tile?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }

        // loop until timeout, cancel, or tile purchased
        while (wishesToPurchase)
        {
            // get the user to choose a tile to buy
            Tile chosenTile;
            try
            {
                chosenTile = ChooseTile(timeout);
            }
            catch (Exception exception)
            {
                if (exception is CancelledException || exception is TimeoutException)
                {
                    return;
                }
                throw;
            }

            // try to buy the tile
            try
            {
                estateAgent.Buy(chosenTile, inventory);
                wishesToPurchase = false;
            }
            catch (TimeoutException)
            {
                return;
            }
            catch
            {
                // TODO catch a not enough money exception
                // TODO catch a already owned exception
                // give the user another go to pick a tile
            }
        }
    }

    /// <summary>
    /// The player may purchase and customise roboticons
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void DoPhaseTwo(Timeout timeout)
    {
        PurchaseRoboticons(timeout);
        if (!timeout.Finished)
        {
            CustomiseRoboticons(timeout);
        }
        
    }

    private void PurchaseRoboticons(Timeout timeout)
    {
        // ask the user if they want to buy any roboticons
        bool wishesToPurchase;
        try
        {
            wishesToPurchase = input.PromptBool("Do you wish to look at the markets selection of roboticons?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }

        if (wishesToPurchase)
        {
            // get the price and quantity of roboticons available from the market
            int roboticonPrice = market.GetBuyPrice(ItemType.Roboticon);
            int quantityInMarket = market.Stock.GetItemAmount(ItemType.Roboticon);

            // work out the maximimum number of roboticons a player can buy
            int quantityPlayerCanAfford = inventory.Money / roboticonPrice;
            int maxQuantity = Math.Min(quantityInMarket, quantityPlayerCanAfford);

            bool purchaseComplete = false;
            do
            {
                // get the number of roboticons te player wants to buy
                int quantity;
                try
                {
                    quantity = input.PromptInt(
                        String.Format("How many roboticons do you wish to purchase? £{0} per roboticon", roboticonPrice),
                        timeout,
                        min: 1,
                        max: maxQuantity,
                        cancelable: true);
                }
                catch (Exception exception)
                {
                    if (exception is TimeoutException || exception is CancelledException)
                    {
                        return;
                    }
                    throw;
                }

                // try to make the purchase
                try
                {
                    market.Buy(ItemType.Roboticon, quantity, inventory);
                    purchaseComplete = true;
                }
                catch
                {
                    // TODO: Create a catch for not enough money
                    // TODO: Create a catch for not enough stock
                }
            } while (!purchaseComplete);
        }
    }

    private void CustomiseRoboticons(Timeout timeout)
    {
    }

    /// <summary>
    /// The player may install/remove roboticons from their owned tiles
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void DoPhaseThree(Timeout timeout)
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
    /// <param name="timeout" >a timeout for the user to choose in</param>
    /// <exception cref="CancelledException">thrown if the user cancels</exception>
    /// <exception cref="TimeoutException">thrown if the user runs out of time</exception>
    /// <returns>the chosen tile or null if the user times out </returns>
    private Tile ChooseTile(Timeout timeout)
    {
        return null;
    }
}
