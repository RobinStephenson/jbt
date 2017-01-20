using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : AbstractPlayer {

    private int expectingInputForPhase;

	public override void Start () {
        
        base.Start();
	}
	
	void Update () {
        if (expectingInputForPhase == 0)
        {
            return;
        }
        else if(expectingInputForPhase == 1)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("fuck");
                EndPhase1(Tile.selectedTile);
            }
        }

    }

    public override void StartPhase1()
    {
        expectingInputForPhase = 1;
    }

    public override void StartPhase2() { }
    public override void StartPhase3() { }
    public override void StartPhase4() { }
    public override void StartPhase5() { }

    public override void EndPhase1(Tile t)
    {
        expectingInputForPhase = -1;
        base.EndPhase1(t);
    }
}
