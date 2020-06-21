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

    public static int lastClickedIndex = -1;

    [Header("References")]
    public Sprite placeholder;

    private bool slotNulled;

    private Sprite lastClickedIcon;
    private string lastClickedCount;

    private void Update()
    {
        if (!InventorySystem.inventoryEnabled)
        {
            if (!slotNulled)
            {
                slotNulled = true;
                itemClicked = false;

                if (lastClickedIndex == InventorySystem.instance.inventorySlots.IndexOf(transform.parent.gameObject))
                {
                    if (lastClickedIcon != null)
                    {
                        transform.GetChild(0).GetComponent<Image>().sprite = lastClickedIcon;
                        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = lastClickedCount;
                    }
                }
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
                    lastClickedIndex = index;
                    lastClickedIcon = transform.GetChild(0).GetComponent<Image>().sprite;
                    lastClickedCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                    transform.GetChild(0).GetComponent<Image>().sprite = placeholder;
                    transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

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
                    lastClickedIndex = -1;
                }
                else if (InventorySystem.instance.inventory[index].itemData.itemID == clickedItem.itemData.itemID)
                {
                    if (InventorySystem.instance.inventory[index].itemCount + clickedItem.itemCount <= clickedItem.itemData.maxStack) 
                    {
                        InventorySystem.instance.inventory[clickedIndex] = new InventorySlot(null, 0);
                        InventorySystem.instance.inventory[index].itemCount += clickedItem.itemCount;
                        ClickedItem.instance.SetItem(new InventorySlot(null, 0));
                        lastClickedIndex = -1;
                        InventorySystem.instance.UpdateInventory();
                    }
                    else
                    {
                        int leftToFill = clickedItem.itemData.maxStack - InventorySystem.instance.inventory[index].itemCount;
                        InventorySystem.instance.inventory[clickedIndex].itemCount -= leftToFill;
                        InventorySystem.instance.inventory[index].itemCount += leftToFill;
                        ClickedItem.instance.SetItem(new InventorySlot(clickedItem.itemData, clickedItem.itemCount));
                        itemClicked = true;
                        InventorySystem.instance.UpdateInventory();

                        // Set last clicked index back to hidden
                        GameObject lastClicked = InventorySystem.instance.inventorySlots[lastClickedIndex];
                        lastClicked.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = placeholder;
                        lastClicked.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                    }
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
