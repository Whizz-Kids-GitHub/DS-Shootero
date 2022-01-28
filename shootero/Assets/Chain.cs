using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject mummy;
    [SerializeField]
    private bool canStop;
    [SerializeField]
    private GameObject particles;
    private Vector3 playerPlace;
    private void Start()
    {
        playerPlace = PlayerMovement.Instance.transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, mummy.GetComponent<EnemyShootingBossBlue>().firePoint.transform.position);

        if (Vector3.Distance(transform.position, playerPlace) <= 0.4f)
        {
            Invoke("StepRet", 0.4f);
        }
    }
    void StepRet()
    {
        Return();
    }
    bool returned;
    private void Return()
    {
        if (returned == false)
        {
            var dir = mummy.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            this.GetComponent<Rigidbody>().AddForce(transform.up * 700f);
            canStop = true;
            returned = true;
        }
    }
    bool grab;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Return();
            grab = true;

            StartCoroutine(GrabPlayer(3f));
        }
        if (collision.gameObject == mummy.gameObject && canStop)
        {
            
            this.GetComponent<Rigidbody>().velocity = Vector2.zero;
            if (!grab)
            {
                mummy.GetComponent<EnemyShootingBossBlue>().playerGrabbed = false;
                mummy.GetComponent<EnemyShootingBossBlue>().blastWave.GetComponent<BlastWave>().BOOOM();
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator GrabPlayer(float duration)
    {
        float time = 0;
        mummy.GetComponent<EnemyShootingBossBlue>().playerGrabbed = true;

        while (time < duration)
        {
           PlayerMovement.Instance.transform.position = this.transform.position;
            time += Time.deltaTime;
            yield return null;
        }
        mummy.GetComponent<EnemyShootingBossBlue>().playerGrabbed = false;
        mummy.GetComponent<EnemyShootingBossBlue>().blastWave.GetComponent<BlastWave>().BOOOM();
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
