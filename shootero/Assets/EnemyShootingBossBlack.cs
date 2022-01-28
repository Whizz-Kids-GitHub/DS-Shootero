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
        StartCoroutine(CastPortal());
    }
    private IEnumerator CastPortal()
    {
        while (true)
        {
            var curBullet = Instantiate(bulletToSummonPortal, firePoint.transform.position, Quaternion.identity);
            var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            curBullet.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            curBullet.GetComponent<Rigidbody>().AddForce(curBullet.transform.up * -200);
            yield return new WaitForSeconds(1f);

            curBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Animator>().SetTrigger("Roll");

            LerpPosition(curBullet.transform.position, 0.5f);
            yield return new WaitForSeconds(0.5f);
            Destroy(curBullet);

            var min = GameObject.Find("EnemyMoveSpaceMin");
            var max = GameObject.Find("EnemyMoveSpaceMax");

            LerpPosition(new Vector3(Random.Range(min.transform.position.x, max.transform.position.x), 
                Random.Range(min.transform.position.y, max.transform.position.y), 0), 1f);

            yield return new WaitForSeconds(2f);
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
