using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingGuard : MonoBehaviour
{
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject firePoint;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Atack());
    }

    private void Update()
    {
        var dir = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

    }

    private IEnumerator Atack()
    {
        do
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(missile, firePoint.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(4);

        } while (true);
    }

}
