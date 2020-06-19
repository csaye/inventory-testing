using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 3, Input.GetAxis("Vertical") * 3);
    }
}
