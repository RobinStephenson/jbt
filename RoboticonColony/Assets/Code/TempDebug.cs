using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDebug : MonoBehaviour {

    Inventory inv1;
    Inventory inv2;
	void Start () {
        inv1 = new Inventory(3, 7, 2, 5);
        inv2 = new Inventory(8, 2, 4, 7);

        Debug.Log(inv1.GetItemAmount(ItemType.Power));
        Debug.Log(inv2.GetItemAmount(ItemType.Power));

        inv2.TransferItem(ItemType.Power, 5, inv1);

        Debug.Log(inv1.GetItemAmount(ItemType.Power));
        Debug.Log(inv2.GetItemAmount(ItemType.Power));
    }
	
	void Update () {
		
	}
}
