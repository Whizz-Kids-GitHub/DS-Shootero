using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class TarkovController : MonoBehaviour
{
    public int Coins;
    [Space(15)]

    public int hp;
    public int mvSpd;
    public int atkSpd;
    public int dmg;
    public int coinsEarn;
    [Space(15)]

    public int[] price;

    [HideInInspector]
    public bool canBuy = true;

    public void buyHp(int num)
    {
        if(Coins >= price[1])
        {
            hp += num;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }

    public void buyMvSpd(int num)
    {
        if (Coins >= price[2])
        {
            mvSpd += num;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }

    public void buyAtkSpd(int num)
    {
        if (Coins >= price[3])
        {
            atkSpd += num;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }

    public void buyDmg(int num)
    {
        if (Coins >= price[4])
        {
            dmg += num;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }

    public void buyEarning(int num)
    {
        if (Coins >= price[5])
        {
            coinsEarn += num;
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }
    }

    public static void TarkovSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerMovement data = new TarkovData(FindObjectOfType<TarkovController>());
    }
}
