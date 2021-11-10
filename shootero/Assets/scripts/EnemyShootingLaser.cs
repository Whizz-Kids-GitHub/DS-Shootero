using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    public LineRenderer rend;
    public GameObject firePoint;
    public LayerMask layerMask;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.up))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, Vector2.up);
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, hit.point);
        }
        else
        {
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, transform.up *  5f);
        }

        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
        //hp -= 1;
        //}
    }
}
