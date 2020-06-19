using UnityEngine;

[System.Serializable]
public class InventorySlotType
{
    public int itemID;
    public int itemCount;

    public InventorySlotType(int _itemID, int _itemCount)
    {
        itemID = _itemID;
        itemCount = _itemCount;
    }
}
