using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    public Material mat;
    public Material matDef;
    private GameObject player;

    private float time, duration = 0.15f;
    private LineRenderer rend;
    public void StartSequence(Vector3 start, float timeBtwWarningAndShot)
    {
        StartCoroutine(CastLaser(start, timeBtwWarningAndShot));
    }
    public IEnumerator CastLaser(Vector3 start, float timeBtwWarningAndShot)
    {

        player = GameObject.FindGameObjectWithTag("Player");
        var playerPlace = player.transform.position;

        var dir = playerPlace - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90 + Random.Range(-30, 30), Vector3.forward);

        rend = GetComponent<LineRenderer>();

        rend.endWidth = 0.06f;
        rend.startWidth = 0.06f;

        rend.material = matDef;

        rend.SetPosition(0, start);
        rend.SetPosition(1, transform.up * -20);
        yield return new WaitForSeconds(timeBtwWarningAndShot);
        rend.SetPosition(1, start);

        rend.material = mat;

        rend.SetPosition(0, start);
        rend.SetPosition(1, transform.up * -20);
        StartCoroutine(SizeUp());
    }

    IEnumerator SizeUp()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.02f, 0), new Vector2(0.06f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.06f;
        rend.startWidth = 0.06f;
        StartCoroutine(SizeDown());
    }

    IEnumerator SizeDown()
    {
        time = 0;
        while (time < duration)
        {
            var vector = Vector2.Lerp(new Vector2(0.06f, 0), new Vector2(0.02f, 0), time / duration);
            rend.startWidth = vector.x;
            rend.endWidth = vector.x;
            time += Time.deltaTime;
            yield return null;
        }
        rend.endWidth = 0.00f;
        rend.startWidth = 0.00f;
    }
}
