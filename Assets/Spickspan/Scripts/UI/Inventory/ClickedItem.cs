using Spellsplit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickedItem : MonoBehaviour
{
    public static ClickedItem instance;

    private GameObject player;

    private Camera cam;

    private Image image;

    private TextMeshProUGUI itemCount;

    private Sprite placeholder;

    private bool slotNulled;

    private void Start()
    {
        if (instance == null) instance = this;
        if (player == null) player = GameObject.FindWithTag("Player");
        if (cam == null) cam = Camera.main;
        if (image == null) image = transform.GetChild(0).GetComponent<Image>();
        if (itemCount == null) itemCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (placeholder == null) placeholder = transform.GetChild(0).GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (InventorySystem.inventoryEnabled)
        {
            slotNulled = false;
            transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else
        {
            if (!slotNulled)
            {
                SetItem(new InventorySlot(null, 0));
                slotNulled = true;
            }
        }
    }

    public void SetItem(InventorySlot item)
    {
        if (item.itemData != null)
        {
            image.sprite = item.itemData.itemIcon;
        }
        else
        {
            image.sprite = placeholder;
        }
        itemCount.text = item.itemCount > 1 ? item.itemCount.ToString() : "";
    }
}
