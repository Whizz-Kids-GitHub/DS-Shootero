using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 200f);
        }
    }
}
