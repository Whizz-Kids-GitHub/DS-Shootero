using UnityEngine;
using UnityEngine.Animations;

public class CamShake : MonoBehaviour
{
    private Animator Anim;
    public int maxShake;
    public int shake;

    private void Start()
    {
        Anim = this.gameObject.GetComponent<Animator>();        
    }

    private void Update()
    {
        Anim.SetInteger("Shake", shake);
    }

    public void voidShake(bool Death)
    {
        if(Death)
        {
            Anim.SetInteger("Shake", maxShake + 1);
        }
        else
        {
            shake = Random.Range(1, maxShake);

            Anim.SetInteger("Shake", shake);
        }
    }
}
