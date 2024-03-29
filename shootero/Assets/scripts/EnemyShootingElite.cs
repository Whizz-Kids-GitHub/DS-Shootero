using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingElite : MonoBehaviour
{
    [SerializeField]
    private GameObject empMissile;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private GameObject forcePoint;
    private void Start()
    {
        StartCoroutine(Atack());
    }
    private void Update()
    {
        var dir = PlayerMovement.Instance.gameObject.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    private IEnumerator Atack()
    {
        var movement = GetComponent<EnemyMovement>();
        while (true)
        {
            movement.canMove = true;
            yield return new WaitForSeconds(3.5f);  
            movement.canMove = false;

            yield return new WaitForSeconds(0.5f);

            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * -200f);

            var anim = GetComponent<Animator>();
            anim.SetTrigger("Knockback");
            
            Instantiate(empMissile, firePoint.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);
            rb.velocity = Vector2.zero;

        }
    }
}
