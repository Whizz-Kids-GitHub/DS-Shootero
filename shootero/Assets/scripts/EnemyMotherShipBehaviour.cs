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
    [SerializeField]
    private GameObject particleSystem;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnShip();
        if (donnaMama == null)  donnaMama = this.gameObject;
    }

    private void Update()
    {
        var dir = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    public void SpawnShip()
    {
        if (dronesAmount < maxDrones)
        {
           // particleSystem.GetComponent<ParticleSystem>().Play();
            GameObject curShip = Instantiate(ship, spawnPoint.transform.position, Quaternion.identity);
            curShip.GetComponent<MotherShipShip>().mum = donnaMama;
            dronesAmount += 1;
        }

        Invoke("SpawnShip",coolDown);
    }
}
