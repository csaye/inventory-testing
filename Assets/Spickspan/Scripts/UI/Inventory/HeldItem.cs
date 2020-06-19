using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldItem : MonoBehaviour
{
    [Header("References")]
    public Transform itemsParent;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sprite = itemsParent.GetChild(HotbarHighlight.currentSlot).GetChild(0).GetChild(0).GetComponent<Image>().sprite;
    }
}
