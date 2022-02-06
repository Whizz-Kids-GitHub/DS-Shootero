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
    [HideInInspector]
    public bool HealPlr;
    [HideInInspector]
    public PlayerMovement PlayrMv;

    private void Start()
    {
        PlayrMv = FindObjectOfType<PlayerMovement>();        
    }

    private void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        if(Enemy.GetComponent<EnemyStatisctics>())
        {
            enemyStats = Enemy.GetComponent<EnemyStatisctics>();

            enemyStats.hp -= damage;

            hit = true;

            if(HealPlr)
            {
                PlayrMv.ProcessDamage(damage / 5);
            }
            Destroy(gameObject);
        }
    }
}
