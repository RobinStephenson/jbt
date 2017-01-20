using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;

public class TileTest
{ 
    [Test]
    public void CreateTile()
    {
        //Create an empty inventory instance
        Tile tile = new Tile(5, 2, 1);

        //Check if all resources have correct values
        Assert.AreEqual(5, tile.Price);
        Assert.AreEqual(2, tile.Ore);
        Assert.AreEqual(1, tile.Power);
        //Assert.AreEqual(-1, tile.Owner.ID);
    }

    //Temporary Produce test
    [Test]    
    public void ProduceTest()
    {
        Assert.Fail();
    }
}
