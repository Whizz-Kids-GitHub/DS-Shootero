using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatisctics : MonoBehaviour
{
    public int damage;
    public int hp;
    [HideInInspector]
    public int startHp;
    private GameObject levelCounter;
    [SerializeField]
    private bool isBoss;
    private void Awake()
    {
        startHp = hp;
    }

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>())
        {
            if (hp <= 0 && !isBoss)
            {
                Destroy(gameObject);
            }
        }
    }
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
