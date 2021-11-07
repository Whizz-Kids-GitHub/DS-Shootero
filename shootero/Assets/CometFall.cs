using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public Rigidbody2D rigidbodyComet;
    float strength = 10;
    private void Start()
    {
        Vector3 v3Force = strength * transform.forward;
        Debug.Log(transform.forward);
        Debug.Log(v3Force);
        rigidbodyComet.AddForce(v3Force);
    }
}
