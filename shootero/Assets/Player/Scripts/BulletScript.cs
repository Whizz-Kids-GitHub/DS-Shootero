using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]
    public int damage;

    private EnemyStatisctics enemyStats;

    [HideInInspector]
    public bool hit;

    private void OnTiggerEnter2D(Collider2D Enemy)
    {
        if(Enemy.GetComponent<EnemyStatisctics>())
        {
            enemyStats = Enemy.GetComponent<EnemyStatisctics>();

            enemyStats.hp -= damage;

            hit = true;

            Destroy(enemyStats.gameObject);
        }
    }
}
