using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBullet : MonoBehaviour
{
    private Transform player;
    public GameObject explosionEffect;
    private Vector3 playerPlace;
    private Rigidbody rb;
    public float slowFactor;
    bool tooNear;

    private void Start()
    {
        tooNear = true;
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPlace = player.position;
    }

    public void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerPlace) <= 1f && tooNear)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPlace, Mathf.Infinity);

        }
        if (Vector2.Distance(transform.position, playerPlace) <= 0.1f)
        {
            tooNear = false;
            for (int i = 0; i < 3; i++)
            {
                GameObject explo = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            if (Vector3.Distance(transform.position, player.transform.position) < 1f)
            {
                //Take damage
            }
            Destroy(gameObject);

        }
    }

}
