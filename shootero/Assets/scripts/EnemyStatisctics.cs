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
    private Color startColor;
    private void Start()
    {
        startColor = this.GetComponent<SpriteRenderer>().color;
        levelCounter = LevelCounter.Instance.gameObject;

        damage += levelCounter.GetComponent<LevelCounter>().enemyStats;

        if (this.TryGetComponent<EnemyShootingBurst>(out EnemyShootingBurst shootingScriptB))
        {
            shootingScriptB.damage = damage;
        }
        else if (this.TryGetComponent<EnemyShootingExplo>(out EnemyShootingExplo shootingScriptEX))
        {
            shootingScriptEX.damage = damage;
        }
        else if (this.TryGetComponent<EnemyShootingPewPew>(out EnemyShootingPewPew shootingScriptPP))
        {
            shootingScriptPP.damage = damage;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Color hitColor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>())
        {
            this.GetComponent<SpriteRenderer>().color = hitColor;
            Invoke("Uncolor", 0.1f);
            if (hp <= 0 && !isBoss)
            {
                Destroy(gameObject);
            }
        }
    }
    private void Uncolor()
    {
        this.GetComponent<SpriteRenderer>().color = startColor;
    }
    private void OnDestroy()
    {
        if (!this.GetComponent<MotherShipShip>())
        {
            if (LevelCounter.Instance.boss && isBoss)
            {
                levelCounter.GetComponent<LevelCounter>().deaths += 1;
            }
            else if (!LevelCounter.Instance.boss && !isBoss)
            {
                levelCounter.GetComponent<LevelCounter>().deaths += 1;
            }
            levelCounter.GetComponent<LevelCounter>().StartSequence();
            GameObject.Find("GoldCounter").GetComponent<GoldCounter>().CountGold();
        }

    }
}
