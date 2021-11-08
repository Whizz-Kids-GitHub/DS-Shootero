using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    public LineRenderer rend;
    public GameObject firePoint;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up) * 50f);
        Debug.Log(hit.point);
        
        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, hit.point);
        //if (hit.collider.gameObject.CompareTag("Player"))
        //{
            //hp -= 1;
        //}
    }
}
