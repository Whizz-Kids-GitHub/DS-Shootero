using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer rend;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject sparkles;

    private float time;

    public int damage;
    private bool canRot;
    private bool canDmg;
    private void Awake()
    {
        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position);

        rend.endWidth = 0.02f;
        rend.startWidth = 0.02f;
    }
    private void Start()
    {
        canDmg = true;
        canRot = true;
        StartCoroutine(Shoot(1.2f));
    }
    private void Update()
    {
        if (canRot)
        {
            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            this.GetComponent<EnemyMovement>().canMove = true;
        }
        else
        {
            this.GetComponent<EnemyMovement>().canMove = false;
        }

        if (canRot == false && canDmg)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, firePoint.transform.position + (-firePoint.transform.up * 15f), out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("hit");
                    StartCoroutine(DealDamage(damage));
                }
            }
            canDmg = false;
        }
    }
    [SerializeField] private Color warn;
    [SerializeField] private Color norm;
    private IEnumerator Shoot(float duration)
    {
        yield return new WaitForSeconds(1.3f);
        rend.startColor = warn;
        rend.endColor = warn;

        time = 0;
        canRot = false;

        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.up * -15f);
        yield return new WaitForSeconds(1f);
        canDmg = true;
        sparkles.gameObject.SetActive(true);
        
        rend.startColor = norm;
        rend.endColor = norm;
        //rend.SetPosition(0, firePoint.transform.position);
        //rend.SetPosition(1, firePoint.transform.up * -15f);
        StartCoroutine(SizeUp());

        this.GetComponent<EnemyMovement>().canMove = false;
        yield return new WaitForSeconds(duration / 2);
        canDmg = true;

        yield return new WaitForSeconds(duration / 2);
        canRot = true;

        this.GetComponent<EnemyMovement>().canMove = false;

        sparkles.gameObject.SetActive(false);

        StartCoroutine(SizeDown());
        rend.SetPosition(0, firePoint.transform.position);
        rend.SetPosition(1, firePoint.transform.position);

        StartCoroutine(Shoot(2.2f));
        yield return null;
    }

    private IEnumerator DealDamage(int damage)
    {
        for (int i = 0; i < 20; i++)
        {
            PlayerMovement.Instance.ProcessDamage(damage);
            yield return new WaitForSeconds(0.08f);
        }
    }
    float duration = 0.15f;
    IEnumerator SizeUp()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.02f, 0), new Vector2(0.06f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.06f;
        rend.startWidth = 0.06f;
    }

    IEnumerator SizeDown()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.06f, 0), new Vector2(0.02f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.02f;
        rend.startWidth = 0.02f;
    }
}
