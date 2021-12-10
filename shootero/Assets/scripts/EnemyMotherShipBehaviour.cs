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

    [SerializeField]
    private GameObject donnaMama;

    private void Start()
    {
        SpawnShip();
        if (donnaMama == null)  donnaMama = this.gameObject;
    }

    public void SpawnShip()
    {
        if (dronesAmount < maxDrones)
        {
            GameObject curShip = Instantiate(ship, spawnPoint.transform.position, Quaternion.identity);
            curShip.GetComponent<MotherShipShip>().mum = donnaMama;
            dronesAmount += 1;
        }

        Invoke("SpawnShip",coolDown);
    }
}
