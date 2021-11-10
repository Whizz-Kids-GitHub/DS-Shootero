using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBurst : MonoBehaviour
{
    private Transform player;
    public GameObject bullet;
    public int burstLength;
    public float timeBetweenBursts;
    public float force;
    public float recoilRange;
    public GameObject firePoint;
    public AudioSource sound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Atack());
    }
    private void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private IEnumerator Atack()
    {
        do
        {
            for (int i = 0; i <= burstLength; i++)
            {
                var dir = player.position - firePoint.transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                firePoint.transform.rotation = Quaternion.AngleAxis((angle + Random.Range(-recoilRange, recoilRange)) - 90f, Vector3.forward);
                
                GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                sound.Play();

                curBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * force);
                //curBullet.GetComponent<Rigidbody2D>().AddForceAtPosition(transform.up * force, player.position);

                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(timeBetweenBursts);

        } while (true);
    }
}
