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
        RaycastHit2D hit;
        Physics2D.Raycast(transform.position, transform.right * 50, Mathf.Infinity, layerMask);
        if (!(hit = Physics2D.Raycast(transform.position, transform.right * 50, Mathf.Infinity, layerMask)))
        {
            hit.point = -transform.up * 20;
        }

        Debug.Log(hit.point);

        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, hit.point);
        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
        //hp -= 1;
        //}
    }
}
