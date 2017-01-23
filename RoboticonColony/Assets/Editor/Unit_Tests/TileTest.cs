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
        playerInv.AddTile(tile);
        tile.InstallRoboticon(player);

        //Check that the roboticon is installed on the tile
        Assert.AreEqual(1, player.InstalledRoboticonCount);
        //Checks that a roboticon is assigned to the tile
        Assert.AreNotEqual(null, tile.InstalledRoboticon);
    }

    [Test]
    public void InstallMultipleRoboticonsTest()
    {
        //Creates a new tile and player and assigns a roboticon to the tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerInv.AddTile(tile);
        tile.InstallRoboticon(player);

        //Checks that a roboticon can't be installed if there is already one installed on the tile
        Assert.Throws<RoboticonAlreadyInstalledException>(() => tile.InstallRoboticon(player));
    }

    [Test]
    public void InstallRoboticonWithNoneInInventoryTest()
    {
        //Creates a new tile and player with no roboticons in their inventory
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 0);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerInv.AddTile(tile);
        
        //Checks that a roboticon is not installed if the player has no roboticons
        Assert.Throws<ArgumentOutOfRangeException>(() => tile.InstallRoboticon(player));
    }

    [Test]
    public void SuccessfulRemoveRoboticon()
    {
        //Creates a new tile and player and assigns a roboticon to the tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerInv.AddTile(tile);
        tile.InstallRoboticon(player);

        //Removes the roboticon from the tile
        tile.RemoveRoboticon();

        //Checks that the roboticon is cleared from the tile
        Assert.AreEqual(null, tile.InstalledRoboticon);
        Assert.AreEqual(0, player.InstalledRoboticonCount);
    }

    [Test]
    public void RemoveRoboticonEmptyTileTest()
    {
        //Creates a new tile and player
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerInv.AddTile(tile);

        //Checks that an exception is thrown if RemoveRoboticon is called on a tile with no roboticon
        Assert.Throws<InvalidOperationException>(() => tile.RemoveRoboticon());
    }
}
