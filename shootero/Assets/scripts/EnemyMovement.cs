using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject targetSpot;
    public GameObject minXnY;
    public GameObject maxXnY;

    public GameObject exploParticles;

    private float duration = 2;
    public Vector3 targetPosition;

    private void Start()
    {
        targetSpot = new GameObject("targetSpotEnemy");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
        }
        transform.position = targetPosition;

        StartCoroutine(Move2());
        do
        {
            maxXnY = GameObject.Find("maxXnY");
            minXnY = GameObject.Find("minXnY");

            targetSpot.transform.position = new Vector3(Random.Range(minXnY.transform.position.x, maxXnY.transform.position.x),
                Random.Range(minXnY.transform.position.y, maxXnY.transform.position.y), 0);
            yield return new WaitForSeconds(Random.Range(2, 4.5f));
        } while (true);

    }
    IEnumerator Move2()
    {
        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, targetSpot.transform.position, Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FallingObjects"))
        {
            Instantiate(exploParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
