using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmp : MonoBehaviour
{
    private void Start()
    {
        var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        this.GetComponent<Rigidbody>().AddForce(transform.up * 200f);
    }

    private void OnTriggerEnter(Collider other)
    {
        float duration, startDur;
        startDur = 1.3333f;
        duration = startDur;
        if (other.gameObject.CompareTag("Player"))
        {
            if (duration >= 0)
            {
                PlayerMovement.Instance.moveInput = Vector2.zero;
                duration -= Time.deltaTime;
            }
            else
            {
                duration = startDur;
            }
            Destroy(gameObject);
        }
    }
}
