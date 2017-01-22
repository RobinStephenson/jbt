using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    //Used to set UI elements text
    private UIController UIC;

    private Market market;
    public AbstractPlayer player1;
    public AbstractPlayer player2;

    //Market
    public int turnCount;
    public int phaseCount;
    public AbstractPlayer activePlayer;

    //Timer variables
    public Text TimerText;
    private float phaseDuration = 60;
    private float currentPhaseTime;
    
    //Hardcoded(for now) time limits for each phase
    private int[] PhaseTimes = new int[5] { 60, 60, 60, 60, 60 };

	void Start () {
        //Set the UI controller
        UIC = GetComponent<UIController>();

        //Initialise Turn/Phase variables and set the activePlayer to player 1
        turnCount = 1;
        phaseCount = 1;
        currentPhaseTime = 0;
        activePlayer = player1;

        //Get a list of all tiles to populate the market with
        List<Tile> allTiles = new List<Tile>();
        for(int i = 0; i < GameObject.Find("Tiles").transform.childCount; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i).GetComponent<Tile>());
        }

        //Create Market instance for this game and populate it with the new tiles
        market = new Market(5,6,3,5,3,5);
        market.Stock.SetTiles(allTiles);

        //Start the game
        activePlayer.StartPhaseOne();
        UIController.UpdateTurnInfo(turnCount,phaseCount,activePlayer.PlayerName);
	}
	
	void Update () {
        Debug.Log("Turn: " + turnCount.ToString() + " Phase: " + phaseCount.ToString() + " Player: " + activePlayer.Name.ToString());
        currentPhaseTime += Time.deltaTime;
        TimerText.text = (int)(phaseDuration - currentPhaseTime) + "s";

        //Display information about currently selected tile
        if(PhysicalTile.selectedTile != null)
        {
            UIController.DisplayTileInfo(PhysicalTile.selectedTile);            
        }
        else
        {
            UIController.HideTileInfo();
        }
	}

    public void NextPhase()
    {
        if(activePlayer == player1)
        {
            Debug.Log((activePlayer == player1).ToString());
            Debug.Log(player1.Name);
            Debug.Log(player2.Name);
            activePlayer = player2;
        }
        else
        {
            phaseCount++;
            activePlayer = player1;
        }

        if (phaseCount == 6)
            phaseCount = 0;

        SetTurnText();
        currentPhaseTime = 0;
        
        switch(phaseCount)
        {
            case 1:
                activePlayer.StartPhaseOne();
                break;
            case 2:
                activePlayer.StartPhaseTwo();
                break;
            case 3:
                activePlayer.StartPhaseThree();
                break;
            case 4:
                activePlayer.StartPhaseFour();
                break;
            case 5:
                activePlayer.StartPhaseFive();
                break;
        }
    }
}