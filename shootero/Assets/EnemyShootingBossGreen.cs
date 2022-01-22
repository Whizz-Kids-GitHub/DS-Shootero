using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBossGreen : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject dronesPoint;
    [SerializeField]
    private GameObject drone1;
    [SerializeField]
    private GameObject drone2;
    [SerializeField]
    private GameObject sparkles;
    [SerializeField]
    private int faze;
    private float time;
    private float duration;
    private LineRenderer rend;
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private LayerMask _layerMask;
    private void Start()
    {
        duration = .49f;
        rend = GetComponent<LineRenderer>();

        faze = 1;
        sparkles.GetComponent<ParticleSystem>().Stop();

        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position);

        StartCoroutine(Faze1());
    }
    private IEnumerator SpawnDrones()
    {
        while (faze == 1)
        {
            Instantiate(drone1, dronesPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
    private IEnumerator Faze1()
    {
        while (faze == 1)
        {
            StartCoroutine(SpawnDrones());

            sparkles.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(2);

            sparkles.GetComponent<ParticleSystem>().Stop();

            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.up * 20f);

            StartCoroutine(Hit());
            StartCoroutine(SizeUp());

            yield return new WaitForSeconds(1f);

            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.position);

        }
        StartCoroutine(Faze2());
    }

    private IEnumerator Faze2()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(drone2, dronesPoint.transform.position, Quaternion.identity);
        }
        while (faze == 2)
        {
            sparkles.GetComponent<ParticleSystem>().Play();

            yield return new WaitForSeconds(2);

            sparkles.GetComponent<ParticleSystem>().Stop();

            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.up * 20f);
            StartCoroutine(Hit());
            StartCoroutine(SizeUp());
            yield return new WaitForSeconds(1f);

            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.position);

        }
    }

    private IEnumerator Hit()
    {
        var hit2 = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (firePoint.transform.up * 20f), _layerMask);

        while (hit2.collider.CompareTag("Enemy") || hit2.collider.CompareTag("Shield"))
        {
            var particlesPosition = hit2.collider.gameObject.transform.position;

            StartCoroutine(Particles(particlesPosition));
            Destroy(hit2.collider.gameObject);

            hit2 = Physics2D.Linecast(firePoint.transform.position, firePoint.transform.position + (firePoint.transform.up * 20f));
        }

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.position + (-transform.up * 15f), out hit, Mathf.Infinity);
        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Player"))
            {
                rend.SetPosition(1, hit.point);
                for (int i = 0; i < 9; i++)
                {
                    var curParticles = Instantiate(particles, hit.point, Quaternion.identity);

                    curParticles.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                    PlayerMovement.Instance.ProcessDamage(10);
                    yield return new WaitForSeconds(0.08f);
                }

            }
        }
    }
    private IEnumerator Particles(Vector3 particlesPosition)
    {
        for (int i = 0; i < 4; i++)
        {
            var curParticles = Instantiate(particles, particlesPosition, Quaternion.identity);

            curParticles.transform.localScale = new Vector3(0.3f, 0.3f, 0);
            yield return new WaitForSeconds(0.08f);
        }
    }
    IEnumerator SizeUp()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.05f, 0), new Vector2(0.5f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.5f;
        rend.startWidth = 0.5f;
        StartCoroutine(SizeDown());
    }

    IEnumerator SizeDown()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.5f, 0), new Vector2(0.0f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.00f;
        rend.startWidth = 0.00f;
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
