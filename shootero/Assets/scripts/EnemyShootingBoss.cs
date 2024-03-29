using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBoss : MonoBehaviour
{
    [Header("Burst Faze")]
    #region burst
    public GameObject bullet;
    public int burstLength;
    public float timeBetweenBursts;
    public float force;
    public float recoilRange;
    public GameObject firePoint;
    public AudioSource sound;
    public int damage = 1;
    private EnemyMovement movement;
    private Transform player;

    #endregion
    #region saw
    [SerializeField]
    private GameObject sawBlade;
    [SerializeField]
    private GameObject rocket;
    [SerializeField]
    private GameObject firePointRocket;
    [SerializeField]
    private Color angryBoi;
    [SerializeField]
    private GameObject particles;
    [SerializeField]
    private GameObject[] laserPoints;
    [SerializeField]
    private GameObject bulletRev;
    [SerializeField]
    private GameObject enemyGuard;
    public bool rotate;
    #endregion

    [Space(15)]
    [Header("Faze options")]
    [SerializeField]
    private int faze;
    private void Start()
    {
        faze = 1;
        rotate = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GetComponent<EnemyMovement>();
        StartCoroutine(Atack1());
    }

    private void Update()
    {
        if (rotate)
        {
            var dir = player.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }

        sawBlade.transform.Rotate(Vector3.forward * -180 * Time.deltaTime);

        //if (Input.GetKeyUp("o"))
        //{
        //    StartCoroutine(Faze3_1());
        //    faze = 3;
        //}
        //if (Input.GetKeyUp("p"))
        //{
        //    StartCoroutine(Atack2());
        //    faze = 2;
        //}
        //if (Input.GetKeyUp("["))
        //{
        //    StartCoroutine(Faze4());
        //    faze = 4;
        //}
    }

    private IEnumerator Atack1()
    {
        if (faze == 1)
        {
            yield return new WaitForSeconds(0.1f);

            movement.canMove = false;
            firePoint.transform.rotation = Quaternion.Euler(0, 0, 45);

            var anim = firePoint.GetComponent<Animator>();
            anim.SetTrigger("rot");

            for (int i = 0; i <= burstLength; i++)
            {
                GameObject curBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                sound.Play();

                curBullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.up * force);
                curBullet.GetComponent<BulletBurst>().damage = damage;

                if (i == 0) Destroy(curBullet);

                yield return new WaitForSeconds(0.08f);
            }

            movement.canMove = true;

            yield return new WaitForSeconds(timeBetweenBursts);
            StartCoroutine(Atack1());
        }
    }
    private IEnumerator Atack2()
    {
        movement.canMove = false;
        do
        {
            var playerPlace = player.transform.position;

            var dir = playerPlace - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            castLine(transform.position, transform.up * -20);
            yield return new WaitForSeconds(0.2f);

            float time = 0;
            float duration = 0.4f;
            Vector3 startPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, playerPlace, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = playerPlace;

            time = 0;
            startPosition = transform.position;

            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, Vector3.zero, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.position = Vector3.zero;

            StartCoroutine(RespRockets(4, rocket));

        } while (faze == 2);
    }
    private IEnumerator Faze3_1()
    {
        movement.canMove = false;
        #region cosmetics
        float time, duration;
        Vector3 startPosition;
        time = 0;
        duration = 2f;
        startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, Vector3.zero, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector3.zero;

        var dialogue = GameObject.Find("DialogueSystem");
        dialogue.GetComponent<DialogueSystem>().StartDialogue(1);
        yield return new WaitForSeconds(2);

        GetComponent<SpriteRenderer>().color = angryBoi;
        sawBlade.GetComponent<SpriteRenderer>().color = angryBoi;
        Instantiate(particles, transform.position, Quaternion.identity);

        time = 0;
        duration = 2f;
        startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, new Vector3(0, 5, 0), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(0, 5, 0);
        #endregion
        StartCoroutine(Faze3_2());
        do
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < laserPoints.Length; i++)
            {
                rotate = false;
                laserPoints[i].GetComponent<LaserPoint>().StartSequence(laserPoints[i].transform.position, 0.5f);
                var we = this.gameObject;
                laserPoints[i].GetComponent<LaserPoint>().boss = we;

            }

        } while (faze == 3 || faze == 4);
    }
    private IEnumerator Faze3_2()
    {
        do
        {
            for (int i = 0; i < 13; i++)
            {
                var curBullet = Instantiate(bulletRev, firePoint.transform.position, Quaternion.identity);
                var dir = player.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                curBullet.transform.rotation = Quaternion.AngleAxis(angle + 180 + 90 + Random.Range(-40, 40), Vector3.forward);
                var boss = this.gameObject;

                curBullet.GetComponent<BuletReverse>().boss = boss;

                var rb = curBullet.GetComponent<Rigidbody2D>();
                rb.AddForce(curBullet.transform.up * 350f);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(6);
        } while (faze == 3);

    }
    private IEnumerator Faze4()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 2; i++)
        {
            if (faze == 4)
            {
                Instantiate(enemyGuard, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(15f);
            }
        }
    }
    private IEnumerator Death()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Vector3 position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f),
                transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            Instantiate(particles, position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator RespRockets(int amount, GameObject bullet)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject curBubllet = Instantiate(bullet, firePointRocket.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
    #region lines
    void castLine(Vector3 startPos, Vector3 endPos)
    {
        keepLinesStart();
        var rend = GetComponent<LineRenderer>();
        rend.SetPosition(0, startPos);
        rend.SetPosition(1, endPos);

        Invoke("StopLine", 1);
    }

    void keepLinesStart()
    {
        float time, duration;
        time = 0;
        duration = 0.8f;

        while (time < duration)
        {
            var rend = GetComponent<LineRenderer>();
            rend.SetPosition(0, transform.position);
            time += Time.deltaTime;
        }
    }

    void StopLine()
    {
        var rend = GetComponent<LineRenderer>();
        rend.SetPosition(0, transform.position);
        rend.SetPosition(1, transform.position);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stats = this.GetComponent<EnemyStatisctics>();

        if (collision.gameObject.GetComponent<BulletScript>())
        {
            if (stats.hp <= 0)
            {
                stats.hp = stats.startHp;
                faze += 1;
                if (faze == 2)
                {
                    StartCoroutine(Atack2());
                }
                if (faze == 3)
                {
                    StartCoroutine(Faze3_1());
                }
                if (faze == 4)
                {
                    StartCoroutine(Faze4());
                }
                if (faze == 5)
                {
                    StartCoroutine(Death());
                }
            }
        }
        if (collision.gameObject.CompareTag("2D"))
        {
            PlayerMovement.Instance.ProcessDamage(20);
        }
    }
}
