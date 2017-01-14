using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonCustomisationTest
{
    [Test]
    public void SuccessfulCreateNewRoboticonCustomisation()
    {

        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual("test", NewCustomisation.Name);
        Assert.AreEqual(2, NewCustomisation.BonusMultiplier);
        Assert.AreEqual(ItemType.Ore, NewCustomisation.ResourceType);
        Assert.AreEqual(prelist, NewCustomisation.Prerequisite);
    }

    [Test]
    public void FailedCreateNewRoboticonCustomisation()
    {

        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Roboticon);

        //Check to ensure the class initialisation fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Roboticon);, typeof(ArgumentOutOfRangeException)));
    }

}
