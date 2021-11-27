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

    private Vector3 rngArea;

    private int randomNum;
    private LevelCounter levelCounter;

    private void Start()
    {
        levelCounter = GameObject.Find("LevelCounter").GetComponent<LevelCounter>();
    }

    public void RespawnEnemies(float enemiesNum, int enemy)
    {
        maxXnY = GameObject.Find("RespawnMaxXnY");
        minXnY = GameObject.Find("RespawnMinXnY");

        maxXnY2 = GameObject.Find("RespawnMaxXnY2");
        minXnY2 = GameObject.Find("RespawnMinXnY2");

        maxXnY3 = GameObject.Find("RespawnMaxXnY3");
        minXnY3 = GameObject.Find("RespawnMinXnY3");

        for (int i = 0; i < enemiesNum; i++)
        {
            #region areaChoose

            Vector3 respArea1 = new Vector3(Random.Range(minXnY.transform.position.x, maxXnY.transform.position.x),
                Random.Range(minXnY.transform.position.y, maxXnY.transform.position.y), 0);

            Vector3 respArea2 = new Vector3(Random.Range(minXnY2.transform.position.x, maxXnY2.transform.position.x),
                Random.Range(minXnY2.transform.position.y, maxXnY2.transform.position.y), 0);

            Vector3 respArea3 = new Vector3(Random.Range(minXnY3.transform.position.x, maxXnY3.transform.position.x),
                Random.Range(minXnY3.transform.position.y, maxXnY3.transform.position.y), 0);

            randomNum = Random.Range(1, 4);

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
            #endregion

            GameObject curEnemy = Instantiate(allEnemies[enemy], new Vector3(rngArea.x, rngArea.y, 10), new Quaternion(0,0,0,0));

            curEnemy.GetComponent<EnemyStatisctics>().hp += levelCounter.enemyStats;
            curEnemy.GetComponent<EnemyStatisctics>().damage += levelCounter.enemyStats;
        }
    }
}
