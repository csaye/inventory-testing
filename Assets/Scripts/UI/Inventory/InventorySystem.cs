using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static List<InventorySlotType> inventory;

    [Header("References")]
    public CanvasGroup inventoryPopup;
    public List<GameObject> inventorySlots;

    private bool inventoryEnabled = false;

    void Start()
    {
        UpdateInventoryEnabled();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            inventoryEnabled = !inventoryEnabled;
            UpdateInventoryEnabled();
        }
    }

    public static void UpdateInventory()
    {

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
