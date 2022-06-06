using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retrieve : MonoBehaviour
{
    public int[] num = new int[5];

    private PlayerMovement plrMv;
    private PlayerShooting plrSh;

    void Start()
    {
        for (int i = 0; i < num.Length; i++)
        {
            num[i] = ActiveController.noumbs[i];
        }

        plrMv.pMaxHealth += num[1];
        plrMv.pHealth += num[1];

        for (int i  = 0; i < plrSh.Bullet.Length; i++)
        {
            plrSh.Bullet[i].damage = plrSh.Bullet[i].damage * num[2];
            plrSh.Bullet[i].atkSpeed = plrSh.Bullet[i].atkSpeed * num[3];
        }

        plrMv.speedModifier = plrMv.speed * num[4];

        plrMv.coinsMod = num[5];

    }
}
