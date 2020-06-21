using Spellsplit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    public static bool itemClicked;

    public static InventorySlot clickedItem;

    public static int clickedIndex;

    private void Update()
    {
        if (!InventorySystem.inventoryEnabled)
        {
            itemClicked = false;
        }
    }

    public void ClickItemSlot()
    {
        if (InventorySystem.inventoryEnabled)
        {
            itemClicked = !itemClicked;

            if (itemClicked)
            {
                int index = InventorySystem.instance.inventorySlots.IndexOf(transform.parent.gameObject);

                // Try to take item from current slot and if no item reset to no clicked item state
                if (InventorySystem.instance.inventory[index].itemCount > 0)
                {
                    clickedIndex = index;
                    clickedItem = InventorySystem.instance.inventory[index];
                }
                else
                {
                    itemClicked = false;
                }

                return;
            }
            else
            {
                int index = InventorySystem.instance.inventorySlots.IndexOf(transform.parent.gameObject);
                
                // Try to put item into slot and if cannot reset back to item clicked state
                if (InventorySystem.instance.inventory[index].itemCount <= 0)
                {
                    InventorySystem.instance.inventory[clickedIndex] = new InventorySlot(null, 0);
                    InventorySystem.instance.inventory[index] = clickedItem;
                    InventorySystem.instance.UpdateInventory();
                }
                else if (InventorySystem.instance.inventory[index].itemData.itemID == clickedItem.itemData.itemID
                && InventorySystem.instance.inventory[index].itemCount + clickedItem.itemCount <= clickedItem.itemData.maxStack)
                {
                    InventorySystem.instance.inventory[clickedIndex] = new InventorySlot(null, 0);
                    InventorySystem.instance.inventory[index].itemCount += clickedItem.itemCount;
                    InventorySystem.instance.UpdateInventory();
                }
                else
                {
                    itemClicked = true;
                }

                return;
            }
        }
    }
}
