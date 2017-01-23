using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{
    [Test]
    public void BaseRoboticonProductionMultiplierTest()
    {
        Roboticon r = new Roboticon(new Tile(4, 6, 4, 5));

        Assert.AreEqual(1, r.ProductionMultiplier(ItemType.Ore));
        Assert.AreEqual(1, r.ProductionMultiplier(ItemType.Power));
    }
}
