using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletToSummonPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject portal;
    [HideInInspector]
    public GameObject currentPortal;
    private void Start()
    {
        StartCoroutine(LerpPosition(new Vector3(transform.position.x + 4, transform.position.y, 0), 0.5f));
    }
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        StartCoroutine(RespPortal());
    }
    private IEnumerator RespPortal()
    {
        this.GetComponent<SpriteRenderer>().sprite = null;
        var curPortal = Instantiate(portal, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(2f);

        curPortal.GetComponent<Animator>().SetTrigger("close");

        transform.position = new Vector3(transform.position.x, transform.position.y - 5, 0);
        curPortal = Instantiate(portal, transform.position, Quaternion.identity);
        currentPortal = curPortal;

        yield return new WaitForSeconds(2f);

        curPortal.GetComponent<Animator>().SetTrigger("close");
    }
}
