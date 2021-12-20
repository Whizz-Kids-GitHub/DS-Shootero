using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBurst : MonoBehaviour
{
    public int damage;

    void Start()
    {
        Invoke("Destro", 5);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage += damage;
            Destroy(gameObject);
        }
    }
    private void Destro()
    {
        Destroy(gameObject);
    }
}
