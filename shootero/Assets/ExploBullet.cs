using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBullet : MonoBehaviour
{
    private Transform player;
    public GameObject explosionEffect;
    private Vector3 playerPlace;
    private Rigidbody rb;
    public float slowFactor = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPlace = player.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerPlace) < 3f)
        {
            float x = rb.velocity.x;
            float y = rb.velocity.y;
            rb.velocity = new Vector3(x -= slowFactor * Time.deltaTime, y -= slowFactor * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject explo = Instantiate(explosionEffect, transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
}
