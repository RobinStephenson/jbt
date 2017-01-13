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
        Tile current_tile = new Tile();
        Roboticon NewRoboticon = new Roboticon(current_tile);

        //Check if all resources have initial value set to zero and lists are empty.
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
    }

    [Test]
    public void SuccessfulCustomiseRoboticon()
    {
        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore);

        //Create an empty Roboticon instance
        Tile current_tile = new Tile();
        Roboticon NewRoboticon = new Roboticon(current_tile);

        //Customise the empty roboticon
        NewRoboticon.CustomiseRoboticon(NewCustomisation);

        //Check if all resources have the value they were set
        Assert.AreEqual(2, NewRoboticon.BonusProductionGetter(ItemType.Ore));
        Assert.AreEqual(2, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(prelist, NewRoboticon.CurrentCustomisations);
    }

}
