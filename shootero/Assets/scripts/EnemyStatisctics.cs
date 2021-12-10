using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatisctics : MonoBehaviour
{
    public int damage;
    public int hp;
    private GameObject levelCounter;

    private void Start()
    {
        levelCounter = GameObject.Find("LevelCounter");

        damage += levelCounter.GetComponent<LevelCounter>().enemyStats;

        if (this.TryGetComponent<EnemyShootingLaser>(out EnemyShootingLaser shootingScriptL))
        {
            shootingScriptL.damage = damage;
        }
        else if (this.TryGetComponent<EnemyShootingBurst>(out EnemyShootingBurst shootingScriptB))
        {
            shootingScriptB.damage = damage;
        }
        else if (this.TryGetComponent<EnemyShootingExplo>(out EnemyShootingExplo shootingScriptEX))
        {
            shootingScriptEX.damage = damage;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown("k"))
        {
            Debug.Log(damage);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("PlayerBullet"))
    //    {
    //        if (hp <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }   
    //}
    private void OnDestroy()
    {
        if (levelCounter = GameObject.Find("LevelCounter"))
        {
            if (!this.GetComponent<MotherShipShip>())
            {
                levelCounter.GetComponent<LevelCounter>().deaths += 1;
                levelCounter.GetComponent<LevelCounter>().StartSequence();
            }
        }

    }
}
