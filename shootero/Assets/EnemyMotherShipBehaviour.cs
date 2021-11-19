using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotherShipBehaviour : MonoBehaviour
{
    public GameObject ship;
    public GameObject spawnPoint;

    public float coolDown;
    public int maxDrones;
    public int dronesAmount;

    private void Start()
    {
        SpawnShip();
    }

    public void SpawnShip()
    {
        if (dronesAmount < maxDrones)
        {
            GameObject curShip = Instantiate(ship, spawnPoint.transform.position, Quaternion.identity);
            curShip.GetComponent<MotherShipShip>().mum = this.gameObject;
            dronesAmount += 1;
        }

        Invoke("SpawnShip",coolDown);
    }
}
