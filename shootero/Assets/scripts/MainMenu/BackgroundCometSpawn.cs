using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCometSpawn : MonoBehaviour
{
    public Object comet;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 17));
            Instantiate(comet, new Vector3(10, 10, 0), new Quaternion());
        }
    }
}
