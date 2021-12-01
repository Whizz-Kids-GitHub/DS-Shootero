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

    [SerializeField]
    private float duration = 0.2f;

    private float angle;
    private Vector3 dir;

    public int damage;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startTime = 0.2f;
        time = startTime;
    }
    private void Update()
    {
        dir = player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        StartCoroutine(Rotate());

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
    IEnumerator Rotate()
    {
        
        time = 0;
        Quaternion startRotation = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(angle , Vector3.forward), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
