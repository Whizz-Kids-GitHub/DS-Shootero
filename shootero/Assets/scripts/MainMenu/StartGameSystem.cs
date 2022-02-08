using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameSystem : MonoBehaviour
{
    GameObject mainCamera;
    public bool wasClicked;
    public bool isClickable = true;
    public float cameraShake;
    public AudioSource[] sounds;
    public Animator black;
    public float speed;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

    }
    public void StartGameButton()
    {
        if(wasClicked == false && isClickable == true)
        {
            StartCoroutine(StartGameCode());
            StartCoroutine(StartGame());
            wasClicked = true;
        }
        
    }
    IEnumerator StartGame()
    {
        sounds[0].Play();
        yield return new WaitForSeconds(2);
        sounds[1].Play();
        yield return new WaitForSeconds(2);
        black.SetBool("Start", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
    private void FixedUpdate()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,new Vector3(Random.Range(-cameraShake, cameraShake), Random.Range(-cameraShake, cameraShake), -10), speed);
    }

    private IEnumerator StartGameCode()
    {
        LevelCounter.Instance.PrePrepareStart();
        yield return new WaitForSeconds(4f);
        GameObject.Find("StartGameAction").GetComponent<StartGameAction>().ChangeScene();
    }
}
