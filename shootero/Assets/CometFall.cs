using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public Rigidbody2D rigidbodyComet;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    float strength = 900;

    private void Start()
    {
        rigidbodyComet.AddForce(transform.right * strength);
        spriteRenderer.sprite = sprites[Random.Range(0, 2)];
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DestroyDetect")
        {
            Destroy(this.gameObject);
        }
    }
}
