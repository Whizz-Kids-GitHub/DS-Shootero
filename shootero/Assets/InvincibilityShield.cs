using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityShield : MonoBehaviour
{
    public bool Heal;


    public void Shield(int Time)
    {
        FindObjectOfType<PlayerMovement>().invunerable = true;
        
        if(Heal)
        {

        }
        
        Invoke("Takedown", Time);
    }

    void Takedown()
    {
        FindObjectOfType<PlayerMovement>().invunerable = false;
        this.gameObject.SetActive(false);
    }

}
