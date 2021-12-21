using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingPewPew : MonoBehaviour
{
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private LineRenderer rend;
    private Transform player;
    [SerializeField]
    private float cooldownbtwshots;

    public int damage = 3;

    private float time;
    private float duration = 0.15f;

    private float time2;
    private float startTime = 1f;

    private EnemyMovement movement;

    private void Start()
    {
        time2 = startTime;
        player = GameObject.Find("Player").GetComponent<Transform>();
        movement = GetComponent<EnemyMovement>();
        StartCoroutine(Atack());
    }

    public void Update()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        rend.SetPosition(0, firePoint.transform.position);

        if (movement.canMove)
        {
            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.position);
        }
    }

    private IEnumerator Atack()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        movement.canMove = false;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
        firePoint.transform.localRotation = Quaternion.Euler(0, 0, -10);
        for (int i = 0; i < 5; i++)
        {
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, -10 + i * 5 + -180);

            rend.SetPosition(0, firePoint.transform.position);
            rend.SetPosition(1, firePoint.transform.position + (-firePoint.transform.up * 15f));

            #region hit
            RaycastHit hit;
            Physics.Raycast(transform.position, firePoint.transform.position + (-firePoint.transform.up * 15f), out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("Player"))
                {
                    rend.SetPosition(1, hit.point);
                    Debug.Log("hit");
                    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage += 10;
                }
            }
            #endregion 

            StartCoroutine(SizeUp());

            yield return new WaitForSeconds(cooldownbtwshots);
        }

        firePoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(Atack());
        movement.canMove = true;
    }

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
        StartCoroutine(SizeDown());
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
