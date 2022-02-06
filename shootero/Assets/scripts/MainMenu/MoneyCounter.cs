using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    public Text Text;
    public int CountFPS;
    public float Duration;
    public string NumberFormat = "N0";
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }
    private Coroutine CountingCoroutine;

    private void UpdateText(int newValue)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }

        CountingCoroutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS/* * Time.deltaTime)*/);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * /*Time.deltaTime* */ Duration)); // newValue = -20, previousValue = 0. CountFPS = 30, and Duration = 1; (-20- 0) / (30*1) // -0.66667 (ceiltoint)-> 0
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * /*Time.deltaTime */ Duration)); // newValue = 20, previousValue = 0. CountFPS = 30, and Duration = 1; (20- 0) / (30*1) // 0.66667 (floortoint)-> 0
        }

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                Text.text = previousValue.ToString(NumberFormat);

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                Text.text = previousValue.ToString(NumberFormat);

                yield return Wait;
            }
        }
    }
}