using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplit : MonoBehaviour
{
    [SerializeField]
    private GameObject smallerBullet;
    private int numberOfSmallerPieces;

    private void Start()
    {
        numberOfSmallerPieces = 8;
        Invoke("Split", 1f);
    }

    private void Split()
    {
        for (int i = 0; i <= numberOfSmallerPieces; i++)
        {
            float angle = 360 / numberOfSmallerPieces;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + angle * i);
            var curPiece = Instantiate(smallerBullet, transform.position, transform.rotation);
            var rb = curPiece.GetComponent<Rigidbody>();
            rb.AddForce(curPiece.transform.up * 200);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.ProcessDamage(6);
            Destroy(gameObject);
        }
    }
}
