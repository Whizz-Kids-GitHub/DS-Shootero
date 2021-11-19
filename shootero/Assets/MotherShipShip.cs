using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipShip : MonoBehaviour
{
    public GameObject mum;

    private void OnDestroy()
    {
        mum.GetComponent<EnemyMotherShipBehaviour>().dronesAmount -= 1;
    }
}
