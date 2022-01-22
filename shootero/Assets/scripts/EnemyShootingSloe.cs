using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingSloe : MonoBehaviour
{
    public GameObject bullet;
    private Transform player;
    public AudioSource sound;
    public GameObject firePoint;
    public float force;
    public GameObject[] particles;
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
        yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        do
        {
            for (int i = 0; i < particles.Length; i ++)
            {
                particles[i].GetComponent<ParticleSystem>().Play();
            }
            yield return new WaitForSeconds(3f);

            firePoint.transform.rotation = this.transform.rotation;
            GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            sound.Play();

            curBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * force, ForceMode.Impulse);

            yield return new WaitForSeconds(5f);
        } while (true);
    }
}
