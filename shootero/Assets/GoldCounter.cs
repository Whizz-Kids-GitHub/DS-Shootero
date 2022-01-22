using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    public int gold;

    public void CountGold()
    {
        gold += 1 * (GameObject.Find("LevelCounter").GetComponent<LevelCounter>().level) / 2;
    }
}
