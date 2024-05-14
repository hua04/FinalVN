using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ChangeScene : MonoBehaviour
{
    public Animator animator;
    public GameObject button;
    public void Start()
    {
        button.SetActive(false);
    }
    [YarnCommand("button_appear")]
    public void ButtonAppear()
    {
        button.SetActive(true);
    }
    public void Change()
    {
        button.SetActive(false);
        animator.SetTrigger("FadeOutScene");
    }
    public void NewScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
