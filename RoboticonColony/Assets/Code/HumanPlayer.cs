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

        // TODO: check the player actually has roboticons to customise and money to buy new customisations
        if (!timeout.Finished)
        {
            CustomiseRoboticons(timeout);
        }
        
    }

    private void PurchaseRoboticons(Timeout timeout)
    {
        int MaxQuantity = MaxQuantityPlayerCanBuy(ItemType.Roboticon);
        if (MaxQuantity <= 0)
        {
            try
            {
                Input.Confirm("You dont have enough money to buy any roboticons this round", timeout);
            }
            catch (TimeoutException)
            {
                return;
            }
            return;
        }
        
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
            bool PurchaseComplete = false;
            do
            {
                // get the number of roboticons te player wants to buy
                int Quantity;
                try
                {
                    Quantity = Input.PromptInt(
                        String.Format("How many roboticons do you wish to purchase? £{0} per roboticon", Market.GetBuyPrice(ItemType.Roboticon)),
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
    /// The player may remove and install roboticons from their owned tiles
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void DoPhaseThree(Timeout timeout)
    {

        if (!timeout.Finished && Inventory.GetItemAmount(ItemType.Roboticon) >= 1)
        {
            InstallRoboticons(timeout);
        }
    }

    private void RemoveRoboticons(Timeout timeout)
    {
        // doest the user want to remove a roboticon?
        bool WishesToRemove;
        try
        {
            WishesToRemove = Input.PromptBool("Would you like to remove a roboticon from any of your tiles?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }
        while (WishesToRemove)
        {
            // which roboticon does the user want to remove
            Tile ChosenTile;
            try
            {
                ChosenTile = Input.ChooseTile(timeout, true);
            }
            catch (Exception exception)
            {
                if (exception is TimeoutException || exception is CancelledException)
                {
                    return;
                }
                throw;
            }

            // TODO: check whether we should add it back into the inventory or whether this happens automatically
            ChosenTile.RemoveRoboticon();

            // does the user want to remove another roboticon
            try
            {
                WishesToRemove = Input.PromptBool("Would you like to remove another?", timeout);
            }
            catch (TimeoutException)
            {
                return;
            }
        }
    }

    private void InstallRoboticons(Timeout timeout)
    {
        // doest the user want to install a roboticon?
        bool WishesToInstall;
        try
        {
            WishesToInstall = Input.PromptBool("Would you like to install a roboticon on any of your tiles?", timeout);
        }
        catch (TimeoutException)
        {
            return;
        }
        while (WishesToInstall)
        {
            // where does the user want to install the roboticon
            Tile ChosenTile;
            try
            {
                ChosenTile = Input.ChooseTile(timeout, true);
            }
            catch (Exception exception)
            {
                if (exception is TimeoutException || exception is CancelledException)
                {
                    return;
                }
                throw;
            }

            // TODO: RemoveRoboticon should also set the installedOnTile field in the roboticon field to null
            // And vice versa if we have a roboticon.removefromtile method
            ChosenTile.RemoveRoboticon();

            // does the user want to install another roboticon
            try
            {
                WishesToInstall = Input.PromptBool("Would you like to install another?", timeout);
            }
            catch (TimeoutException)
            {
                return;
            }
        }
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
        bool WishesToBuyOrSell = Input.PromptBool("Would you like to go to the market?");
        while (WishesToBuyOrSell)
        {
            // TODO
            // display a list of resources with buy/sell prices
            // ...choose which resource the player wants to buy/sell
            // ...temorarily set as ore to stop error messages
            ItemType TransactionItem = ItemType.Ore;

            // does the player want to buy or sell the resource?
            bool ChoseToBuy = Input.PromptBool("Do you want to buy or sell?", trueOption: "Buy", falseOption: "Sell");
            if (ChoseToBuy)
            {
                BuyResource(TransactionItem);
            }
            else
            {
                SellResource(TransactionItem);
            }

            WishesToBuyOrSell = Input.PromptBool("Do you want to stay at the market?");
        }
    }

    // TODO Refactor BuyResource and SellResource into one method
    private void BuyResource(ItemType transactionItem)
    {
        int MaxQuantity = MaxQuantityPlayerCanBuy(transactionItem);
        int ChosenQuantity = 0;
        try
        {
            ChosenQuantity = Input.PromptInt("How many?", min: 1, max: MaxQuantity, cancelable: true);
        }
        catch (CancelledException)
        {
            return;
        }
        Market.Buy(transactionItem, ChosenQuantity, Inventory);
    }

    private void SellResource(ItemType transactionItem)
    {
        int MaxQuantity = MaxQuantityPlayerCanSell(transactionItem);
        int ChosenQuantity = 0;
        try
        {
            ChosenQuantity = Input.PromptInt("How many?", min: 1, max: MaxQuantity, cancelable: true);
        }
        catch (CancelledException)
        {
            return;
        }
        Market.Sell(transactionItem, ChosenQuantity, Inventory);
    }

    /// <summary>
    /// the maximum quanitity of an item a player can buy based on market stock and player money
    /// </summary>
    /// <param name="item">the item the being bought</param>
    /// <returns>the maximum quantity</returns>
    private int MaxQuantityPlayerCanBuy(ItemType item)
    {
        int ItemPrice = Market.GetBuyPrice(ItemType.Roboticon);
        int QuantityInMarket = Market.Stock.GetItemAmount(item);
        int QuantityPlayerCanAfford = Inventory.Money / ItemPrice;
        return Math.Min(QuantityInMarket, QuantityPlayerCanAfford);
    }

    private int MaxQuantityPlayerCanSell(ItemType item)
    {
        // TODO: Create a market method which will return the max the shop will buy based on the money in the shop and the shop not wanting to buy too many of an item
        int QuantityShopWillBuy = int.MaxValue;
        return Math.Min(Inventory.GetItemAmount(item), QuantityShopWillBuy);
    }
}
