using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{
    [Test]
    public void CreateNewRoboticon()
    {

        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
    }

    [Test]
    public void SuccessfulCustomise()
    {
        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore);

        //Create an empty Roboticon instance
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Customise the empty roboticon
        NewRoboticon.CustomiseRoboticon(NewCustomisation);

        //Check if all resources have the value they were set
        Assert.AreEqual(2, NewRoboticon.GetBonusProduction(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.GetBonusProduction(ItemType.Power));
        Assert.AreEqual(prelist, NewRoboticon.CurrentCustomisations);
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
}
