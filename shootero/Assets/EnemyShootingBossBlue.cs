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
        StartCoroutine(NozzlesSettings(maxNozzlesCount));
    }

    private IEnumerator AtackChain(Vector3 fireP)
    {
        while (faze == 1)
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
        int b = 0;
        int i = 0;
        GameObject[] spawnedNozzles = new GameObject[20];
        for (; i < maxNozzlesCount; i++, b++)
        {
            yield return new WaitForSeconds(0.3f);

            var curNozl = Instantiate(nozzles[i], transform.position, nozzles[i].transform.rotation);
            nozzles[i].transform.rotation = Quaternion.Euler(0, 0, 180 + rotation(i));
            curNozl.transform.parent = nozzlesSugarDaddy.transform;

            spawnedNozzles[b] = curNozl;
            StartCoroutine(NozzlesShoot(rotation(i), spawnedNozzles, maxNozzlesCount));
        }
        faze = 2;
        yield return new WaitForEndOfFrame();
        StartCoroutine(NozzlesShoot(rotation(i), spawnedNozzles, maxNozzlesCount));
    }
    float rotation(int i)
    {
        return (float)i * (360 / maxNozzlesCount);
    }
    private IEnumerator NozzlesShoot(float rotation, GameObject[] nozzles, int curNumOfNozls)
    {
        while (faze == 2) {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < curNumOfNozls; i++)
            {
                var curFirePoint = nozzles[i].GetComponent<FirePointNozzle>().firePoint;
                var curBul = Instantiate(bullet, curFirePoint.transform.position, new Quaternion(0, 0, 180 + rotation, 0));
                curBul.GetComponent<Rigidbody>().AddForce(200 * curBul.transform.up);
            }
        }
        for (int i = 0; i < curNumOfNozls; i++)
        {
            var curFirePoint = nozzles[i].GetComponent<FirePointNozzle>().firePoint;
            var curBul = Instantiate(bullet, curFirePoint.transform.position, new Quaternion(0, 0, 180 + rotation, 0));
            curBul.GetComponent<Rigidbody>().AddForce(200 * curBul.transform.up);
        }
        yield return null;
    }
    public GameObject blastWave;
    private void Update()
    {
        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) <= 2f && !playerGrabbed)
        {
            blastWave.GetComponent<BlastWave>().BOOOM();
        }
    }
}
