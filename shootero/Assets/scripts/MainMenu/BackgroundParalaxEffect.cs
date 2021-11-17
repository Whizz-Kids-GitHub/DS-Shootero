using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParalaxEffect: MonoBehaviour
{
    [SerializeField] private float paralaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * paralaxEffectMultiplier;
        lastCameraPosition = cameraTransform.position;
    }
}
