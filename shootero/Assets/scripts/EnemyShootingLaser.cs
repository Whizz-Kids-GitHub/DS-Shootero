using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer rend;
    [SerializeField]
    private GameObject firePoint;
    private Transform player;

    private float time;
    private float startTime;

    [SerializeField]
    private float speed = 0;

    public int damage;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startTime = 0.2f;
        time = startTime;
    }
    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.z = 0; // keep the direction strictly horizontal
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        var rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotation.z);

        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position + (-transform.up * 15F));

        var hit = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (-transform.up * 5F));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                rend.SetPosition(1, hit.point);

                if (time <= 0)
                {
                    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage += damage;
                    time = startTime;
                }
                else
                {
                    time -= Time.deltaTime;
                }
            }
        }
    }

}
