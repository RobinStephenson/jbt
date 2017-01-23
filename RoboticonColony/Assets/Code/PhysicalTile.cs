using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to represent a tile gameobject in the scene
/// </summary>
public class PhysicalTile : MonoBehaviour {

    public GameObject AttachedRoboticonObject;
    private static int TotalTiles;
    public static PhysicalTile selectedTile;
    public static bool canSelect;

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
        if (!canSelect)
            return;
        MakeSelected(this);
    }

    public void OnMouseExit()
    {
        if (!canSelect)
            return;
        ClearSelected();
    }

    public void SetSprite(Sprite s)
    {
        GetComponent<SpriteRenderer>().sprite = s;
    }

    public void SetAttachedRoboticon(string path)
    {
        AttachedRoboticonObject = Instantiate(Resources.Load(path), transform.position, Quaternion.identity) as GameObject;
        AttachedRoboticonObject.transform.parent = transform;
    }

    public void RemoveAttachedRoboticon()
    {
        Destroy(AttachedRoboticonObject);
    }

    public void SetAttachedRoboticonCustomisation(string path)
    {
        if(AttachedRoboticonObject == null)
        {
            return;
        }
        AttachedRoboticonObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
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

    public static void ClearSelected()
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
