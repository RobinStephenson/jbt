using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using NUnit.Framework;
using NUnit;

public class TileTest
{ 
    [Test]
    public void CreateTile()
    {
        //Create an empty inventory instance
        Tile tile = new Tile(2, 5, 2, 1);

        //Check if all resources have correct values
        Assert.AreEqual(5, tile.Price);
        Assert.AreEqual(2, tile.Ore);
        Assert.AreEqual(1, tile.Power);
        Assert.AreEqual(null, tile.Owner);
    }

    [Test]    
    public void ProduceTest()
    {
        //Create tile and assign roboticon to tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        tile.InstallRoboticon(player);
        Dictionary<ItemType, int> production = tile.Produce();
       
        //Check if the correct values for ore and power are returned
        Assert.AreEqual(production[ItemType.Ore], 3);
        Assert.AreEqual(production[ItemType.Power], 2);
    }
    
    [Test]
    public void SuccessfulInstallRoboticon()
    {
        //Creates a new tile and player and assigns a roboticon to the tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        tile.InstallRoboticon(player);

        //Checks that a roboticon is assigned to the tile
        Assert.AreNotEqual(null, tile.InstalledRoboticon);
    }

    [Test]
    public void InstallRoboticonMultipleRoboticonsTest()
    {
        //Creates a new tile and player and assigns a roboticon to the tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        tile.InstallRoboticon(player);

        //Checks that a roboticon can't be installed if there is already one installed on the tile
        Assert.Throws<RoboticonAlreadyInstalledException>(() => tile.InstallRoboticon(player));
    }

    [Test]
    public void InstallRoboticonNoRoboticonsTest()
    {
        //Creates a new tile and player with no roboticons in their inventory
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 0);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());

        //Checks that a roboticon is not installed if the player has no roboticons
        Assert.Throws<ArgumentOutOfRangeException>(() => tile.InstallRoboticon(player));
    }

}
