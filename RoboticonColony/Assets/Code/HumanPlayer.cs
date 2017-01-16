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
        // check the player has enough money for at least the cheapest tile
        int CheapestTilePrice = int.MaxValue;
        foreach (Tile UnsoldTile in EstateAgent.GetAvailableTiles())
        {
            int CurrentTilePrice = EstateAgent.GetPrice(UnsoldTile);
            if (CurrentTilePrice < CheapestTilePrice)
            {
                CheapestTilePrice = CurrentTilePrice;
            }
        }
        if (Inventory.Money < CheapestTilePrice)
        {
            try
            {
                Input.Confirm("You dont have enough money to buy a tile this round", timeout);
            }
            catch (TimeoutException)
            {
                return;
            }
            return;
        }

        // does the player want to buy a tile?
        bool WishesToPurchase = false;
        try
        {
            WishesToPurchase = Input.PromptBool("Do you wish to purchase a tile?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }

        // loop until timeout, cancel, or tile purchased
        while (WishesToPurchase)
        {
            // get the user to choose a tile to buy
            Tile ChosenTile;
            try
            {
                ChosenTile = Input.ChooseTile(timeout, true);
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
                EstateAgent.Buy(ChosenTile, Inventory);
                WishesToPurchase = false;
            }
            catch (TimeoutException)
            {
                return;
            }
            catch (TileAlreadyOwnedException)
            {
                try
                {
                    Input.Confirm("The tile you selected is already owned", timeout);
                }
                catch (TimeoutException)
                {
                    return;
                }
            }
            catch (NotEnoughMoneyException)
            {
                try
                {
                    WishesToPurchase = Input.PromptBool("You cannot afford that tile. Would you like to pick another?", timeout);
                }
                catch (TimeoutException)
                {
                    return;
                }
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
        bool WishesToPurchase;
        try
        {
            WishesToPurchase = Input.PromptBool("Do you wish to look at the markets selection of roboticons?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }

        if (WishesToPurchase)
        {
            // get the price and quantity of roboticons available from the market
            int RoboticonPrice = Market.GetBuyPrice(ItemType.Roboticon);
            int QuantityInMarket = Market.Stock.GetItemAmount(ItemType.Roboticon);

            // work out the maximimum number of roboticons a player can buy
            int QuantityPlayerCanAfford = Inventory.Money / RoboticonPrice;
            int MaxQuantity = Math.Min(QuantityInMarket, QuantityPlayerCanAfford);

            bool PurchaseComplete = false;
            do
            {
                // get the number of roboticons te player wants to buy
                int Quantity;
                try
                {
                    Quantity = Input.PromptInt(
                        String.Format("How many roboticons do you wish to purchase? £{0} per roboticon", RoboticonPrice),
                        timeout,
                        min: 1,
                        max: MaxQuantity,
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
                    Market.Buy(ItemType.Roboticon, Quantity, Inventory);
                    PurchaseComplete = true;
                }
                catch (NotEnoughMoneyException)
                {
                    Input.Confirm("You do not have enough money", timeout);
                }
                catch (NotEnoughItemException)
                {
                    Input.Confirm("The market does not have enought stock", timeout);
                }
            } while (!PurchaseComplete);
        }
    }

    private void CustomiseRoboticons(Timeout timeout)
    {
        bool WishesToCustomise;
        try
        {
            WishesToCustomise = Input.PromptBool("Do you wish to customise a roboticon?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }
        
        while (WishesToCustomise & !timeout.Finished)
        {

            // TODO: select roboticon to customise from a list here

            try
            {
                WishesToCustomise = Input.PromptBool("Do you wish to customise another roboticon?", timeout);
            }
            catch (TimeoutException)
            {
                return;
            }
        }
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
}
