using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue                      Index")]
    public string BossDialogue = "1";
    public void StartDialogue(int dialIndex)
    {
        switch (dialIndex)
        {
            case 1:
                Debug.Log("You pissed me off");
                break;
        }
    }
}
