using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBossRed : MonoBehaviour
{
    public GameObject[] firePoints;
    private int faze;
    [SerializeField]
    private int burstLength;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject bullet2;
    [SerializeField]
    private GameObject particles;

    private void Start()
    {
        faze = 1;
        StartCoroutine(ShootFirePoints());
    }
    private IEnumerator ShootFirePoints()
    {
        while (faze == 1)
        {
            StartCoroutine(LerpFunction(new Quaternion(0, 0, transform.rotation.z + 360, 0), 2.9f));
            for (int a = 0; a < burstLength; a++)
            {
                for (int i = 0; i < firePoints.Length; i++)
                {
                    yield return new WaitForSeconds(0.2f);
                    Instantiate(missile, firePoints[i].transform.position, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(Faze2());

    }
    private IEnumerator Faze2()
    {
        while (faze == 2)
        {
            var curBullet = Instantiate(bullet2, transform.position, Quaternion.identity);

            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            curBullet.transform.rotation = Quaternion.AngleAxis(angle - 90 + Random.Range(-40, 40), Vector3.forward);
            var rb = curBullet.GetComponent<Rigidbody>();
            rb.AddForce(curBullet.transform.up * 300);

            yield return new WaitForSeconds(.5f);
        }
        StartCoroutine(Death());
    }
    private IEnumerator Death()
    {
        for (int i = 0; i < 15; i++)
        {
            Vector3 position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f),
               transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            Instantiate(particles, position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        var bigBoi = Instantiate(particles, transform.position, Quaternion.identity);
        bigBoi.transform.localScale = new Vector3(2, 2, 0);
        Destroy(gameObject);
    }
    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
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

