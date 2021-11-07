using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public Rigidbody2D rigidbodyComet;
    void Update()
    {
        rigidbodyComet.AddForce(new Vector2(0f, -10f));
    }
}
