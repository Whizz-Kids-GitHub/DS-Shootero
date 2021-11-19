using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnMenager : MonoBehaviour
{
    public GameObject[] allEnemies;
    private GameObject minXnY;
    private GameObject maxXnY;

    private GameObject minXnY2;
    private GameObject maxXnY2;

    private GameObject minXnY3;
    private GameObject maxXnY3;

    private GameObject finalMoveSpaceMax;
    private GameObject finalMoveSpaceMin;

    private int randomNum;

    public void RespawnEnemies(float enemiesNum)
    {
        maxXnY = GameObject.Find("RespawnMaxXnY");
        minXnY = GameObject.Find("RespawnMinXnY");

        maxXnY2 = GameObject.Find("RespawnMaxXnY2");
        minXnY2 = GameObject.Find("RespawnMinXnY2");

        maxXnY3 = GameObject.Find("RespawnMaxXnY3");
        minXnY3 = GameObject.Find("RespawnMinXnY3");

        maxXnY3 = GameObject.Find("EnemyMoveSpaceMax");
        minXnY3 = GameObject.Find("EnemyMoveSpaceMin");

        for (int i = 0; i < enemiesNum; i++)
        {
            Vector3 respArea1 = new Vector3(Random.Range(minXnY.transform.position.x, maxXnY.transform.position.x),
                Random.Range(minXnY.transform.position.y, maxXnY.transform.position.y), 0);

            Vector3 respArea2 = new Vector3(Random.Range(minXnY2.transform.position.x, maxXnY2.transform.position.x),
                Random.Range(minXnY2.transform.position.y, maxXnY2.transform.position.y), 0);

            Vector3 respArea3 = new Vector3(Random.Range(minXnY3.transform.position.x, maxXnY3.transform.position.x),
                Random.Range(minXnY3.transform.position.y, maxXnY3.transform.position.y), 0);

            randomNum = Random.Range(1, 4);
            Vector3 rngArea = Vector3.zero;

            switch (randomNum)
            {
                case 1:
                    rngArea = respArea1;

                    break;
                case 2:
                    rngArea = respArea2;
                    break;
                case 3:
                    rngArea = respArea3;
                    break;
            }

            Debug.Log(rngArea);

            GameObject curEnemy = Instantiate(allEnemies[Random.Range(0, allEnemies.Length - 1)],
                rngArea, Quaternion.identity);

            curEnemy.TryGetComponent<EnemyMovement>(out EnemyMovement useless);
            useless.targetPosition = new Vector3(Random.Range(finalMoveSpaceMin.transform.position.x, finalMoveSpaceMax.transform.position.x),
                 Random.Range(finalMoveSpaceMin.transform.position.y, finalMoveSpaceMax.transform.position.y), 0);

            //curEnemy.GetComponent<EnemyStatisctics>();
        }
    }
}
