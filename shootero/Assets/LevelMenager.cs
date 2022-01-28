using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenager : MonoBehaviour
{
    public int Level;
    [SerializeField]
    private GameObject Slider;

    private static LevelMenager instance;
    public static LevelMenager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateSlider());
    }
    private IEnumerator UpdateSlider()
    {
        yield return new WaitForSeconds(1);
        Slider.GetComponent<Slider>().value = (Level * 4)/100;
    }
}
