using System;
using System.Collections.Generic;

public class HumanPlayer : AbstractPlayer
{
    /// <summary>
    /// Create a HumanPlayer with the given inputController
    /// </summary>
    /// <param name="inv">a player inventory</param>
    /// <param name="market">the market</param>
    /// <param name="estateAgent">the estate agent</param>
    public HumanPlayer(string playerName, Inventory inv, Market market) : base(playerName, inv, market)
    {
    }

    private bool CanBuyCheapest()
    {
        int CheapestTilePrice = int.MaxValue;
        //TODO: Uncomment when estateagent is merged
        //foreach (Tile UnsoldTile in Market.GetUnsoldTiles())
        //{
        //    int CurrentTilePrice = Market.GetTilePrice(UnsoldTile);
        //    if (CurrentTilePrice < CheapestTilePrice)
        //    {
        //        CheapestTilePrice = CurrentTilePrice;
        //    }
        //}
        return CheapestTilePrice <= Inventory.Money;
    }

    /// <summary>
    /// The player may choose to buy a plot of land
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void StartPhaseOne()
    {
        // check the player has enough money for at least the cheapest tile
        if (!CanBuyCheapest())
        {
            //Input.Confirm("You dont have enough money to buy a tile this round", timeout: timeout);
            return;
        }

        Action<Tile> BuyTile = delegate (Tile tileToBuy)
        {
            //TODO: Uncomment when estateagent is merged
            //Market.BuyTile(tileToBuy, Inventory);
        };

        Action<bool?> ChooseTileToBuy = delegate (bool? wantsToBuy)
        {
            // TODO create a list of unnocupied tiles the player can afford
            List<Tile> AvailableTiles = null;

            if (wantsToBuy == true)
            {
                //Input.PromptTile("Which tile would you like to buy?", BuyTile, AvailableTiles, timeout);
            }
        };

        // does the player want to buy a tile?
        //Input.PromptBool("Do you want to buy a tile?", ChooseTileToBuy, timeout);
    }

    private void PurchaseRoboticons()
    {
        int MaxQuantity = MaxQuantityPlayerCanBuy(ItemType.Roboticon);
        if (MaxQuantity <= 0)
        {
            //Input.Confirm("You cant afford any roboticons this round", timeout: timeout);
            return;
        }

        Action<int?> BuyRoboticons = delegate (int? quantityToBuy)
        {
            if (quantityToBuy > 0)
            {
                Market.Buy(ItemType.Roboticon, (int)quantityToBuy, Inventory);
            }
        };

        //Input.PromptInt("How many roboticons don you want to buy this turn?", BuyRoboticons, timeout, max: MaxQuantity, cancelable: true);
    }

    private void CustomiseRoboticons()
    {
        Action<Tile> WhichCustomisation = delegate (Tile tile)
        {
            Roboticon RoboticonToCustomise = tile.InstalledRoboticon;

            Action<RoboticonCustomisation> BuyCustomisationForRoboticon = delegate (RoboticonCustomisation customisation)
            {
                if (customisation == null)
                {
                    return;
                }
                else
                {
                    Market.BuyCustomisation(customisation, RoboticonToCustomise, Inventory);
                }
            };

            if (tile == null)
            {
                return;
            }
            else
            {
                // TODO: get a list of available customisations for the roboticon
                List<RoboticonCustomisation> AvailableCustomisations = null;

                //Input.PromptCustomisation("Which customisation do you want to apply", BuyCustomisationForRoboticon, AvailableCustomisations);
            }
        };

        Action<bool?> ChooseRoboticonToCustomise = delegate (bool? wantsToCustomise)
        {
            if (wantsToCustomise == true)
            {


                // Can only customise installed roboticons as at time of writing removing a roboticon from a tile clears customisations
                // TODO: create a list of tiles with installed roboticons here
                //List<Tile> TilesWithRoboticon = null;
                //Input.PromptTile("Which robototicon do you want to customise?", WhichCustomisation, TilesWithRoboticon, timeout, cancelable: true);
            }
        };

        //Input.PromptBool("Do you want to customise a roboticon?", ChooseRoboticonToCustomise, timeout);
    }

    /// <summary>
    /// The player may purchase and customise roboticons
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void StartPhaseTwo()
    {
        //PurchaseRoboticons(timeout);

        //// TODO: check the player actually has roboticons to customise and money to buy new customisations
        //if (timeout.Finished)
        //{
        //    return;
        //}

        //CustomiseRoboticons(timeout);
        
    }

    private void RemoveRoboticons()
    {
        Action<Tile> RemoveRoboticonFromTile = delegate (Tile tile)
        {
            if (tile != null)
            {
                tile.RemoveRoboticon();
            }
        
            // Recursive so that players can remove another roboticon or go again if they cancelled
            //RemoveRoboticons(timeout);
        };

        Action<bool?> ChooseRoboticonToRemove = delegate (bool? wantsToRemove)
        {
            if (wantsToRemove == true)
            {
                // TODO create a list of roboticons the player has installed
                List<Tile> TilesWithRoboticonInstalled = null;
                //Input.PromptTile("Which tile do you want to remove the roboticon from?", RemoveRoboticonFromTile, TilesWithRoboticonInstalled, timeout, cancelable: true);
            }
        };

        //Input.PromptBool("Do you want to remove any roboticons? this will remove their customisations", ChooseRoboticonToRemove, timeout);
    }

    private void InstallRoboticons()
    {
        Action<Tile> InstallRoboticon = delegate (Tile tile)
        {
            if (tile != null)
            {
                tile.InstallRoboticon(Inventory);
            }

            // Recursive so that players can install another roboticon or go again if they cancelled
            //InstallRoboticons(timeout);
            
        };

        Action<bool?> ChooseTileToInstall = delegate (bool? wantsToInstall)
        {
            if (wantsToInstall == true)
            {
                // TODO Create a list of tiles where the player could install a roboticon
                List<Tile> PossibleTiles = null;
                //Input.PromptTile("Where do you want to install a roboticon", InstallRoboticon, PossibleTiles, timeout, cancelable: true);
            }
        };

        //Input.PromptBool("Do you want to isntall a roboticon?", ChooseTileToInstall, timeout);
    }

    /// <summary>
    /// The player may remove and install roboticons from their owned tiles
    /// </summary>
    /// <param name="timeout">a time for which the phase can run</param>
    public override void StartPhaseThree()
    {
        // remove roboticons frist so that the player can then install them elsewhere
        // TODO: notify the user that this will clear their customisations
        // TODO: check the player actually has some roboticons installed
        //RemoveRoboticons(timeout);

        //if (!timeout.Finished && Inventory.GetItemAmount(ItemType.Roboticon) >= 1)
        //{
        //    InstallRoboticons(timeout);
        //}
    }

    /// <summary>
    /// Show the player their inventory after the colony has produced
    /// </summary>
    public override void StartPhaseFour()
    {
        int OreProduction = 0;
        int EnergyProduction = 0;

        // TODO
        // for each tile
        // for each roboticon
        // for each resource
        // add to the total production

        String ProductionMessage = String.Format("Your colony produced {0} Ore, {1} Food this turn", OreProduction, EnergyProduction);
        //Input.Confirm(ProductionMessage);
    }

    /// <summary>
    /// The player may buy or sell to the market
    /// </summary>
    public override void StartPhaseFive()
    {
        Action<ItemType?> ChooseQuantityToSell = delegate (ItemType? resource)
        {
            if (resource != null)
            {
                Action<int?> SellResource = delegate (int? quantity)
                {
                    if (quantity > 0)
                    {
                        Market.Buy((ItemType)resource, (int)quantity, Inventory);
                    }
                };

                //Input.PromptInt(
                //   "How much?",
                //   SellResource,
                //   min: 1,
                //   max: MaxQuantityPlayerCanSell((ItemType)resource),
                //   cancelable: true);
            }
        };

        Action<ItemType?> ChooseQuantityToBuy = delegate (ItemType? resource)
        {
            if (resource != null)
            {
                Action<int?> BuyResource = delegate (int? quantity)
                {
                    if (quantity > 0)
                    {
                        Market.Buy((ItemType)resource, (int)quantity, Inventory);
                    }
                };

                //Input.PromptInt(
                //    "How much?",
                //    BuyResource,
                //    min: 1,
                //    max: MaxQuantityPlayerCanBuy((ItemType)resource),
                //    cancelable: true);
            }
        };

        Action<bool?> ChooseResource = delegate (bool? wantsToBuy)
        {
            if (wantsToBuy == true)
            {
                // which resource
                //Input.PromptResource("Which resource do you want to buy?", ChooseQuantityToBuy, cancelable: true);
            }
            else if (wantsToBuy == false)
            {
                // wants to sell
                //Input.PromptResource("Which resource do you want to sell?", ChooseQuantityToSell, cancelable: true);
            }

            // recurive so the player can buy/sell again
            StartPhaseFive();
        };

        //Input.PromptBool("Do you want to buy or sell at the market?", ChooseResource, trueOption: "Buy", falseOption: "Sell", cancelable: true);
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
        // TODO: use a market method which will return the max the shop will buy based on the money in the shop and the shop not wanting to buy too many of an item
        int QuantityShopWillBuy = int.MaxValue;
        return Math.Min(Inventory.GetItemAmount(item), QuantityShopWillBuy);
    }
}
