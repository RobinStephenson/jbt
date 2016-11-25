using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractPlayer
{
    private List<Tile> ownedTiles;
    private Inventory inventory;

    public abstract void DoPhase(int phase);
}
