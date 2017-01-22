using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to represent a tile gameobject in the scene
/// </summary>
public class PhysicalTile : MonoBehaviour {

    private static int TotalTiles;
    public static PhysicalTile selectedTile;

    public int Id;
    public Tile ContainedTile;

    public void Awake()
    {
        Id = TotalTiles;
        ContainedTile = new Tile(Id, Random.Range(5, 50), Random.Range(5, 50), Random.Range(5, 50));
        TotalTiles++;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is PhysicalTile))
            return false;

        return Id == ((PhysicalTile)obj).Id;
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

    public void SetSprite(Sprite s)
    {
        GetComponent<SpriteRenderer>().sprite = s;
    }

    private static void MakeSelected(PhysicalTile t)
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

    public int Price
    {
        get { return ContainedTile.Price; }
    }

    public int Ore
    {
        get { return ContainedTile.Ore; }
    }

    public int Power
    {
        get { return ContainedTile.Power; }
    }
}
