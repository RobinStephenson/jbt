using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{
    [Test]
    public void CreateNewRoboticonEmpty()
    {

        //Create an empty Roboticon instance
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    public void CreateNewRoboticon()
    {

        //Create a Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    [Test]
    public void SuccessfulGetBonusProduction()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check if all resources have the value they were set
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
    }

    public void FailedGetBonusProduction()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check to ensure the class fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => NewRoboticon.GetBonusProduction(ItemType.Roboticon), typeof(ArgumentException)));
    }

    public void SuccessfulSetBonusProduction()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);
        NewRoboticon.SetBonusProduction(ItemType.Ore);

        //Check if all resources have the value they were set
        Assert.AreEqual(2, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
    }

    public void FailedSetBonusProduction()
    {
        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check to ensure the class fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => NewRoboticon.SetBonusProduction(ItemType.Roboticon), typeof(ArgumentException)));
    }
}
