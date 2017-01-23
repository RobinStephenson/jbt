using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{
    

    [Test]
    public void SuccessfulAddCustomisation()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile(2, 2, 2);
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create a new RoboticonCustomisation instance
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, null, ItemType.Ore, 10);

        //Apply customisation to Roboticon, which should not thrown an exception
        Assert.False(TestHelper.Throws(() => NewRoboticon.AddCustomisation(NewCustomisation), typeof(ArgumentException)));

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 2, NewRoboticon.Production()[ItemType.Ore]);
        Assert.AreEqual(NEEDS CHANGING TO TILE OUTPUT * 2, NewRoboticon.Production()[ItemType.Ore]);
        Assert.AreEqual(NewCustomisation, NewRoboticon.CurrentCustomisation);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    public void FailedAddCustomisation()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile(2, 2, 2);
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Create a new RoboticonCustomisation instance
        RoboticonCustomisation PrereqCustomisation = new RoboticonCustomisation("test", 2, null, ItemType.Ore, 10);
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test2", 3, PrereqCustomisation, ItemType.Ore, 10);

        //Check to ensure the class fails when a customisation cannot be applied
        Assert.True(TestHelper.Throws(() => NewRoboticon.AddCustomisation(NewCustomisation), typeof(ArgumentException)));
    }
}
