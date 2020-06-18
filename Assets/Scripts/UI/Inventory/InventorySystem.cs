using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("References")]
    public CanvasGroup inventoryPopup;
    public List<InventorySlotType> customInventory = new List<InventorySlotType>();
    public List<GameObject> inventorySlots;

    private bool inventoryEnabled = false;

    void Start()
    {
        for (int i = 0; i < customInventory.Count; i++)
        {
            inventory[i] = customInventory[i];
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
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventory[i].itemID != null)
            {
                inventorySlots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = GetIcon(inventory[i].itemID);
            }
            if (inventory[i].itemCount > 1)
            {
                inventorySlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = inventory[i].itemCount.ToString();
            }
        }
    }

    private Sprite GetIcon(string itemID)
    {
        return Resources.Load<Sprite>("Items/" + itemID);
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
