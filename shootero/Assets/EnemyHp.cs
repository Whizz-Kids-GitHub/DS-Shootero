using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int hp;
    public GameObject damageEffect;
    public GameObject deathEffect;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //  if (collision.gameObject.CompareTag("PlayerBullet"))
    //  {
    //      if (hp <= 0)
    //      {
    //          Destroy(gameObject);
    //     }
    // }
    // }

    private void OnDestroy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}
