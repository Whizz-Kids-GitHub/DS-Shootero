using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloe : MonoBehaviour
{
    public float scalingSpeed;

    private void Awake()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0);
    }
    private void Start()
    {
        
        SizeUp();
    }

    void SizeUp()
    {


        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 0), scalingSpeed * Time.deltaTime);
        Invoke("step", 0.1f);

    }
    void step()
    {
        if (transform.localScale.x <= 2)
        {
            SizeUp();
        }
    }
}
