using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    private int Id;

    public Tile(int id)
    {
        Id = id;
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public override bool Equals(object obj)
    {
        if(!(obj is Tile))
        {
            return false;
        }
        else
        {
            return Id == ((Tile)obj).Id;
        }
    }

    public int GetCost()
    {
        return 1;
    }
}
