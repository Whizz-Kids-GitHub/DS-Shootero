using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject targetSpot;
    public GameObject minXnY;
    public GameObject maxXnY;

    private void Start()
    {
        minXnY = GameObject.Find("Min");
        maxXnY = GameObject.Find("Max");
        targetSpot = new GameObject("targetSpotEnemy");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        StartCoroutine(Move2());
        do
        {

            targetSpot.transform.position = new Vector3(Random.Range(minXnY.transform.position.x, maxXnY.transform.position.x),
                Random.Range(minXnY.transform.position.y, maxXnY.transform.position.y), 0);
            yield return new WaitForSeconds(4f);
        } while (true);

    }
    IEnumerator Move2()
    {
        while (0 == 0)
        {
            transform.position = Vector2.Lerp(transform.position, targetSpot.transform.position, Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }

    }
}
