using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSystem : MonoBehaviour
{
    public List<Animator> animators;
    public Animator startButton;
    StartGameSystem sgs;
    int animatorsLength;


    private void Start()
    {
        sgs = GetComponent<StartGameSystem>();
    }
    public void OpenSettingWindow()
    {
        animatorsLength = animators.Count;
        Debug.Log(animatorsLength);
        startButton.SetBool("Start", true);
        sgs.wasClicked = true;
        StartCoroutine(OpenSettingsWindow2());
    }
    IEnumerator OpenSettingsWindow2()
    {
        while (animatorsLength > 0)
        {
            animators[animatorsLength - 1].SetBool("isOpen", true);
            animatorsLength--;
            yield return null;
        }
    }
}
