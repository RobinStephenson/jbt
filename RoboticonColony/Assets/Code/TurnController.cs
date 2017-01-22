﻿using System.Collections;
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
    private int[] PhaseTimes = new int[5] { 5, 60, 60, 60, 60 };

	void Start () {
        //Get a list of all tiles to populate the market with
        List<Tile> allTiles = new List<Tile>();
        Debug.Log(GameObject.Find("Tiles").transform.childCount.ToString());
        for (int i = 0; i < GameObject.Find("Tiles").transform.childCount; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i).GetComponent<PhysicalTile>().ContainedTile);
        }

        //Create Market instance for this game and populate it with the new tiles
        Inventory marketInv = new Inventory(500, 10, 10, 10);
        market = new Market(marketInv,5,6,3,5,3,5);
        market.Stock.SetTiles(allTiles);

        //Create customisations for the Market
        Dictionary<ItemType, int> oreIBonus = new Dictionary<ItemType, int>();
        Dictionary<ItemType, int> powerIBonus = new Dictionary<ItemType, int>();
        oreIBonus[ItemType.Ore] = 2;
        powerIBonus[ItemType.Power] = 2;
        RoboticonCustomisation oreI = new RoboticonCustomisation("Ore I", oreIBonus, null, 15, "Objects/OreRobo");
        RoboticonCustomisation powerI = new RoboticonCustomisation("Panel I", powerIBonus, null, 15, "Objects/PowerRobo");
        market.AddCustomisation(oreI);
        market.AddCustomisation(powerI);

        //Initialise players and inventories
        Inventory p1Inv = new Inventory(100, 10, 10, 0);
        Inventory p2Inv = new Inventory(100, 10, 10, 0);
        player1 = new HumanPlayer("Mikeywalsh", p1Inv, market, Resources.Load<Sprite>("Sprites/Player1 Tile"));
        player2 = new HumanPlayer("Some Guy", p2Inv, market, Resources.Load<Sprite>("Sprites/Player2 Tile"));


        //Start the game
        NextPhase();
	}
	
	void Update () {
        if(currentTimer.Finished)
        {
            NextPhase();
        }

        if (PhysicalTile.selectedTile != null)
        {
            Debug.Log(market.Stock.TileCount().ToString());
            Debug.Log(market.Stock.Tiles.Contains(PhysicalTile.selectedTile.ContainedTile));
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
                        string spritePath;

                        if (PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon.InstalledCustomisations.Count == 0)
                        {
                            spritePath = "Objects/BaseRobo";
                        }
                        else
                        {
                            spritePath = PhysicalTile.selectedTile.ContainedTile.InstalledRoboticon.InstalledCustomisations[0].SpritePath;
                        }
                        UIController.ShowCustomiseRoboticon(spritePath);
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
            Debug.Log((activePlayer == player1).ToString());
            Debug.Log(player1.PlayerName);
            Debug.Log(player2.PlayerName);
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
                UIController.DisplayMessage("Install/Customise Roboticons, click next phase when finished!");
                break;
            case 4:
                PhysicalTile.ClearSelected();
                PhysicalTile.canSelect = false;
                UIController.HideInstallRoboticon();
                UIController.HideCustomiseRoboticon();
                UIController.ShowProductionDisplay(activePlayer.DoPhaseFour());
                //Convert ore to roboticons in market once per phase 4
                if (activePlayer == player2)
                {
                    market.BuyRoboticonOre();
                }
                UIController.DisplayMessage("View your production, then click next phase!");
                break;
            case 5:
                PhysicalTile.ClearSelected();
                PhysicalTile.canSelect = false;
                UIController.HideProductionDisplay();
                UIController.ShowMarketDisplay();
                UIController.DisplayMessage("Buy/Sell from the market, then click next phase when finished!");
                break;
        }
    }

    public void BuyRoboticonPressed(int amount)
    {
        if(activePlayer.DoPhaseTwo(amount))
        {
            UIController.DisplayMessage("You bought " + amount.ToString() + " Roboticons!");
            UIController.ShowBuyRoboticon(market);
        }
        else
        {
            UIController.DisplayMessage("Could not purchase the required amount of roboticons");
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
            PhysicalTile.canSelect = true;
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
        PhysicalTile.canSelect = true;
    }
}