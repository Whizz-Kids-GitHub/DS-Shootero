using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletReverse : MonoBehaviour
{
    public GameObject boss;
    private bool can;
    [SerializeField]
    private GameObject particles;
    private void Start()
    {
        can = false;
        Invoke("Reverse", Random.Range(2.5f, 3f));
    }

    private void Reverse()
    {
        can = true;
        var dir = boss.GetComponent<EnemyShootingBoss>().firePoint.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 320f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && can)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            PlayerMovement.Instance.ProcessDamage(10);
            GameObject curParticles = Instantiate(particles, transform.position, Quaternion.identity);
            curParticles.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            Destroy(gameObject);
        }
    }
}
