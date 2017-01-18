using System;
using System.Collections.Generic;

public class HumanPlayer : AbstractPlayer
{
    /// <summary>
    /// Create a HumanPlayer with the given inputController
    /// </summary>
    /// <param name="inputController">object to gather user input</param>
    /// <param name="inv">a player inventory</param>
    /// <param name="market">the market</param>
    /// <param name="estateAgent">the estate agent</param>
    public HumanPlayer(InputController inputController, Inventory inv, Market market) : base(inputController, inv, market)
    {
    }

    private bool CanBuyCheapest()
    {
        int CheapestTilePrice = int.MaxValue;
        foreach (Tile UnsoldTile in Market.GetUnsoldTiles())
        {
            int CurrentTilePrice = Market.GetTilePrice(UnsoldTile);
            if (CurrentTilePrice < CheapestTilePrice)
            {
                CheapestTilePrice = CurrentTilePrice;
            }
        }
        return CheapestTilePrice <= Inventory.Money;
    }

    /// <summary>
    /// The player may choose to buy a plot of land
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void DoPhaseOne(Timeout timeout)
    {
        // check the player has enough money for at least the cheapest tile
        if (!CanBuyCheapest())
        {
            Input.Confirm("You dont have enough money to buy a tile this round", timeout: timeout);
            return;
        }

        Action<Tile> BuyTile = delegate (Tile tileToBuy)
        {
            Market.BuyTile(tileToBuy, Inventory);
        };

        Action<bool?> ChooseTileToBuy = delegate (bool? wantsToBuy)
        {
            // TODO create a list of unnocupied tiles the player can afford
            List<Tile> AvailableTiles = null;

            if (wantsToBuy == true)
            {
                Input.PromptList<Tile>("Which tile would you like to buy?", BuyTile, AvailableTiles, timeout);
            }
        };

        // does the player want to buy a tile?
        Input.PromptBool("Do you want to buy a tile?", ChooseTileToBuy, timeout);
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
            Input.Confirm("You cant afford any roboticons this round", timeout: timeout);
            return;
        }

        Action<int?> BuyRoboticons = delegate (int? quantityToBuy)
        {
            if (quantityToBuy > 0)
            {
                Market.Buy(ItemType.Roboticon, (int)quantityToBuy, Inventory);
            }
        };

        Input.PromptInt("How many roboticons don you want to buy this turn?", BuyRoboticons, timeout, max: MaxQuantity);
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
        // remove roboticons frist so that the player can then install them elsewhere
        // TODO: check the player actually has some roboticons installed
        RemoveRoboticons(timeout);

        if (!timeout.Finished && Inventory.GetItemAmount(ItemType.Roboticon) >= 1)
        {
            InstallRoboticons(timeout);
        }
    }

    // TODO see if this can be combined with InstallRoboticons
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

            // TODO which roboticon does the user want to install?
            Roboticon ChosenRoboticon = new Roboticon();
            ChosenTile.InstallRoboticon(ChosenRoboticon);

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
        // TODO
        // for each tile
        // for each roboticon
        // for each resource
        // add to the total production
        // display

        Input.Confirm("Your colony produced XYZ this turn");
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

    /// <summary>
    /// the maximum quantity of an item a player can sell to the market, based on user stock and market ruless
    /// </summary>
    /// <param name="item">the item being sold</param>
    /// <returns>the maximum quantity</returns>
    private int MaxQuantityPlayerCanSell(ItemType item)
    {
        // TODO: Create a market method which will return the max the shop will buy based on the money in the shop and the shop not wanting to buy too many of an item
        int QuantityShopWillBuy = int.MaxValue;
        return Math.Min(Inventory.GetItemAmount(item), QuantityShopWillBuy);
    }
}
