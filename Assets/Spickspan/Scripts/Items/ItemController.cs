using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsplit
{
    public class ItemController : MonoBehaviour
    {
        [Header("References")]
        public ItemScriptable itemScriptable;
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (InventorySystem.instance.AddItem(itemScriptable))
                {
                    InventorySystem.instance.UpdateInventory();
                    Destroy(gameObject);
                }
            }
        }
    }
}
