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
        player = PlayerMovement.Instance.gameObject.GetComponent<Transform>();
        playerPlace = player.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerPlace, Time.deltaTime);

        #region Damage
        if (Vector3.Distance(transform.position, playerPlace) <= 0.2f)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            
            if (Vector3.Distance(transform.position, player.position) <= 0.5f)
            {
                PlayerMovement.Instance.ProcessDamage(damage);
            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        #endregion
    }
}
