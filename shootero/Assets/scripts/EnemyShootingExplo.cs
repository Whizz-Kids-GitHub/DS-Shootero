using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingExplo : MonoBehaviour
{
    public GameObject bullet;
    private Transform player;
    public AudioSource sound;
    public GameObject firePoint;
    public float force;
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
        do
        {
            firePoint.transform.rotation = this.transform.rotation;
            GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            sound.Play();

            curBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * -force, ForceMode.Impulse);
            //curBullet.GetComponent<Rigidbody>().drag = Vector2.Distance(transform.position, player.position) * Time.deltaTime;
            yield return new WaitForSeconds(3f);
        } while (true);
    }
}
