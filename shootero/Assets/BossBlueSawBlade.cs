using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlueSawBlade : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 5));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DealDamage(10));
        }
    }

    private IEnumerator DealDamage(int loops)
    {
        for (int i = 0; i < loops; i++)
        {
            PlayerMovement.Instance.ProcessDamage(2);
            yield return new WaitForSeconds(0.08f);
        }
    }
}
