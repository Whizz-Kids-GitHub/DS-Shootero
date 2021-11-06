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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Atack());
    }
    private void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private IEnumerator Atack()
    {
        do
        {
            for (int i = 0; i <= burstLength; i++)
            {
                Quaternion recoil = new Quaternion(0, 0, transform.rotation.z - Random.Range(-recoilRange, recoilRange), 0);
                GameObject curBullet = Instantiate(bullet, transform.position, recoil);
                Debug.Log(curBullet.transform.rotation);
                curBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                Debug.Log(curBullet.transform.rotation);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(timeBetweenBursts);

        } while (true);
    }
}
