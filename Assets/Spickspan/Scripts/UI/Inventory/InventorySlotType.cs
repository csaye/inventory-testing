using UnityEngine;

[System.Serializable]
public class InventorySlotType
{
    public string itemID;
    public int itemCount;

    public InventorySlotType(string _itemID, int _itemCount)
    {
        itemID = _itemID;
        itemCount = _itemCount;
    }
}
