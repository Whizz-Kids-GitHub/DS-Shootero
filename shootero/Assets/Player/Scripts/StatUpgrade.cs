using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    [Header("Stat Boost")]
    
    public int hp;
    public int atkDmg;
    public int atkSpd;
    public int bulSpd;
    public int heal;
    public int mvSpd;

    private PlayerMovement plyrMv;
    private PlayerShooting plyrSh;
    
    void Start()
    {
        plyrMv = FindObjectOfType<PlayerMovement>();
        plyrSh = FindObjectOfType<PlayerShooting>();

        plyrMv.pMaxHealth += hp; 
        plyrMv.pHealth += hp; 
        plyrMv.pHealth += heal;
    }

}
