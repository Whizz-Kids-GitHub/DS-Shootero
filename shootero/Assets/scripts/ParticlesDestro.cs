using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestro : MonoBehaviour
{
    private float time = 5f;

    private static ParticlesDestro instance;
    public static ParticlesDestro Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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
