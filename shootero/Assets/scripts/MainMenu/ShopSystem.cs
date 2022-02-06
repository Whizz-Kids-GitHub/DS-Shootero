using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public Animator shopAnimator;

    public bool isClickable = true;

    public List<GameObject> sold;
    public ShopBuySystem shopBuySystem;

    //Turn Off Scripts
    public StartGameSystem startGameSystem;
    public SettingsSystem settingsSystem;

    public void ButtonShopOpen()
    {
        if (isClickable == true)
        {
            shopAnimator.SetBool("isOpen", true);
            for(int i = 0; i<=4; i++)
            {
                sold[i].SetActive(shopBuySystem.boughtSkin[i]);
            }
            startGameSystem.isClickable = false;
            settingsSystem.isClickable = false;
        }
    }
    public void ButtonShopClose()
    {
        shopAnimator.SetBool("isOpen", false);
        startGameSystem.isClickable = true;
        settingsSystem.isClickable = true;
    }
    public void BoughtUpdate(int which)
    {
        sold[which].SetActive(true);
    }
}
