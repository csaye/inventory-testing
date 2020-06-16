using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.Count - 1; i++)
            {
                // If available slot in inventory
                if (inventory[i])
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
