using Spellsplit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickItem : MonoBehaviour
{
    public static bool itemClicked;

    public static InventorySlot clickedItem;

    public static int clickedIndex;

    private bool slotNulled;

    private void Update()
    {
        if (!InventorySystem.inventoryEnabled)
        {
            if (!slotNulled)
            {
                slotNulled = true;
                itemClicked = false;
                transform.GetChild(0).GetComponent<Image>().enabled = true;
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
            }
        }
        else
        {
            slotNulled = false;
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
                    transform.GetChild(0).GetComponent<Image>().enabled = false;
                    transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
                    clickedItem = InventorySystem.instance.inventory[index];
                    clickedIndex = index;
                    ClickedItem.instance.SetItem(clickedItem);
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
                    ClickedItem.instance.SetItem(new InventorySlot(null, 0));
                    InventorySystem.instance.UpdateInventory();
                }
                else if (InventorySystem.instance.inventory[index].itemData.itemID == clickedItem.itemData.itemID
                && InventorySystem.instance.inventory[index].itemCount + clickedItem.itemCount <= clickedItem.itemData.maxStack)
                {
                    InventorySystem.instance.inventory[clickedIndex] = new InventorySlot(null, 0);
                    InventorySystem.instance.inventory[index].itemCount += clickedItem.itemCount;
                    ClickedItem.instance.SetItem(new InventorySlot(null, 0));
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
