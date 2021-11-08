using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public Rigidbody2D rigidbodyComet;
    float strength = 50;
    private void Start()
    {
        rigidbodyComet.AddRelativeForce(new Vector2(0, strength * Time.deltaTime));
    }
}
