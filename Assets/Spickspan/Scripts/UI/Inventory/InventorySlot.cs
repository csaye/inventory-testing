using UnityEngine;

public class InventorySlot
{
    public ItemScriptable itemData;
    public int itemCount;

    public InventorySlot(ItemScriptable _itemData, int _itemCount)
    {
        itemData = _itemData;
        itemCount = _itemCount;
    }    
}
