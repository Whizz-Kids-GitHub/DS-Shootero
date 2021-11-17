using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnedComet : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriterenderer;
    public Sprite[] sprites;

    private IEnumerator Start()
    {
        animator.SetFloat("which", Random.Range(1, 3));
        spriterenderer.sprite = sprites[Random.Range(0, 2)];
        yield return new WaitForSeconds(14);
        Destroy(this.gameObject);
    }
}
