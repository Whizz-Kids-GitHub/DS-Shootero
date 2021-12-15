using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocket : MonoBehaviour
{
    private Transform player;
    private GameObject max;
    private GameObject min;

    private Vector3 target;
    [SerializeField]
    private GameObject particles;

    private void Start()
    {

        max = GameObject.Find("EnemyMoveSpaceMax");
        min = GameObject.Find("EnemyMoveSpaceMin");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Invoke("Destro", 10);

        Target();
        Dash();
        
    }
    void Target()
    {
        target = new Vector3(Random.Range(min.transform.position.x, max.transform.position.x),
            Random.Range(min.transform.position.y, max.transform.position.y));
    }

    void Dash()
    {
        while (Vector3.Distance(transform.position, target) >= 0.3f)
        {
            var speed = 10f;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }

        var playerPlace = player.transform.position;

        var dir = playerPlace - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90 + Random.Range(-20, 20), Vector3.forward);

        var rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.up * 200f);
    }
    private void Destro()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var particle = Instantiate(particles, transform.position, Quaternion.identity);
            particle.transform.localScale = new Vector3(0.1f, 0.1f, 0);
            Destro();
        }
    }
}
