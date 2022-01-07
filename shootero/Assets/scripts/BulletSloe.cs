using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSloe : MonoBehaviour
{
    private Transform player;
    private Transform playerPlace;
    private GameObject temp;
    public GameObject sloe;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPlace = player;

        temp = new GameObject("Temporary Bullet Sloe Object");
        temp.AddComponent<Rigidbody>().useGravity = false;
        temp.AddComponent<SphereCollider>().radius = 0.5f;
        temp.GetComponent<SphereCollider>().isTrigger = true;
        temp.tag = "tempSloeObject";
        temp.transform.position = playerPlace.position;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ("tempSloeObject"))
        {
            Instantiate(sloe, transform.position, Quaternion.identity);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            
            Destroy(temp);
            Destroy(gameObject);
        }
        else
        {
            Invoke("DeleteTemp", 5);
        }
    }

    void DeleteTemp()
    {
        Destroy(temp);
    }
}
