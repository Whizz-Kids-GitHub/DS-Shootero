using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class LevelCounter : MonoBehaviour
{
    public int level;
    private EnemyRespawnMenager respMenager;
    public float enemyCount;
    public int deaths;
    public float dificultyScallingSpeed;

    private void Start()
    {
        respMenager = GameObject.Find("EnemyRespawnMenager").GetComponent<EnemyRespawnMenager>();

        enemyCount = level * (dificultyScallingSpeed);
        Respawn();
    }

    private void Update()
    {
        if (deaths >= enemyCount)
        {
            level += 1;

            enemyCount = dificultyScallingSpeed * (Mathf.Pow(level, 1.5f));
            deaths = 0;
            Respawn();
        }

    }

    public void Respawn()
    {

        for (int i = 0; i < enemyCount; i++)
        {
            respMenager.RespawnEnemies(1);
        }

    }
}
