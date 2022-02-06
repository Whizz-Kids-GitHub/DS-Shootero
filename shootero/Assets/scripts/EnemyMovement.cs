using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject targetSpot;
    [SerializeField]
    private GameObject minXnY;
    [SerializeField]
    private GameObject maxXnY;

    public GameObject exploParticles;

    public bool canMove;

    private void Start()
    {
        targetSpot = new GameObject("targetSpotEnemy");
        targetSpot.transform.position = Vector3.zero;
        canMove = true;
        StartCoroutine(GenerateTarget());
    }

    IEnumerator GenerateTarget()
    {
        StartCoroutine(Move());
        do
        {
            maxXnY = GameObject.Find("EnemyMoveSpaceMax");
            minXnY = GameObject.Find("EnemyMoveSpaceMin");

            targetSpot.transform.position = new Vector3(Random.Range(minXnY.transform.position.x, maxXnY.transform.position.x),
                Random.Range(minXnY.transform.position.y, maxXnY.transform.position.y), 0);
            yield return new WaitForSeconds(Random.Range(2, 4.5f));
        } while (true);
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (canMove)
            {
                transform.position = Vector2.Lerp(transform.position, targetSpot.transform.position, Time.deltaTime);
                yield return new WaitForSeconds(.001f);
            }
            else
            {
                yield return null;
            }
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

    private void OnDestroy()
    {
        Destroy(targetSpot);
    }
}
