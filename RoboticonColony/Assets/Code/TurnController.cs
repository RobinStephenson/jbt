using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    //Player and market variables
    private Market market;
    public AbstractPlayer player1;
    public AbstractPlayer player2;

    //Turn variables
    public int turnCount;
    public int phaseCount;
    public AbstractPlayer activePlayer;

    //Timer variables
    public Text TimerText;
    public Timeout currentTimer;
    
    //Hardcoded(for now) time limits for each phase
    private int[] PhaseTimes = new int[5] { -1, 40, 40, -1, -1 };

	void Start () {
        //Get a list of all tiles to populate the market with
        List<Tile> allTiles = new List<Tile>();

        for (int i = 0; i < GameObject.Find("Tiles").transform.childCount; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i).GetComponent<PhysicalTile>().ContainedTile);
        }

        //Create Market instance for this game and populate it with the new tiles and starting inventory values
        Inventory marketInv = new Inventory(99999999, 0, 16, 12);
        market = new Market(marketInv,5,6,3,5,3,5);
        market.Stock.SetTiles(allTiles);

        //Create customisations for the Market
        Dictionary<ItemType, int> oreIBonus = new Dictionary<ItemType, int>();
        Dictionary<ItemType, int> powerIBonus = new Dictionary<ItemType, int>();
        oreIBonus[ItemType.Ore] = 2;
        oreIBonus[ItemType.Power] = 1;
        powerIBonus[ItemType.Ore] = 1;
        powerIBonus[ItemType.Power] = 2;
        RoboticonCustomisation oreI = new RoboticonCustomisation("Ore I", oreIBonus, null, 15, "Sprites/OreRobo");
        RoboticonCustomisation powerI = new RoboticonCustomisation("Power I", powerIBonus, null, 15, "Sprites/EnergyRobo");
        market.AddCustomisation(oreI);
        market.AddCustomisation(powerI);

        //Initialise players and inventories
        Inventory p1Inv = new Inventory(100, 10, 10, 0);
        Inventory p2Inv = new Inventory(100, 10, 10, 0);
        player1 = new HumanPlayer(MainMenuController.PlayerOneName, p1Inv, market, Resources.Load<Sprite>("Sprites/Player1 Tile"));
        player2 = new HumanPlayer(MainMenuController.PlayerTwoName, p2Inv, market, Resources.Load<Sprite>("Sprites/Player2 Tile"));

        //Start the game
        NextPhase();
	}
	
	void Update () {
        //The timer is only used in phase 2 and 3, as per the requirements
        if(currentTimer.Finished && (phaseCount == 2 || phaseCount == 3))
        {
            NextPhase();
        }
        
        UIController.UpdateTimerDisplay(currentTimer);
        UIController.UpdateResourceDisplay(activePlayer);

        //Display information about currently selected tile, can only be done on phase 1 and phase 3
        if (phaseCount == 1 || phaseCount == 3)
        {
            if (PhysicalTile.selectedTile != null)
            {
                UIController.DisplayTileInfo(PhysicalTile.selectedTile);
            }
            else
            {
                UIController.HideTileInfo();
            }
        }

        //Logic for selecting a tile in phase 1 and 3, these are the only phases where a user can select a tile
        if(phaseCount == 1)
        {
            if(Input.GetMouseButtonDown(0) && PhysicalTile.selectedTile != null)
            {
                if(PhysicalTile.selectedTile.ContainedTile.Owner != null)
                {
                    UIController.DisplayMessage("This tile is already owned by a player!");
                    return;
                }

                if (activePlayer.DoPhaseOne(PhysicalTile.selectedTile.ContainedTile))
                {
                    PhysicalTile.selectedTile.SetSprite(activePlayer.TileSprite);
                    UIController.HideTileInfo();
                    NextPhase();
                }
                else
                {
                    UIController.DisplayMessage("You cannot afford this tile, please buy another!");
                }
            }
        }
        else if(phaseCount == 3)
        {
            if(Input.GetMouseButtonDown(0) && PhysicalTile.selectedTile != null)
            {
                if (PhysicalTile.selectedTile.ContainedTile.Owner != activePlayer)
                {
                    UIController.DisplayMessage("This is not your tile!");
                    return;
                }
                else
                {
                    PhysicalTile.canSelect = false;
                    //Display install roboticon panel if there is no roboticon on the tile, else display customise roboticon
                    if (PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon == null)
                    {
                        UIController.ShowInstallRoboticon();
                    }
                    else
                    {                      
                        UIController.ShowCustomiseRoboticon(PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon);
                    }
                }
            }
        }
	}

    public void NextPhase()
    {
        //If activePlayer is null, then the game has just begun, initialise variables and start the game
        if(activePlayer == null)
        {
            turnCount = 1;
            phaseCount = 1;
            activePlayer = player1;
        }
        else if(activePlayer == player1)
        {
            activePlayer = player2;
        }
        else
        {
            phaseCount++;
            activePlayer = player1;
        }

        if (phaseCount == 6)
        {
            turnCount++;
            phaseCount = 1;
            //Convert ore to roboticons in market once per turn
            market.NewTurn(5);
        }

        UIController.UpdateTurnInfo(turnCount, phaseCount, activePlayer.PlayerName);
        UIController.UpdateResourceDisplay(activePlayer);
        currentTimer = new Timeout(PhaseTimes[phaseCount - 1]);
        
        switch(phaseCount)
        {
            case 1:
                PhysicalTile.canSelect = true;
                UIController.HideMarketDisplay();
                UIController.DisplayMessage("Buy a tile, or click next phase!");
                break;
            case 2:
                PhysicalTile.ClearSelected();
                PhysicalTile.canSelect = false;
                UIController.ShowBuyRoboticon(market);
                UIController.DisplayMessage("Buy roboticons from the market, click next phase when finished!");
                break;
            case 3:
                PhysicalTile.canSelect = true;
                UIController.HideBuyRoboticon();
                UIController.HideInstallRoboticon();
                UIController.HideCustomiseRoboticon();
                UIController.DisplayMessage("Install/Customise Roboticons, click next phase when finished!");
                break;
            case 4:
                PhysicalTile.ClearSelected();
                PhysicalTile.canSelect = false;
                UIController.HideInstallRoboticon();
                UIController.HideCustomiseRoboticon();
                UIController.ShowProductionDisplay(activePlayer.DoPhaseFour());
                UIController.DisplayMessage("View your production, then click next phase!");
                break;
            case 5:
                PhysicalTile.ClearSelected();
                PhysicalTile.canSelect = false;
                UIController.HideProductionDisplay();
                UIController.ShowMarketDisplay(market);
                UIController.DisplayMessage("Buy/Sell from the market, then click next phase when finished!");
                break;
        }
    }

    public void BuyRoboticonPressed(int amount)
    {
        if(activePlayer.DoPhaseTwo(amount))
        {
            UIController.DisplayMessage("You bought " + amount.ToString() + " roboticons!");
            UIController.ShowBuyRoboticon(market);
        }
        else
        {
            UIController.DisplayMessage("Could not purchase the required amount of roboticons");
        }
    }

    public void BuyOrePressed(int amount)
    {
        if (activePlayer.DoPhaseFiveBuy(ItemType.Ore, amount))
        {
            UIController.DisplayMessage("You bought " + amount.ToString() + " ore!");
            UIController.ShowMarketDisplay(market);
        }
        else
        {
            UIController.DisplayMessage("Could not purchase the required amount of ore");
        }
    }

    public void BuyPowerPressed(int amount)
    {
        if (activePlayer.DoPhaseFiveBuy(ItemType.Power, amount))
        {
            UIController.DisplayMessage("You bought " + amount.ToString() + " power!");
            UIController.ShowMarketDisplay(market);
        }
        else
        {
            UIController.DisplayMessage("Could not purchase the required amount of power");
        }
    }

    public void SellOrePressed(int amount)
    {
        if (activePlayer.DoPhaseFiveSell(ItemType.Ore, amount))
        {
            UIController.DisplayMessage("You sold " + amount.ToString() + " ore!");
            UIController.ShowMarketDisplay(market);
        }
        else
        {
            UIController.DisplayMessage("Could not sell the required amount of ore");
        }
    }

    public void SellPowerPressed(int amount)
    {
        if (activePlayer.DoPhaseFiveSell(ItemType.Power, amount))
        {
            UIController.DisplayMessage("You sold " + amount.ToString() + " power!");
            UIController.ShowMarketDisplay(market);
        }
        else
        {
            UIController.DisplayMessage("Could not sell the required amount of power");
        }
    }

    public void InstallRoboticonPressed()
    {
        if(PhysicalTile.selectedTile.ContainedTile.Owner != activePlayer)
        {
            return;
        }

        if (activePlayer.DoPhaseThreeInstall(PhysicalTile.selectedTile))
        {
            UIController.HideInstallRoboticon();
            UIController.ShowCustomiseRoboticon(PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon);
            UIController.DisplayMessage("Installed Roboticon!");
        }
        else
        {
            UIController.DisplayMessage("You don't have enough roboticons!");
        }
    }

    public void CancelInstallRoboticonPressed()
    {
        UIController.HideInstallRoboticon();
        PhysicalTile.canSelect = true;
    }

    public void CancelCustomiseRoboticonPressed()
    {
        UIController.HideCustomiseRoboticon();
        PhysicalTile.canSelect = true;
    }

    public void RemoveRoboticonPressed()
    {
        PhysicalTile.selectedTile.ContainedTile.RemoveRoboticon();
        PhysicalTile.selectedTile.RemoveAttachedRoboticon();
        UIController.HideCustomiseRoboticon();
        UIController.ShowInstallRoboticon();
    }
    
    public void BuyCustomisationPressed(int customisationIndex)
    {
        if (activePlayer.DoPhaseThreeCustomise(PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon, market.CustomisationsList[customisationIndex]))
        {
            UIController.DisplayMessage("Customised Roboticon");
            PhysicalTile.selectedTile.SetAttachedRoboticonCustomisation(market.CustomisationsList[customisationIndex].SpritePath);

            //Refresh the panel to show the bought customisation
            UIController.ShowCustomiseRoboticon(PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon);
        }
        else
        {
            UIController.DisplayMessage("You can't afford this customistaion");
        }
    }
}