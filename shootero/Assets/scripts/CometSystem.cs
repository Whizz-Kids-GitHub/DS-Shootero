using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSystem : MonoBehaviour
{
    Vector3 whereSpawn;
    GameObject enemy;
    GameObject objectLine;
    public Animator animator;
    public GameObject comet;
    public LineRenderer lineRenderer;

    IEnumerator Start()
    {
        enemy = GameObject.Find("Statek");
        objectLine = GameObject.Find("CometWay");
        while (true)
        {
            StartCoroutine(SpawningComet());
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator SpawningComet()
    {
        whereSpawn = new Vector3(Random.Range(-9f, 9f), 6f, 0f);
        Vector3 dir = enemy.transform.position - whereSpawn;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject cometObject = Instantiate(comet, whereSpawn, Quaternion.AngleAxis(angle, Vector3.forward));

        Vector3 endPosition = enemy.transform.position + (cometObject.transform.right * 5f);
        lineRenderer.SetPositions(new Vector3[] { whereSpawn, endPosition });
        objectLine.SetActive(true);


        animator.SetBool("isWarning", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isWarning", false);
        objectLine.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<CameraShakeSystem>().CameraShake(2.5f);
    }
}
