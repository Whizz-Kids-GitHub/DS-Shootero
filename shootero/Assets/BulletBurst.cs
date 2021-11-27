using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBurst : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage += damage;
            Destroy(gameObject);
        }
    }
}
