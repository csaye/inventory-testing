using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotType
{
    public string itemID;
    public float itemCount;

    public InventorySlotType(string _itemID, float _itemCount)
    {
        itemID = _itemID;
        itemCount = _itemCount;
    }
}
