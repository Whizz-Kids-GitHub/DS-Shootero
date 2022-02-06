using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
	private Transform target;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody2D rb;
	[SerializeField]
	private GameObject particles;
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
		Invoke("Destro", 3);
	}
	private void Destro()
    {
		Destroy(gameObject);
    }
	void FixedUpdate()
	{
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

	void Update()
    {
		var player = GameObject.FindGameObjectWithTag("Player");
		if (Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
        {
			PlayerMovement.Instance.ProcessDamage(15);
			Instantiate(particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
        }
    }
}
