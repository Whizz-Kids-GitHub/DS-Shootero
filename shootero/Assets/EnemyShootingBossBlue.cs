using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBossBlue : MonoBehaviour
{
    [SerializeField]
    private GameObject chain;

    public GameObject firePoint;

    private int faze;

    [SerializeField]
    private float timeBtwChainAtacks;
    public bool playerGrabbed;
    private int maxNozzlesCount = 15;

    private void Start()
    {
        faze = 1;
        StartCoroutine(AtackChain(firePoint.transform.position));
        //StartCoroutine(NozzlesSettings(maxNozzlesCount));
    }

    private IEnumerator AtackChain(Vector3 fireP)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBtwChainAtacks);

            var curChain = Instantiate(chain, fireP, Quaternion.identity);

            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            curChain.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            curChain.GetComponent<Rigidbody>().AddForce(curChain.transform.up * 350);
            curChain.GetComponent<Chain>().mummy = this.gameObject;
        }
    }
    [SerializeField]
    GameObject[] nozzles;
    [SerializeField]
    GameObject nozzlePref;
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject nozzlesSugarDaddy;
    private IEnumerator NozzlesSettings(int maxNozzlesCount)
    {
        for (int a = 0; a < maxNozzlesCount; a++)
        {
            nozzles[a] = nozzlePref;
        }
        int i = 0;
        int nozleCount = 0;
        GameObject[] spawnedNozzles = new GameObject[maxNozzlesCount];
        for (; i < maxNozzlesCount; i++)
        {
            yield return new WaitForSeconds(0.3f);

            var rotToSpawn = rotation(i);
            var curNozl = Instantiate(nozzles[i], transform.position, Quaternion.Euler(0, 0, 180 + rotToSpawn));
            curNozl.transform.parent = nozzlesSugarDaddy.transform;

            spawnedNozzles[i] = curNozl;
            nozleCount += 1;
            StartCoroutine(NozzlesShoot(spawnedNozzles, nozleCount, rotToSpawn));
        }
        faze = 2;
        yield return new WaitForEndOfFrame();
        StartCoroutine(NozzlesShoot(spawnedNozzles, maxNozzlesCount, 0));
    }
    float rotation(int i)
    {
        return (float)(i * (450 / maxNozzlesCount));
    }
    private IEnumerator NozzlesShoot(GameObject[] nozzles, int curNumOfNozls, float rot)
    {
        while (faze == 2)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < curNumOfNozls; i++)
            {
                var curFirePoint = nozzles[i].GetComponent<FirePointNozzle>().firePoint;
                var curBul = Instantiate(bullet, curFirePoint.transform.position, new Quaternion(0, 0, 180 + rotation(i), 0));
                curBul.GetComponent<Rigidbody>().AddForce(200 * curBul.transform.up);
            }
        }
        for (int i = 0; i < curNumOfNozls; i++)
        {
            var curFirePoint = nozzles[i].GetComponent<FirePointNozzle>().firePoint;
            var curBul = Instantiate(bullet, curFirePoint.transform.position, new Quaternion(0, 0, 180 + rot, 0));
            curBul.GetComponent<Rigidbody>().AddForce(200 * curBul.transform.up);
        }
    }
    public GameObject blastWave;
    private void Update()
    {
        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) <= 2f && !playerGrabbed)
        {
            blastWave.GetComponent<BlastWave>().BOOOM();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerMovement.Instance.gameObject)
        {
            StartCoroutine(SawBladeDamage());
        }

    }
    [SerializeField]
    private GameObject particlesSawBladeDamage;
    private IEnumerator SawBladeDamage()
    {
    //    GameObject rayPoint;
    //    rayPoint = new GameObject();
    //    rayPoint.transform.parent = this.gameObject.transform;
    //    rayPoint.transform.localPosition = Vector3.zero;

    //    var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
    //    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //    rayPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //    RaycastHit hit;
    //    Physics.Raycast(rayPoint.transform.position, rayPoint.transform.position + (-rayPoint.transform.up * 15f), out hit, Mathf.Infinity);
        for (int i = 0; i < 37; i++)
        {
            PlayerMovement.Instance.ProcessDamage(3);
            
            //var curParticles = Instantiate(particlesSawBladeDamage, hit.point, Quaternion.identity);
            //curParticles.transform.localScale = new Vector3(0.2f, 0.2f, 0);

            yield return new WaitForSeconds(0.08f); //1.6 seconds
        }
    }
}
