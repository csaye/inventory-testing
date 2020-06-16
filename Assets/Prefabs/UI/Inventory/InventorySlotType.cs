using UnityEngine;

[System.Serializable]
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
