using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameAction : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Sample enemies", LoadSceneMode.Single);
    }
}
