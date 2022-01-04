using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TarkovData : MonoBehaviour
{
    public int Coins;

    public int hp;
    public int mvSpd;
    public int bulSpd;
    public int atkSpd;
    public int dmg;

    public TarkovData(TarkovController obj)
    {
        Coins = obj.Coins;
        hp = obj.hp;
        mvSpd = obj.mvSpd;
        bulSpd = obj.bulSpd;
        atkSpd = obj.atkSpd;
        dmg = obj.dmg;
    }
}
