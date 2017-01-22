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
    private int[] PhaseTimes = new int[5] { 60, 60, 60, 60, 60 };

	void Start () {
        //Get a list of all tiles to populate the market with
        List<Tile> allTiles = new List<Tile>();
        for(int i = 0; i < GameObject.Find("Tiles").transform.childCount; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i).GetComponent<PhysicalTile>().ContainedTile);
        }

        //Create Market instance for this game and populate it with the new tiles
        market = new Market(5,6,3,5,3,5);
        market.Stock.SetTiles(allTiles);

        //Initialise players and inventories
        Inventory p1Inv = new Inventory(100, 10, 10, 0);
        Inventory p2Inv = new Inventory(100, 10, 10, 0);
        player1 = new HumanPlayer("Mikeywalsh", p1Inv, market);
        player2 = new HumanPlayer("Some Guy", p2Inv, market);

        //Start the game
        NextPhase();
	}
	
	void Update () {
        Debug.Log("Turn: " + turnCount.ToString() + " Phase: " + phaseCount.ToString() + " Player: " + activePlayer.PlayerName.ToString());

        UIController.UpdateTimerDisplay(currentTimer);

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
            if(Input.GetMouseButtonDown(0) && PhysicalTile.selectedTile)
            {
                if(activePlayer.DoPhaseOne(PhysicalTile.selectedTile.ContainedTile))
                {
                    PhysicalTile.selectedTile.SetOwnerSprite();
                    UIController.HideTileInfo();
                    NextPhase();
                }
                else
                {
                    UIController.DisplayMessage("You cannot afford this tile, please buy another!");
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
        UIController.UpdateResourceDisplay(activePlayer.Inv);
        currentTimer = new Timeout(PhaseTimes[phaseCount - 1]);
        
        switch(phaseCount)
        {
            case 1:
                activePlayer.StartPhaseOne();
                UIController.DisplayMessage("Buy a tile, or click next phase!");
                break;
            case 2:
                activePlayer.StartPhaseTwo();
                UIController.DisplayMessage("Buy roboticons from the market, click next phase when finished!");
                break;
            case 3:
                activePlayer.StartPhaseThree();
                UIController.DisplayMessage("Install/Customise Roboticons, click next phase when finished!");
                break;
            case 4:
                activePlayer.StartPhaseFour();
                UIController.DisplayMessage("View your production, then click next phase!");
                break;
            case 5:
                activePlayer.StartPhaseFive();
                UIController.DisplayMessage("Buy/Sell from the market, then click next phase when finished!");
                break;
        }
    }
}