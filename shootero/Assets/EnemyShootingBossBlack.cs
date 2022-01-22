using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBossBlack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletToSummonPortal;
    private int faze;
    [SerializeField]
    private GameObject firePoint;

    private void Start()
    {
        faze = 1;
        StartCoroutine(Castportal());
    }
    private IEnumerator Castportal()
    {
        yield return new WaitForSeconds(2f);

        while (faze == 1)
        {
            var curBullet = Instantiate(bulletToSummonPortal, firePoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);

            
            StartCoroutine(LerpPosition(new Vector3(transform.position.x + 4, transform.position.y, 0), 1));
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Animator>().SetTrigger("Roll");

            yield return new WaitForSeconds(1f);

            var curSprite = this.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<SpriteRenderer>().sprite = null;
            

            while (!curBullet.GetComponent<BulletToSummonPortal>().currentPortal.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PortalOtw"))
            {
                Debug.Log("nie");
            }
            transform.position = new Vector3(transform.position.x, transform.position.y - 5, 0);
            this.GetComponent<SpriteRenderer>().sprite = curSprite;
            StartCoroutine(LerpPosition(new Vector3(transform.position.x - 4, transform.position.y, 0), 1f));

            yield return new WaitForSeconds(5f);
        }
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stats = this.GetComponent<EnemyStatisctics>();

        if (collision.gameObject.GetComponent<BulletScript>())
        {
            if (stats.hp <= 0)
            {
                stats.hp = stats.startHp;
                faze += 1;
            }
        }
    }
}
