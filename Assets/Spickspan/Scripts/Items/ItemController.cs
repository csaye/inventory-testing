using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsplit
{
    public class ItemController : MonoBehaviour
    {
        [Header("References")]
        public ItemScriptable itemScriptable;
        
        // private InventorySystem inventorySystem;

        // void Awake()
        // {
        //     if (inventorySystem == null)
        //     {
        //         inventorySystem = FindObjectOfType<InventorySystem>();
        //     }
        // }
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                List<InventorySlotType> inventory = InventorySystem.inventory;

                for (int i = 0; i < inventory.Count; i++)
                {
                    // If available slot in inventory
                    if (inventory[i].itemCount == 0)
                    {
                        inventory[i].itemID = itemScriptable.itemID;
                        inventory[i].itemCount++;
                        InventorySystem.inventory = inventory;
                        InventorySystem.instance.UpdateInventory();
                        Destroy(gameObject);
                        break;
                    }
                    else if (inventory[i].itemID == itemScriptable.itemID && inventory[i].itemCount < itemScriptable.maxStack)
                    {
                        inventory[i].itemCount++;
                        InventorySystem.inventory = inventory;
                        InventorySystem.instance.UpdateInventory();
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
