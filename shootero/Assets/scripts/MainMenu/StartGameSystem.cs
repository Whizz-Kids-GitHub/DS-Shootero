using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSystem : MonoBehaviour
{
    GameObject mainCamera;
    int howManyAnimators;
    public bool wasClicked;
    public bool isClickable = true;
    public float cameraShake;
    public Animator[] animators;
    public AudioSource[] sounds;
    public Animator black;
    public float speed;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        howManyAnimators = animators.Length - 1;

    }
    public void StartGameButton()
    {
        if(wasClicked == false && isClickable == true)
        {
            StartCoroutine(StartGameCode());
            StartCoroutine(StartGame());
            wasClicked = true;
            isClickable = false;
        }
        
    }
    IEnumerator StartGame()
    {
        while (howManyAnimators >= 0)
        {
            animators[howManyAnimators].SetBool("Start", true);
            howManyAnimators--;
        }
        sounds[0].Play();
        yield return new WaitForSeconds(2);
        sounds[1].Play();
        yield return new WaitForSeconds(2);
        black.SetBool("Start", true);
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
