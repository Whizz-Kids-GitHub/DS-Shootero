using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBullet : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private GameObject explosionEffect;
    private Vector3 playerPlace;
    private Rigidbody rb;
    [SerializeField]
    private float slowFactor = 0.08f;

    private float time;
    private float startTime = 4f;

    public int damage = 5;

    private void Start()
    {
        time = startTime;
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

        if (Vector3.Distance(transform.position, playerPlace) <= 0.5)
        {
            GameObject explo = Instantiate(explosionEffect, transform.position, new Quaternion(0, 0, 0, 0));

            if (Vector3.Distance(transform.position, playerPlace) <= 0.5) player.GetComponent<PlayerMovement>().Damage += damage;

            Destroy(gameObject);
        }

        if (time <= 0)
        {
            GameObject explo = Instantiate(explosionEffect, transform.position, new Quaternion(0, 0, 0, 0));

            if (Vector3.Distance(transform.position, playerPlace) <= 0.5) player.GetComponent<PlayerMovement>().Damage += damage;

            Destroy(gameObject);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
