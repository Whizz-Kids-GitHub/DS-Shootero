using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletReverse : MonoBehaviour
{
    public GameObject boss;
    private void Start()
    {
        Invoke("Reverse", Random.Range(2.5f, 3f));
    }

    private void Reverse()
    {
        var dir = boss.GetComponent<EnemyShootingBoss>().firePoint.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        var rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 200f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
