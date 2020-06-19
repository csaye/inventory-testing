using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarHighlight : MonoBehaviour
{
    public static int currentSlot;

    [Header("References")]
    public int slotNumber;

    private Color white = new Color(1, 1, 1);
    private Color gray = new Color(0.8f, 0.8f, 0.8f);

    private void Start()
    {
        if (currentSlot == slotNumber)
        {
            GetComponent<Image>().color = gray;
        }
    }

    // Set highlighted slot to slot clicked
    public void SetHighlight()
    {
        if (currentSlot == slotNumber)
        {
            return;
        }

        foreach (Transform child in transform.parent.parent)
        {
            child.GetChild(0).GetComponent<Image>().color = white;
        }

        currentSlot = slotNumber;
        GetComponent<Image>().color = gray;
    }
}
