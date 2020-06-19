using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spellsplit
{
    public class InventorySystem : MonoBehaviour
    {
        public List<InventorySlot> inventory = new List<InventorySlot>();

        // public List<ItemScriptable> itemData = new List<ItemScriptable>()
        // {
        //     null,
        //     null,
        //     null,
        //     null,
        //     null,
        //     null,
        //     null,
        //     null,
        //     null,
        //     null
        // };

        // public List<int> itemCount = new List<int>
        // {
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0
        // };

        public static InventorySystem instance;

        [Header("References")]
        public List<GameObject> inventorySlots;
        public CanvasGroup inventoryPopup;
        public Sprite placeholder;
        public GameObject slotPrefab;
        public Transform itemsParent;

        private bool inventoryEnabled = false;
        
        private float slotCount = 10;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }

            // Begin with ten empty inventory slots in hotbar
            for (int i = 0; i < 10; i++)
            {
                inventory.Add(new InventorySlot(null, 0));
            }

            UpdateInventoryEnabled();
            UpdateInventory();
        }

        void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                inventoryEnabled = !inventoryEnabled;
                UpdateInventoryEnabled();
            }

            if (Input.GetKeyDown("u"))
            {
                Unlock();
            }
        }

        private void Unlock()
        {
            if (slotCount >= 40)
            {
                return;
            }

            slotCount += 10;

            for (int i = 0; i < 10; i++)
            {
                GameObject slot = Instantiate(slotPrefab, itemsParent);
                inventorySlots.Add(slot);
                
                inventory.Add(new InventorySlot(null, 0));
            }
        }

        // Attempt to add the item to the inventory
        public bool AddItem(ItemScriptable item)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                // If slot is empty
                if (inventory[i].itemCount == 0)
                {
                    inventory[i] = new InventorySlot(item, 1);
                    return true;
                }

                // If stacking to slot
                if (inventory[i].itemData.itemID == item.itemID && inventory[i].itemCount < item.maxStack)
                {
                    inventory[i].itemCount++;
                    return true;
                }
            }

            return false;
        }

        // Update inventory based on item data and inventory data
        public void UpdateInventory()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Transform slotTransform = inventorySlots[i].transform.GetChild(0).transform;

                // Set slot icon
                if (inventory[i].itemCount != 0)
                {
                    slotTransform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemData.itemIcon;
                }
                else
                {
                    slotTransform.GetChild(0).GetComponent<Image>().sprite = placeholder;
                }

                // Set slot number
                if (inventory[i].itemCount > 1)
                {
                    slotTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = inventory[i].itemCount.ToString();
                }
                else
                {
                    slotTransform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
                }
            }
        }

        private void UpdateInventoryEnabled()
        {
            inventoryPopup.interactable = inventoryEnabled;
            inventoryPopup.blocksRaycasts = inventoryEnabled;

            // If the inventory is enabled
            if (inventoryEnabled)
            {
                inventoryPopup.alpha = 1;
            }
            // If the inventory is disabled
            else
            {
                inventoryPopup.alpha = 0;
            }
        }
    }
}
