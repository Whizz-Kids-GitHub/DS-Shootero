using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    public LineRenderer rend;
    public GameObject firePoint;
    public LayerMask layerMask;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position + (-transform.up * 5F));

        var hit = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (-transform.up * 5F));

        if (hit.collider != null)
        {
            Debug.Log(hit.transform.name);
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("I HIT PLAYER!");
            }
        }

        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
        //hp -= 1;
        //}
    }
}
