using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemScriptable itemScriptable;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            List<InventorySlotType> inventory = InventorySystem.inventory;

            for (int i = 0; i < inventory.Count - 1; i++)
            {
                // If available slot in inventory
                if (inventory[i].itemCount == 0)
                {
                    inventory[i].itemID = itemScriptable.itemID;
                    inventory[i].itemCount++;
                    InventorySystem.UpdateInventory();
                    Destroy(gameObject);
                }
                else if (inventory[i].itemID == itemScriptable.itemID && inventory[i].itemCount < itemScriptable.maxStack)
                {
                    inventory[i].itemCount++;
                    InventorySystem.UpdateInventory();
                    Destroy(gameObject);
                }
            }
        }
    }
}
