using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeSystem : MonoBehaviour
{
    Vector3 whereMove;
    private float time;
    public float cameraShakeTime;

    public float speed = 0.125f;

    public void CameraShake(float howLong)
    {
        if (cameraShakeTime < howLong)
        {
            cameraShakeTime = howLong;
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, whereMove, speed);
        if (cameraShakeTime >= 0)
        {
            whereMove = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -10);
            cameraShakeTime = cameraShakeTime - Time.deltaTime;
        } else
        {
            whereMove = new Vector3(0, 0, -10);
        }
    }
}
