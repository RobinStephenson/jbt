using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{

    //NEEDS UPDATING WHEN TILE IS MERGED
    [Test]
    public void CreateNewRoboticon()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);
    
    
        //Check if roboticon produces the correct amount of each resource and the initial values of Roboticon are set correctly
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 1, NewRoboticon.Production[ItemType.Ore]);
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 1, NewRoboticon.Production[ItemType.Power]);
        Assert.AreEqual(null, NewRoboticon.CurrentCustomisation);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    [Test]
    public void SuccessfulCreateNewRoboticon()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create an empty Roboticon instance
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create a new RoboticonCustomisation instance
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, null, ItemType.Ore, 10);

        //Apply customisation to Roboticon
        NewRoboticon.AddCustomisation(NewCustomisation);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 2, NewRoboticon.Production[ItemType.Ore]);
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 2, NewRoboticon.Production[ItemType.Power]);
        Assert.AreEqual(NewCustomisation, NewRoboticon.CurrentCustomisation);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    public void FailedCreateNewRoboticon()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create an empty Roboticon instance
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create a new RoboticonCustomisation instance
        RoboticonCustomisation PrereqCustomisation = new RoboticonCustomisation("test", 2, null, ItemType.Ore, 10);
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test2", 3, PrereqCustomisation, ItemType.Ore, 10);

        //Check to ensure the class fails when a customisation cannot be applied
        Assert.True(TestHelper.Throws(() => NewRoboticon.AddCustomisation(NewCustomisation), typeof(ArgumentException)));
    }


}
