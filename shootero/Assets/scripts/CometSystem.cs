using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSystem : MonoBehaviour
{
    Vector3 whereSpawn;
    GameObject enemy;
    public Animator animator;
    public Object comet;

    IEnumerator Start()
    {
        enemy = GameObject.Find("Statek");
        while (true)
        {
            StartCoroutine(SpawningComet());
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator SpawningComet()
    {
        animator.SetBool("isWarning", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isWarning", false);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<CameraShakeSystem>().CameraShake(2.5f);
        whereSpawn = new Vector3(Random.Range(-9f, 9f), 6f, 0f);
        Vector3 dir = enemy.transform.position - whereSpawn;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(comet, whereSpawn, Quaternion.AngleAxis(angle, Vector3.forward));

    }
}
