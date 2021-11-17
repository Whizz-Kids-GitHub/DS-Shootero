using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatisctics : MonoBehaviour
{
    public int damage;
    public int hp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameObject levelCounter = GameObject.Find("LevelCounter");
        levelCounter.GetComponent<LevelCounter>().deaths += 1;
    }
}
