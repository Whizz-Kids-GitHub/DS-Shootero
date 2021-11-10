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
        RaycastHit2D Thehit;
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up)))
        {
            Thehit = Physics2D.Raycast(firePoint.transform.position, firePoint.transform.up);
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, Thehit.point);
        }
        else
        {
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, transform.TransformDirection(Vector2.up) * 10f);
        }

        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
        //hp -= 1;
        //}
    }
}
