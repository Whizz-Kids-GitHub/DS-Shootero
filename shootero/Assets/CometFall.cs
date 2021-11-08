using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public Rigidbody2D rigidbodyComet;
    float strength = 900;
    private void Start()
    {
        rigidbodyComet.AddForce(transform.right * strength);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DestroyDetect")
        {
            Destroy(this.gameObject);
        }
    }

}
