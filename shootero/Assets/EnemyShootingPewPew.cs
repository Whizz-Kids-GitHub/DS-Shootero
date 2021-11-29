using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingPewPew : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private LineRenderer rend;
    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        StartCoroutine(Atack());
    }

    private void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        rend.SetPosition(0, firePoint.transform.position);
    }

    IEnumerator Atack()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));

        firePoint.transform.rotation = Quaternion.Euler(0, 0, -30);

        for (int i = 1; i < 5; i++)
        {
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.position + (-firePoint.transform.up * 15F));
            yield return new WaitForSeconds(1f);
            firePoint.transform.rotation = Quaternion.Euler(0, 0, firePoint.transform.rotation.z + 10);
            
        }
        firePoint.transform.rotation = Quaternion.Euler(0, 0, -20);

        var hit = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (-transform.up * 5F));

        StartCoroutine(Atack());
    }
}
