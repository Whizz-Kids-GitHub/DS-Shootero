using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploBullet : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private GameObject explosionEffect;
    private Vector3 playerPlace;

    public int damage = 5;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPlace = player.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerPlace, Time.deltaTime);

        #region Damage
        if (Vector3.Distance(transform.position, playerPlace) <= 0.5)
        {
            GameObject explo = Instantiate(explosionEffect, transform.position, new Quaternion(0, 0, 0, 0));

            if (Vector3.Distance(transform.position, playerPlace) <= 0.5) PlayerMovement.Instance.ProcessDamage(damage);

            Destroy(gameObject);
        }
        #endregion
    }
}
