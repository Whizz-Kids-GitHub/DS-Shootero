using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 oldPosition = transform.position;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3((oldPosition.x + Random.Range(-1.5f, 1.5f)),
                 (oldPosition.y + Random.Range(-1.5f, 1.5f)), -10), 1f);
            yield return new WaitForSeconds(0.05f);
        }
        transform.position = oldPosition;
    }
}
