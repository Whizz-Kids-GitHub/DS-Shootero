using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    public int pointsCount;
    private LineRenderer renderer;
    public float maxRadius;
    public float speed;
    public float width;

    [SerializeField]
    private float power;
    private float lift = 0f;

    private void Awake()
    {
        renderer = this.GetComponent<LineRenderer>();
        renderer.positionCount = pointsCount + 1;
    }

    public void BOOOM()
    {
        StartCoroutine(Blast());

        Vector3 explosionPos = transform.position;
        Collider[] collider = {null};
        collider[0] = PlayerMovement.Instance.gameObject.GetComponent<Collider>();
        foreach (Collider hit in collider)
        {
            if (hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, maxRadius, lift);
            }
        }
    }
    private IEnumerator Blast()
    {
        float currentRadius;
        currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            yield return null;
        }
    }
    void Draw(float currentRadius)
    {
        float angleBPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            renderer.SetPosition(i, position);
        }
        renderer.widthMultiplier = Mathf.Lerp(0f, width, 1f - currentRadius / maxRadius);
    }

}
