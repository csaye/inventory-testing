﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Spellsplit
{
    public class InventorySystem : MonoBehaviour
    {   
        public static List<InventorySlotType> inventory = new List<InventorySlotType>
        {
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0),
            new InventorySlotType(null, 0)
        };

        public static List<ItemScriptable> itemData = new List<ItemScriptable>()
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };

        public static InventorySystem instance;

        [Header("References")]
        public List<InventorySlotType> customInventory = new List<InventorySlotType>();
        public List<GameObject> inventorySlots;
        public CanvasGroup inventoryPopup;
        public GameObject slotPrefab;
        public Transform itemsParent;

        private bool inventoryEnabled = false;
        
        private float slotCount = 10;

        void Start()
        {
            for (int i = 0; i < customInventory.Count; i++)
            {
                inventory[i] = customInventory[i];
            }
            
            UpdateInventoryEnabled();
            UpdateInventory();

            if (instance == null)
            {
                instance = this;
            }
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
                
                itemData.Add(null);
                inventory.Add(new InventorySlotType(null, 0));
                inventorySlots.Add(slot);
            }
        }

        // Set item data and visual data according to inventory slot type data
        public void UpdateInventory()
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                // if (inventory[i].itemID != null && (i > itemData.Count || itemData[i] == null))
                if (inventory[i].itemID != null)
                {
                    if (itemData[i] == null)
                    {
                        itemData[i] = findData(inventory[i].itemID);
                    }

                    inventorySlots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = itemData[i].itemIcon;
                }

                if (inventory[i].itemCount > 1)
                {
                    inventorySlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = inventory[i].itemCount.ToString();
                }
                else
                {
                    inventorySlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "";
                }
            }
        }

        // Find item scriptable corresponding to item ID
        private ItemScriptable findData(string itemID)
        {
            try
            {
                return Resources.Load<ItemScriptable>("Scriptables/Items/" + itemID);
            }
            catch
            {
                Debug.LogError("Item data for: " + itemID + " could not be found.");
                return null;
            }
        }

        // private Sprite GetIcon(string itemID)
        // {
        //     return Resources.Load<Sprite>("Item Icons/" + itemID);
        // }

        // private void SetIcon(string itemID, int index)
        // {
        //     AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(itemID);

        //     handle.Completed += ctx =>
        //     {
        //         inventorySlots[index].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = handle.Result;
        //     };
        // }

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
