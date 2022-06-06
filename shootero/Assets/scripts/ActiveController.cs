using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActiveController : MonoBehaviour
{
    [SerializeField]
    public NumActive[] NumActive;

    [SerializeField]
    public ButtonsInfo[] ButtonsInfo;

    [SerializeField]
    public TarkovPrices[] TarkovPrices;

    public static int Coins;

    public int Num;

    public int maxAmount;
    public int maxAmountNow;

    void Start()
    { 
        for (int num = 1; num < ButtonsInfo.Length; num++)
        {
            ButtonsInfo[num].price = TarkovPrices[num].price[ButtonsInfo[num].times];
            ButtonsInfo[num].amount = TarkovPrices[num].amount[ButtonsInfo[num].times];
        }
    }
    
    void SetButtonText(int num)
    {
        if(num > 0)
        {
            ButtonsInfo[num].priceButton.text = (ButtonsInfo[num].price.ToString());
            ButtonsInfo[num].amountButton.text = (ButtonsInfo[num].amount.ToString());
        }
    }

    void Set()
    {
        for (int i = 0; i < noumbs.Length; i++)
        {
            noumbs[i] = ButtonsInfo[i].amount;
        }
    }

    public static int[] noumbs = new int[5];//static to another scene
    
    public void Buy(int num)
    {

        if (Coins >= ButtonsInfo[num].price && ButtonsInfo[num].times <= maxAmount && ButtonsInfo[num].times <= maxAmountNow && (Num == num || (Num == 2 && num == 5)))
        {
            Debug.Log(num);

            Coins -= ButtonsInfo[num].price;
            ButtonsInfo[num].total += ButtonsInfo[num].amount;

            ButtonsInfo[num].times += 1;

            ButtonsInfo[num].price = TarkovPrices[num].price[ButtonsInfo[num].times];
            ButtonsInfo[num].amount = TarkovPrices[num].amount[ButtonsInfo[num].times];

            SetButtonText(num);

            if(num == 2)
            {
                SetButtonText(5);
            }
        }
    }
    
    public void ButtonPushUp()
    {
        if (NumActive[Num].Active.Length > 0 || NumActive[Num].inActive.Length > 0)
        {
            for (int i = 0; i < NumActive[Num].Active.Length; i++)
            {
                NumActive[Num].Active[i].SetActive(false);
            }

            for (int i = 0; i < NumActive[Num].inActive.Length; i++)
            {
                NumActive[Num].inActive[i].SetActive(true);
            }
        }

        if (Num < NumActive.Length - 1)
        {
            Num += 1;
        }
        else
        {
            Num = 0;
        }

        if (NumActive[Num].Active.Length > 0 || NumActive[Num].inActive.Length > 0)
        {
            for (int i = 0; i < NumActive[Num].Active.Length; i++)
            {
                NumActive[Num].Active[i].SetActive(true);
            }

            for (int i = 0; i < NumActive[Num].inActive.Length; i++)
            {
                NumActive[Num].inActive[i].SetActive(false);
            }
        }

        SetButtonText(Num);

        if (Num == 2)
        {
            SetButtonText(5);
        }
    }

    public void ButtonPushDown()
    {
        if (NumActive[Num].Active.Length > 0 || NumActive[Num].inActive.Length > 0)
        {
            for (int i = 0; i < NumActive[Num].Active.Length; i++)
            {
                NumActive[Num].Active[i].SetActive(false);
            }

            for (int i = 0; i < NumActive[Num].inActive.Length; i++)
            {
                NumActive[Num].inActive[i].SetActive(true);
            }
        }

        if (Num > 0)
        {
            Num -= 1;
        }
        else
        {
            Num = NumActive.Length - 1;
        }

        if (NumActive[Num].Active.Length > 0 || NumActive[Num].inActive.Length > 0)
        {
            for (int i = 0; i < NumActive[Num].Active.Length; i++)
            {
                NumActive[Num].Active[i].SetActive(true);
            }

            for (int i = 0; i < NumActive[Num].inActive.Length; i++)
            {
                NumActive[Num].inActive[i].SetActive(false);
            }
        }

        SetButtonText(Num);

        if (Num == 2)
        {
            SetButtonText(5);
        }

        Set();
    }
}

[System.Serializable]
public class NumActive
{
    public GameObject[] Active; 
    public GameObject[] inActive; 
}

[System.Serializable]
public class ButtonsInfo
{
    public int price;
    public int amount;
    [Space(15)]
    public int times;
    public int total;
    [Space(15)]
    public Text priceButton;
    public Text amountButton;
}

[System.Serializable]
public class TarkovPrices
{
    public int[] price;
    public int[] amount;
}