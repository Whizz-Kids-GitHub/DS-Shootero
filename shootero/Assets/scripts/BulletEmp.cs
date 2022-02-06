using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmp : MonoBehaviour
{
    [SerializeField]
    private GameObject hameha;
    [SerializeField]
    private GameObject particlesExplo;
    private void Start()
    {
        var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        this.GetComponent<Rigidbody>().AddForce(transform.up * 200f);
        Invoke("Destro", 6f);
    }
    private void Update()
    {
        var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        float duration, startDur;
        startDur = 1.3333f;
        duration = startDur;
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(hameha, PlayerMovement.Instance.gameObject.transform.position, Quaternion.identity);
            Instantiate(particlesExplo, PlayerMovement.Instance.gameObject.transform.position, Quaternion.identity);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<SpriteRenderer>().sprite = null;

            Vector3 lastPos = PlayerMovement.Instance.gameObject.transform.position;
            if (duration >= 0)
            {
                PlayerMovement.Instance.gameObject.transform.position = lastPos;
                duration -= Time.deltaTime;
            }
            else
            {
                duration = startDur;
            }
            Destroy(gameObject);
        }
    }
    private void Destro()
    {
        Destroy(gameObject);
    }
}
