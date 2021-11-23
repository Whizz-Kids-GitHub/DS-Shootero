using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    public int level;
    private EnemyRespawnMenager respMenager;
    public float enemyCount;
    public int deaths;
    public float dificultyScallingSpeed;

    [SerializeField]
    private int dificulty;

    private void Start()
    {
        respMenager = GameObject.Find("EnemyRespawnMenager").GetComponent<EnemyRespawnMenager>();

        enemyCount = level * (dificultyScallingSpeed);
        StartCoroutine(Respawn());
    }

    public void StartSequence()
    {
        for (int i = 0; i < 2; i++)
        {
            if (deaths >= enemyCount)
            {
                level += 1;

                enemyCount = dificultyScallingSpeed * (Mathf.Pow(level, 1.5f));
                deaths = 0;
                StartCoroutine(Respawn());
            }
        }

    }
    public IEnumerator Respawn()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            respMenager = GameObject.Find("EnemyRespawnMenager").GetComponent<EnemyRespawnMenager>();
            yield return new WaitForSeconds(1);

            if (level == 1 )
            {
                //tez jak 2 sie dzieje pamietaj
                dificulty = 1;

                respMenager.RespawnEnemies(1, 0);
            }
            else if (1 < level && level < 3) //uwu baka sussy
            {
                dificulty = 2;

                respMenager.RespawnEnemies(1, Random.Range(0, 1));
            }
            else if (level >= 3 && level < 5)
            {
                dificulty = 3;

                respMenager.RespawnEnemies(1, Random.Range(0, 2));
            }
            else if (level >= 5)
            {
                dificulty = 4;

                respMenager.RespawnEnemies(1, Random.Range(0, respMenager.allEnemies.Length));
            }
        }
    }
}
