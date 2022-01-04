using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSmol : MonoBehaviour
{
    private void Start()
    {
        Invoke("Destro", 4);
    }
    private void Destro()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.ProcessDamage(4);
            Destroy(gameObject);
        }
    }
}
