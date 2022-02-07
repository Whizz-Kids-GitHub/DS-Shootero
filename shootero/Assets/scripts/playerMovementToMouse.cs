using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementToMouse : MonoBehaviour
{
    public float mvOffset;
    public float BaseMvOffset;
    public bool canMove;

    private static playerMovementToMouse instance;
    public static playerMovementToMouse Instance { get => instance; }

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
    private void Start()
    {
        mvOffset = BaseMvOffset;
        canMove = true;
    }
    private void Update()
    {
        if (canMove)
        {
            Vector2 gryzonPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 1);
            transform.position = Vector2.Lerp(transform.position, gryzonPos, Time.deltaTime + mvOffset);

            Vector3 curRot = new Vector3(0, 0, transform.rotation.z);
            float wantedRotZ = Vector3.Lerp(curRot, Vector3.zero, Time.deltaTime).z;
            transform.rotation = Quaternion.Euler(0, 0, wantedRotZ);
        }
    }
}
