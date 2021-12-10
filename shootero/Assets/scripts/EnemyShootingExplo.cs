using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingExplo : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private AudioSource sound;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private float force;

    public int damage;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Atack());
    }
    private void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle +90, Vector3.forward);
    }

    private IEnumerator Atack()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        do
        {
            firePoint.transform.rotation = this.transform.rotation;
            GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            sound.Play();

            curBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * -force, ForceMode.Impulse);
            curBullet.GetComponent<ExploBullet>().damage = damage;
            
            yield return new WaitForSeconds(Random.Range(3f, 5));

        } while (true);
    }
}
