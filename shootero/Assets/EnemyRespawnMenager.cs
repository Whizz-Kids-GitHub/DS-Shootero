using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnMenager : MonoBehaviour
{
    public GameObject[] allEnemies;
    private GameObject minXnY;
    private GameObject maxXnY;

    public void RespawnEnemies(float enemiesNum)
    {
        maxXnY = GameObject.Find("maxXnY");
        minXnY = GameObject.Find("minXnY");


        for (int i = 0; i < enemiesNum; i++)
        {
            GameObject curEnemy = Instantiate(allEnemies[Random.Range(0, allEnemies.Length - 1)],
                transform.position, Quaternion.identity);

            curEnemy.GetComponent<EnemyMovement>().minXnY= minXnY;
            curEnemy.GetComponent<EnemyMovement>().maxXnY= maxXnY;

            //curEnemy.GetComponent<EnemyStatisctics>();
        }
    }
}
