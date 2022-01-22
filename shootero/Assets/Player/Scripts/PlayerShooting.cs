using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Range(0, 1)]
    public float recoil;

    public bool shooting;

    public Bullet[] Bullet;

    private PlayerMovement PlayrMv;

    void Start()
    {
        PlayrMv = FindObjectOfType<PlayerMovement>();
        
        for (int i = 0; i < Bullet.Length; i++)
        {
            Bullet[i].firePoint.position = new Vector3(Bullet[i].firePoint.position.x - 1.24f, Bullet[i].firePoint.position.y + 0.25f, Bullet[i].firePoint.position.z);
        }
    }

    void Update()
    {
        if (shooting == true)
        {
            for (int i = 0; i < Bullet.Length; i++)
            {
                if (Bullet[i].canShoot)
                {
                    Bullet[i].canShoot = false;

                    Bullet[i].lastBullet = Instantiate(Bullet[i].bulletPre, Bullet[i].firePoint);

                    Bullet[i].lastBullet.GetComponent<Rigidbody2D>().AddForce(Bullet[i].firePoint.up * Bullet[i].bulSpeed);

                    Bullet[i].lastBullet.GetComponent<BulletScript>().damage = Bullet[i].damage;

                    if(PlayrMv.invunerable == true && FindObjectOfType<InvincibilityShield>().Heal == true)
                    {
                        Bullet[i].lastBullet.GetComponent<BulletScript>().HealPlr = true;
                    }

                    StartCoroutine(shoot(i));
                }
            }
        }
    }

    IEnumerator shoot(int num)
    {
        yield return new WaitForSeconds(Bullet[num].atkSpeed);

        Bullet[num].canShoot = true;
    }
}

[System.Serializable]
public class Bullet
{
    [HideInInspector]
    public GameObject lastBullet;

    public Transform firePoint;
    public bool canShoot = true;

    [Space]
    public int damage;
    public float bulSpeed;
    public float atkSpeed;
    public float amount;

    public GameObject bulletPre;
}
