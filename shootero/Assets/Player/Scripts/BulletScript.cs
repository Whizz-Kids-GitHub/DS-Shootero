using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;

    private EnemyStatistics enemyStats;

    public bool hit;

    private void OnTiggerEnter2D(Collider2D Enemy)
    {
        if(Enemy.GetComponent<EnemyStatistics>())
        {

        }
    }
}
