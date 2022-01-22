using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    public Animator shopAnimator;
    public bool isClickable;

    public StartGameSystem startGameSystem;
    public SettingsSystem settingsSystem;

    public void ButtonShopOpen()
    {
        if (isClickable == true)
        {
            shopAnimator.SetBool("isOpen", true);
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

}
