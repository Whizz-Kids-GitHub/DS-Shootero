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

        if (Physics2D.Raycast(transform.position, Vector2.up * 111))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, Vector2.up);
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, hit.point);
        }
        else
        {
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, Vector2.up *  5f);
        }

        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
        //hp -= 1;
        //}
    }
}
