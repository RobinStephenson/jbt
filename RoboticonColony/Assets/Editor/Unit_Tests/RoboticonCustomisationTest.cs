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
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, null, ItemType.Ore, 10);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual("test", NewCustomisation.Name);
        Assert.AreEqual(2, NewCustomisation.BonusMultiplier);
        Assert.AreEqual(ItemType.Ore, NewCustomisation.ResourceType);
        Assert.AreEqual(null, NewCustomisation.Prerequisites);
        Assert.AreEqual(10, NewCustomisation.Price);
    }

    [Test]
    public void FailedCreateNewRoboticonCustomisation()
    {

        //Check to ensure the class fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => new RoboticonCustomisation("Test",2,null,ItemType.Roboticon,10), typeof(ArgumentException)));
    }

}
