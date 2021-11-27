using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestro : MonoBehaviour
{
    private float time = 1f;

    private void Update()
    {
        if (time <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
