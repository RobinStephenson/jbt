using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class InventoryTest
{
    [Test]
    public void CreateEmptyInventory()
    {
        //Create an empty inventory instance
        Inventory inv = new Inventory();

        //Check if all resources have initial value set to zero
        Assert.AreEqual(0, 0);
        Assert.AreEqual(0, inv.GetAmount(ItemType.Ore));
        Assert.AreEqual(0, inv.GetAmount(ItemType.Power));
        Assert.AreEqual(0, inv.GetAmount(ItemType.Roboticon));
    }

    [Test]
    public void CreatePopulatedInventory()
    {
        //Create a populated inventory instance
        Inventory inv = new Inventory(4, 3, 2, 1);

        //Check if all resources have the value they were set
        Assert.AreEqual(4, inv.GetAmount(ItemType.Money));
        Assert.AreEqual(3, inv.GetAmount(ItemType.Ore));
        Assert.AreEqual(2, inv.GetAmount(ItemType.Power));
        Assert.AreEqual(1, inv.GetAmount(ItemType.Roboticon));
    }

    [Test]
    public void AddItemToInventory()
    {
        //Create an empty inventory instance
        Inventory inv = new Inventory();

        //Prove that the inventory has no money
        Assert.AreEqual(0, inv.GetAmount(ItemType.Money));

        //Now add money
        inv.AddAmount(ItemType.Money, 6);

        //Check if the money has successfully been added
        Assert.AreEqual(6, inv.GetAmount(ItemType.Money));
    }

    [Test]
    public void FailedTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(1, 2, 3, 4);
        Inventory inv2 = new Inventory(10, 11, 12, 13);

        //Attempt to transfer 2 money from inv1 to inv2, even though inv1 only has 1 money
        bool successful = inv1.Transfer(ItemType.Money, 2, inv2);

        //Check if the transfer failed
        Assert.AreEqual(false, successful);

        //Check that both inventories still have the amount of money they started with
        Assert.AreEqual(1, inv1.GetAmount(ItemType.Money));
        Assert.AreEqual(10, inv2.GetAmount(ItemType.Money));
    }

    [Test]
    public void SuccessfulTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(3, 7, 2, 5);
        Inventory inv2 = new Inventory(8, 2, 4, 7);

        //Attempt to transfer 3 power from inv2 to inv1, which should work
        bool successful = inv2.Transfer(ItemType.Power, 3, inv1);

        //Check if the transfer was successful
        Assert.AreEqual(true, successful);

        //Check that the two inventories power amounts have changed due to the transaction
        Assert.AreEqual(5, inv1.GetAmount(ItemType.Power));
        Assert.AreEqual(1, inv2.GetAmount(ItemType.Power));
    }
}
