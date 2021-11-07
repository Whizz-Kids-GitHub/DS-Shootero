using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSystem : MonoBehaviour
{
    Vector3 whereSpawn;
    GameObject enemy;
    public Object comet;

    IEnumerator Start()
    {
        enemy = GameObject.Find("Statek");
        SpawningComet();
        while (true)
        {
            SpawningComet();
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SpawningComet()
    {
        whereSpawn = new Vector3(Random.Range(-9f, 9f), 6f, 0f);
        Vector3 dir = enemy.transform.position - whereSpawn;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Instantiate(comet, whereSpawn, Quaternion.AngleAxis(angle, Vector3.forward));

    }
}
