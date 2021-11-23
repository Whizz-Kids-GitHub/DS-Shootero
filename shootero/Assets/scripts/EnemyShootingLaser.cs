using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    public LineRenderer rend;
    public GameObject firePoint;
    public LayerMask layerMask;
    private Transform player;

    private float time;
    private float startTime;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startTime = 0.2f;
        time = startTime;
    }

    private void LateUpdate()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

    }
    private void Update()
    {
        
        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position + (-transform.up * 15F));

        var hit = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (-transform.up * 5F));

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                rend.SetPosition(1, hit.point);

                if (time <= 0)
                {
                    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage += 1;
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
