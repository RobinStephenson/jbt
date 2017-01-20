using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour {

    public TurnController Controller;
    public int ID;

    string Name;
    Inventory Inv;

	public virtual void Start () {
        ID = 1;
        Name = "Mikeywalsh";
        Inv = new Inventory();
        Controller = GameObject.Find("Controller").GetComponent<TurnController>();
	}
	

    public abstract void StartPhase1();
    public abstract void StartPhase2();
    public abstract void StartPhase3();
    public abstract void StartPhase4();
    public abstract void StartPhase5();

    public virtual void EndPhase1(Tile t)
    {
        //if (!(Controller.activePhase != 1 || Controller.activePlayer.ID != ID))
            //return;

        t.Bought = true;
        t.GetComponent<SpriteRenderer>().sprite = Resources.Load("Player" + ID.ToString() + " Tile") as Sprite;

        Controller.NextPhase();
    }
}
