using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuySystem : MonoBehaviour
{
    public int money;
    public List<bool> boughtSkin;
    public int chosenSkin;
    int whichSkin;
    public List<int> skinPrice;
    public ShopSystem shopSystem;

    public GameObject skinUI;
    public Animator skinUIAnimator;

    public SpriteRenderer shipSpriteRenderer;
    public List<GameObject> shipParticles;
    public List<Sprite> shipSprites;

    public MoneyCounter moneyCounter;

    public Button buyItButton;
    public Text buyItButtonText;
    public Text skinPriceText;

    private void Start()
    {
        moneyCounter.Value = money;
    }

    void TurnOn()
    {
        skinUI.SetActive(true);

        shipSpriteRenderer.sprite = shipSprites[whichSkin];

        if (boughtSkin[whichSkin])
        {
            buyItButtonText.text = "Sold";
            buyItButton.interactable = false;
            skinPriceText.text = "SOLD";
        }
        else
        {
            buyItButtonText.text = "Buy it";
            buyItButton.interactable = true;
            skinPriceText.text = skinPrice[whichSkin].ToString();
        }

        skinUIAnimator.SetBool("isOpen", true);
        shipParticles[0].SetActive(true);        
        shipParticles[1].SetActive(true);        
    }

    public void TurnOff()
    {
        StartCoroutine(TurningOff());
    }

    public IEnumerator TurningOff()
    {
        shipParticles[0].SetActive(false);
        shipParticles[1].SetActive(false);
        skinUIAnimator.SetBool("isOpen", false);
        yield return new WaitForSeconds(0.25f);
        skinUI.SetActive(false);
    }

    public void BuyIt()
    {
        if (boughtSkin[whichSkin] == false && money >= skinPrice[whichSkin])
        {
            money -= skinPrice[whichSkin];
            boughtSkin[whichSkin] = true;

            buyItButtonText.text = "Bought";
            buyItButton.interactable = false;

            shopSystem.BoughtUpdate(whichSkin);

            moneyCounter.Value = money;
        } else
        {
            Debug.Log("Za ma³o pieniêdzy");
        }
    }

    public void Skin1Pressed()
    {
        whichSkin = 0;
        TurnOn();
    }
    public void Skin2Pressed()
    {
        whichSkin = 1;
        TurnOn();
    }
    public void Skin3Pressed()
    {
        whichSkin = 2;
        TurnOn();
    }
    public void Skin4Pressed()
    {
        whichSkin = 3;
        TurnOn();
    }
    public void Skin5Pressed()
    {
        whichSkin = 4;
        TurnOn();
    }
}
