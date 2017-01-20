using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    Market m;
    public AbstractPlayer player1;
    public AbstractPlayer player2;

    public int turnCount;
    public int activePhase;
    public AbstractPlayer activePlayer;

    //Timer variables
    public Text TimerText;
    private float phaseDuration = 60;
    private float currentPhaseTime;

    //Turn Display variables
    public Text TurnDisplay;
    public Text PhaseDisplay;
    public Text PlayerDisplay;

	void Start () {
        //Initialise
        turnCount = 1;
        activePhase = 1;
        currentPhaseTime = 0;
        activePlayer = player1;

        //Get a list of all tiles to populate the market with
        List<Tile> allTiles = new List<Tile>();
        for(int i = 0; i < GameObject.Find("Tiles").transform.childCount; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i).GetComponent<Tile>());
        }

        //Create Market instance for this game
        m = new Market(5,6,3,5,3,5);
        m.Stock.SetTiles(allTiles);

        //Start the game
        activePlayer.StartPhase1();
        SetTurnText();
	}
	
	void Update () {
        currentPhaseTime += Time.deltaTime;
        TimerText.text = (int)(phaseDuration - currentPhaseTime) + "s";
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
            activePhase++;
            activePlayer = player1;
        }

        if (activePhase == 6)
            activePhase = 0;

        Debug.Log(activePlayer.Name);
        SetTurnText();
    }

    private void SetTurnText()
    {
        TurnDisplay.text = "Turn " + turnCount.ToString();
        PhaseDisplay.text = "Phase " + activePhase.ToString();
        //Debug.Log(activePlayer.Name);
        PlayerDisplay.text = activePlayer.Name;
    }
}
