using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloe : MonoBehaviour
{
    public float time;
    public float duration;
    public float duration2;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(SizeUp());
    }

    IEnumerator SizeUp()
    {
        time = 0;
        Vector3 startSize = Vector3.zero;
        while (time < duration)
        {

            transform.localScale = Vector3.Lerp(startSize, new Vector3(2, 2, 0), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(2, 2, 0);

        yield return new WaitForSeconds(3f);
        StartCoroutine(destroy());

    }

    IEnumerator destroy()
    {
        time = 0;
        Vector3 startSize = Vector3.zero;
        while (time < duration2)
        {
            transform.localScale = Vector3.Lerp(new Vector3(2, 2, 0), startSize, time / duration2);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(0, 0, 0);
        Destroy(gameObject);
    }

}
