using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    private int randomNum;
    public int level = 0;

    [Space]
    [SerializeField]
    public Upgrades[] Upgrades = new Upgrades[4];

    [SerializeField]
    public Rarities[] Rarities;

    public GameObject[] buttons = new GameObject[3];

    public int[] UpgradeNum = new int[3];
    public int[] rarity = new int[3];
    [Space(15)]

    public bool[] avaliable;

    void Start()
    {
        UpgradeRarity(level);
    }

    public void UpgradeRarity(int lvl)
    {
        for (int i = 0; i < 3; i++)
        {
            randomNum = Random.Range(0, 100);

            if (randomNum <= Rarities[lvl].Rarity[0])
            {
                Upgrade(0, i);
            }
            else if (randomNum > Rarities[lvl].Rarity[0] && randomNum <= Rarities[lvl].Rarity[1])
            {
                Upgrade(2, i);
            }
            else if (randomNum > Rarities[lvl].Rarity[1] && randomNum <= Rarities[lvl].Rarity[2])
            {
                Upgrade(3, i);
            }
            else if (randomNum > Rarities[lvl].Rarity[2] && randomNum <= 100)
            {
                Upgrade(4, i);
            }
        }
    }

    void Upgrade(int rar, int i)
    { 
        int num = Random.Range(0 , Upgrades[rar].UpgradeStats.Length); 
        
        UpgradeNum[i] = num;
        rarity[i] = rar;

        buttons[num].GetComponent<Image>().sprite = Upgrades[rar].UpgradeStats[num].Icon;
    }

    public void Apply(int button)
    {
        Instantiate(Upgrades[rarity[button]].UpgradeStats[UpgradeNum[button]].Icon, this.transform);
    }
}

[System.Serializable]
public class Upgrades
{
    public UpgradeStats[] UpgradeStats;
}

[System.Serializable]
public class Rarities
{
    [Range(0, 100)]
    public  int[] Rarity = new int[4];
}

[System.Serializable]
public class UpgradeStats
{
    public GameObject UpgradeObj;
    public Sprite Icon;
    [Space(15)]

    public int number;
    public bool change;
}