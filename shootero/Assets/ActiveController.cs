using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveController : MonoBehaviour
{
    [SerializeField]
    public NumActive[] NumActive;

    [SerializeField]
    public ButtonsInfo[] ButtonsInfo;

    [SerializeField]
    public TarkovPrices[] TarkovPrices;

    public int Coins;

    public int Num;

    void SetButtonText(int num)
    {
        ButtonsInfo[num].priceButton.text = (ButtonsInfo[num].price.ToString());
        ButtonsInfo[num].amountButton.text = (ButtonsInfo[num].price.ToString());
    }

    void Buy(int num)
    {
        if (ButtonsInfo[num].times < TarkovPrices[num].amount.Length - 1)
        {
            Coins -= ButtonsInfo[num].price;
            ButtonsInfo[num].total += ButtonsInfo[num].amount;

            ButtonsInfo[num].times += 1;

            ButtonsInfo[num].price = TarkovPrices[num].price[ButtonsInfo[num].times];
            ButtonsInfo[num].amount = TarkovPrices[num].amount[ButtonsInfo[num].times];
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