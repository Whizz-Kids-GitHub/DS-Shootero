using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBossBlack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletToSummonPortal;
    private int faze;
    [SerializeField]
    private GameObject firePoint;

    private void Start()
    {
        faze = 1;
        StartCoroutine(CastPortal());
    }
    private IEnumerator CastPortal()
    {
        while (true)
        {
            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90 + Random.Range(-20, 20), Vector3.forward);

            this.GetComponent<Animator>().SetTrigger("Roll");
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * -200);
            yield return new WaitForSeconds(1f);

            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            var min = GameObject.Find("EnemyMoveSpaceMin");
            var max = GameObject.Find("EnemyMoveSpaceMax");
            if (this.transform.position.x < min.transform.position.x || this.transform.position.x > max.transform.position.x)
            {
                var dir2 = PlayerMovement.Instance.gameObject.transform.position - transform.position;
                var angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle2 + 90 + Random.Range(-20, 20), Vector3.forward);

                this.GetComponent<Animator>().SetTrigger("Roll");
                this.GetComponent<Rigidbody2D>().AddForce(transform.up * -200);
                yield return new WaitForSeconds(1f);

                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            StartCoroutine(Shoot());
           
            yield return new WaitForSeconds(2f);
        }
    }
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private int recoilRange;
    private IEnumerator Shoot()
    {
        for (int i = 0; i < 25; i++)
        {
            var dir = PlayerMovement.Instance.gameObject.transform.position - firePoint.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis((angle + Random.Range(-recoilRange, recoilRange)) - 90f, Vector3.forward);

            GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            curBullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.up * 200);
            curBullet.GetComponent<BulletBurst>().damage = 4;

            yield return new WaitForSeconds(0.08f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stats = this.GetComponent<EnemyStatisctics>();

        if (collision.gameObject.GetComponent<BulletScript>())
        {
            if (stats.hp <= 0)
            {
                stats.hp = stats.startHp;
                faze += 1;
            }
        }
    }
}
