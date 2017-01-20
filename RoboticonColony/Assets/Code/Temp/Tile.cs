using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private static int TotalTiles;
    public static Tile selectedTile;

    public int Id;
    public int Cost;
    public int Ore;
    public int Power;

    public bool Bought;

    public void Start()
    {
        Id = TotalTiles++;
        Cost = Random.Range(5, 50);
        Ore = Random.Range(0, 5);
        Power = Random.Range(0, 5);
        Bought = false;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Tile))
            return false;

        return Id == ((Tile)obj).Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public void OnMouseEnter()
    {
        MakeSelected(this);
    }

    public void OnMouseExit()
    {
        ClearSelected();
    }

    private static void MakeSelected(Tile t)
    {
        ClearSelected();

        selectedTile = t;

        //Make tile yellow for selected
        Color tileColor = selectedTile.GetComponent<SpriteRenderer>().color;
        tileColor.b = 0;
        selectedTile.GetComponent<SpriteRenderer>().color = tileColor;
    }

    private static void ClearSelected()
    {
        if (selectedTile != null)
        {
            Color tileColor = selectedTile.GetComponent<SpriteRenderer>().color;

            //Set tile back to original color
            tileColor.b = 1;
            selectedTile.GetComponent<SpriteRenderer>().color = tileColor;
            selectedTile = null;
        }
    }
}
